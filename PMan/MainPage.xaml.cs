namespace PMan
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PasswordPage());
        }

        private async void OnCreateClicked(object sender, EventArgs e)
        {
            // Navigate to the create page here
            await Navigation.PushAsync(new NewUserPage(), false);
        }

    }
}