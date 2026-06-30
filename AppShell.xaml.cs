namespace Shuntghada
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void OnSettingsFlyoutClicked(object sender, EventArgs e)
        {
            // إغلاق الشريط الجانبي تلقائياً بعد الضغط
            Current.FlyoutIsPresented = false;

            // تنبيه مؤقت لحين بناء صفحة الإعدادات في فرعها الخاص لاحقاً
            await DisplayAlert("إعدادات التطبيق", "هذه الصفحة سنبنيها بالتفصيل في الخطوة رقم 6 (نظام التنبيهات وتصفير السجل).", "حسناً");

            // مستقبلاً عندما تجهز الصفحة، سيكون سطر الانتقال هكذا:
             //await Navigation.PushAsync(new SettingsPage());
        }
    }
}
