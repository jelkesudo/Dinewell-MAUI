namespace Ispitpredaja;

public partial class UnauthorizedUserPage : ContentPage
{
	public UnauthorizedUserPage()
	{
		InitializeComponent();
	}

	private void LogOutUser(object sender, EventArgs e)
	{
        Application.Current.MainPage = new MainPage();
        Preferences.Default.Set("user", "");
    }
}