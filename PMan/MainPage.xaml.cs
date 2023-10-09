namespace PMan
{
    public partial class MainPage : ContentPage
    {
        private MasterPasswordCollection _mp;
        public MainPage()
        {
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);
            _mp = new MasterPasswordCollection();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var hasher = new PasswordHasher();
            var password = PasswordEntry.Text;
            if (password == null)
            {
                await DisplayAlert("Alert", "Please Input a password", "OK");
            }
            else
            {
                if (_mp.Login(password))
                {
                    await Navigation.PushAsync(new PasswordPage());
                }
                else
                {
                    await DisplayAlert("Alert", "NOT VERIFIED!", "OK");
                }
            }
            
        }

        private async void OnCreateClicked(object sender, EventArgs e)
        {
            // Navigate to the create page here
            var checksum = _mp.GetCurrentId();
            switch (checksum)
            {
                case 0:
                    await Navigation.PushAsync(new NewUserPage()); break;
                case 1:
                    await DisplayAlert("Alert", "Password already set", "OK"); break;
                default:
                    await DisplayAlert("Alert", "Something went wrong", "OK"); break;
            }
            
            
        }

    }
}