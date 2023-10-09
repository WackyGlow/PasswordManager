using PMan.Entity;

namespace PMan;

public partial class PasswordPage : ContentPage
{
	public PasswordPage()
    {

        var _pw = new PasswordCollection();

		InitializeComponent();
        Shell.SetNavBarIsVisible(this, false);

        // Create a list of User objects
        var userList = new List<Password>
        {
            new Password{ Website = "Example.com", HashedLogin = "user1", HashedPassword = "password1" },
            new Password { Website = "SampleSite.com", HashedLogin = "user2", HashedPassword = "password2" },
            // Add more User objects as needed
        };

        // Set the list as the ItemsSource for the ListView
        UserListView.ItemsSource = _pw.GetAllPasswords();

    }

}