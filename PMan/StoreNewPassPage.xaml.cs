using ZstdSharp.Unsafe;

namespace PMan;

public partial class StoreNewPassPage : ContentPage
{
    private readonly PasswordCollection _ctx;
    private readonly KeyIvCollection _keyIvCollection;
    private readonly AesEncryptionHelper _aesEncryptionHelper;
    private readonly AesKeyGenerator _aesKeyGenerator;
    private readonly Byte[] key;
    private readonly Byte[] iv;
	public StoreNewPassPage()
	{
		InitializeComponent();
        Shell.SetNavBarIsVisible(this, false);

        _ctx = new PasswordCollection();
        _keyIvCollection = new KeyIvCollection();

        _aesKeyGenerator = new AesKeyGenerator();
        var checksum = _keyIvCollection.GetKeys();

        if (checksum != null)
        {
            key = _keyIvCollection.GetKeys().HashKey;
            iv = _keyIvCollection.GetKeys().HashKey;
        }
        else
        {
            key = _aesKeyGenerator.GenerateKey();
            iv = _aesKeyGenerator.GenerateIV();
        }

        
        _keyIvCollection.InsertValues(key,iv);

        _aesEncryptionHelper = new AesEncryptionHelper(key, iv);
    }



    private async void OnCreateClicked(object sender, EventArgs e)
    {
        // Handle the create button click event here.
        string website = WebsiteEntry.Text;
        string loginName = LoginNameEntry.Text;
        string password = PasswordEntry.Text;

        // Perform actions with the entered data.
        var encryptedLogin = _aesEncryptionHelper.Encrypt(loginName);
        var encryptedPassword = _aesEncryptionHelper.Encrypt(password);

        _ctx.AddNewPass(website, encryptedLogin, encryptedPassword);

        await Navigation.PopAsync();

    }
}