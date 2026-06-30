using Shuntghada.Services;
namespace Shuntghada.Pages;

public partial class SuggestMealPage : ContentPage
{
	private readonly ApiService _apiService;

	public SuggestMealPage()
	{
		InitializeComponent();
		_apiService = new ApiService();
	}

	private async void OnSubmitSuggestionClicked(object sender, EventArgs args)
	{
		string mealName = MealNameEntry.Text?.Trim();
		string ingredients = IngredientsEditor.Text?.Trim() ?? string.Empty;
		string instructions = InstructionsEditor.Text?.Trim() ?? string.Empty;

        // فحص الحقل الإجباري
        if (string.IsNullOrEmpty(mealName))
        {
            await DisplayAlert("خطأ", "يرجى كتابة اسم الطبخة أولاً!", "حسناً");
			return;
        }

		SubmitButton.IsEnabled = false;
        SubmitButton.Text = "جاري الإرسال...";

		// إرسال البيانات عبر السيرفس للـ API
		bool isSuccess = await _apiService.SendMealSuggestionAsync(mealName, ingredients, instructions);

		if(isSuccess)
		{
            await DisplayAlert("شكرًا لك! ❤️", "وصل اقتراحك بنجاح. سنقوم بفحصه وتنسيقه قريباً.", "رائع");

            // تصفير الحقول بعد النجاح
            MealNameEntry.Text = string.Empty;
            IngredientsEditor.Text = string.Empty;
            InstructionsEditor.Text = string.Empty;
        }
        else
        {
            // بما أن الرابط وهمي حالياً، سنعرض التنبيه التالي للفحص الظاهري
            await DisplayAlert("تنبيه محلي", "تمت محاكاة الإرسال بنجاح! (الرابط الفعلي للسيرفر غير مفعل حالياً).", "حسناً");
        }

        SubmitButton.IsEnabled = true;
        SubmitButton.Text = "إرسال الاقتراح للمراجعة 🚀";

    }




}