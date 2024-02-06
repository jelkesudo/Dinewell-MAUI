namespace Ispitpredaja;

public partial class MyProfile : ContentPage
{
	public MyProfile()
	{
		InitializeComponent();
	}

    private void Logout(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new MainPage());
        Preferences.Default.Set("user", "");
    }
}