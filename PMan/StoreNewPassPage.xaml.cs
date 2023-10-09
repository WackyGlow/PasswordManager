using ZstdSharp.Unsafe;

namespace PMan;

public partial class StoreNewPassPage : ContentPage
{
    private readonly PasswordCollection _ctx; 
	public StoreNewPassPage()
	{
		InitializeComponent();
        Shell.SetNavBarIsVisible(this, false);

        _ctx = new PasswordCollection();
    }



    private void OnCreateClicked(object sender, EventArgs e)
    {
        // Handle the create button click event here.
        string website = WebsiteEntry.Text;
        string loginName = LoginNameEntry.Text;
        string password = PasswordEntry.Text;

        // Perform actions with the entered data.
        

    }
}