using System.Runtime.CompilerServices;

namespace PMan;

public partial class NewUserPage : ContentPage
{
	public NewUserPage()
	{
		InitializeComponent();
        Shell.SetNavBarIsVisible(this, false);
    }

    private void OnCreateClicked(object sender, EventArgs e)
    {
        var passwordEntry = PasswordEntry.Text;

        // Now, you can use the passwordEntry variable to access the Entry field's properties or value.
        if (passwordEntry != null)
        {
            string password = passwordEntry;
            Console.WriteLine(password);
            var _mpdb = new MasterPasswordCollection();
            _mpdb.NewMPassword(password);
            Navigation.PopAsync();
        }
        else
        {
            //do absolutely nothing... yet...
        }


        
    }
}