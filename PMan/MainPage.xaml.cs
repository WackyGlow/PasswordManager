namespace PMan
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);
        }

        private void OnLoginClicked(object sender, EventArgs e)
        {
          
        }

        private async void OnCreateClicked(object sender, EventArgs e)
        {
            // Navigate to the create page here
            await Navigation.PushAsync(new NewUserPage());
        }

    }
}