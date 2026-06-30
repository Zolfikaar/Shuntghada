using System.Text;
using System.Text.Json;


namespace Shuntghada.Services
{
    public class ApiService
    {
        private HttpClient _httpClient;

        // استبدل هذا الرابط برابط سيرفر UrLabs الفعلي لاحقاً
        private const string BaseUrl = "https://api.urlabs.com/shuntghada/suggestions";

        public ApiService ()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> SendMealSuggestionAsync(string name, string ingredients, string instructions)
        {
            try
            {
                var mealSuggestion = new
                {
                    meal_name = name,
                    Ingredients = ingredients,
                    Instructions = instructions,
                    submited_at = DateTime.Now,
                };

                string json = JsonSerializer.Serialize(mealSuggestion);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // إرسال الطلب للسيرفر
                var response = await _httpClient.PostAsync(BaseUrl, content);
                // تعيد true إذا كان كود الحالة 200 أو 201 (نجاح)
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // في بيئة الإنتاج يمكنك تسجيل الخطأ (Log)، حالياً نعيد false لحماية التطبيق
                return false;
            }
    }
}
}
