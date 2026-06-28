using Shuntghada.Models;
using Shuntghada.Services;

namespace Shuntghada.Pages
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDatabaseService _databaseService;
        private Meal _currentSuggestedMeal;

        // تمرير الـ Service عبر الـ Constructor بفضل الـ Dependency Injection الذي ضبطناه
        public MainPage(LocalDatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        // حدث الضغط على زر الاقتراح
        private async void OnSuggestButtonClicked(object sender, EventArgs e)
        {
            _currentSuggestedMeal = await _databaseService.GetRandomMealSuggestionAsync(7);

            if (_currentSuggestedMeal != null)
            {
                // 1. تصغير الشفافية مؤقتاً لتجنب الوميض أثناء تغيير البيانات
                SuggestionCard.Opacity = 0;

                // 2. إسناد الصورة والبيانات
                MealImage.Source = _currentSuggestedMeal.RecipeImageUrl;
                MealNameLabel.Text = _currentSuggestedMeal.Name;
                MealAltNameLabel.Text = string.IsNullOrEmpty(_currentSuggestedMeal.AltName) ? "" : $"({_currentSuggestedMeal.AltName})";
                DifficultyLabel.Text = $"المستوى: {_currentSuggestedMeal.DifficultyLevel}";
                DescriptionLabel.Text = _currentSuggestedMeal.Description;

                AcceptButton.IsEnabled = true;
                AcceptButton.Text = "صادقنا عليها ✔️";
                AcceptButton.BackgroundColor = Color.Parse("#388E3C");

                // 3. حركة أنيميشن لطيفة تزيد الشفافية خلال 300 ملي ثانية لضمان ثبات الرندرة
                await SuggestionCard.FadeTo(1, 300);
            }
            else
            {
                await DisplayAlert("تنبيه", "لم نجد أي أكلات في قاعدة البيانات!", "موافق");
            }
        }
        // حدث الضغط على زر طريقة العمل
        private async void OnViewRecipeClicked(object sender, EventArgs e)
        {
            if (_currentSuggestedMeal != null)
            {
                // عرض خطوات الطبخ في نافذة منبثقة مريحة
                await DisplayAlert($"طريقة إعداد: {_currentSuggestedMeal.Name}",
                                     _currentSuggestedMeal.RecipeInstructions,
                                     "فهمت، شكراً");
            }
        }

        // حدث الضغط على زر الاعتماد ( يله)
        private async void OnAcceptButtonClicked(object sender, EventArgs e)
        {
            if (_currentSuggestedMeal != null)
            {
                // حفظ الأكلة في جدول السجل (History)
                await _databaseService.SaveMealToHistoryAsync(_currentSuggestedMeal.Id);

                // تغيير شكل الزر ليعطي انطباعاً بالتأكيد للمستخدم
                AcceptButton.IsEnabled = false;
                AcceptButton.Text = "تم الحفظ في السجل! ✔️";
                AcceptButton.BackgroundColor = Color.Parse("#757575");

                await DisplayAlert("بالعافية!", $"تم تسجيل {_currentSuggestedMeal.Name} كطبخة اليوم. لن تظهر مجدداً في الاقتراحات لمدة أسبوع.", "شكراً");
            }
        }
    }
}