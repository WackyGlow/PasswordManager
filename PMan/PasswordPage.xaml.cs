using PMan.Entity;

namespace PMan;

public partial class PasswordPage : ContentPage
{
	public PasswordPage()
    {

        var _pw = new PasswordCollection();

		InitializeComponent();
        Shell.SetNavBarIsVisible(this, false);
        // Set the list as the ItemsSource for the ListView
        UserListView.ItemsSource = _pw.GetAllPasswords();

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // This code will run when the page is about to be displayed
        // Put your reload logic here
        ReloadPageData();
    }
    private void ReloadPageData()
    {
        var _pw = new PasswordCollection();
        UserListView.ItemsSource = _pw.GetAllPasswords();
    }



    private async void OnAddClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new StoreNewPassPage());
    }

}