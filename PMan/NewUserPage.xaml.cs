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
        
    }
}