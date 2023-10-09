using Konscious.Security.Cryptography;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace PMan;

public partial class NewUserPage : ContentPage
{
	public NewUserPage()
	{
		InitializeComponent();
        Shell.SetNavBarIsVisible(this, false);
    }

    private async void OnCreateClicked(object sender, EventArgs e)
    {
        var passwordEntry = PasswordEntry.Text;
        try
        {
            if (passwordEntry != null)
            {
                string password = passwordEntry;
                Console.WriteLine(password);
                var _mpdb = new MasterPasswordCollection();
                _mpdb.NewMPassword(password);
                await DisplayAlert("Alert", "Password Created", "OK");
            }
            else
            {
                //do absolutely nothing... yet...
            }
        }
        catch (Exception ex)
        {
            // Handle the exception here
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}