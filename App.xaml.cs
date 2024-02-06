using Ispitpredaja.Bussiness;
using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace Ispitpredaja;

public partial class App : Application
{
	public App()
	{
        InitializeComponent();
        
        MainPage = new MainPage();
    }

    protected async override void OnResume()
    {
        var user = Preferences.Default.Get("user", "");

        if (user == "")
        {
            return;
        }

        var dUser = JsonConvert.DeserializeObject<User>(user);

        if (dUser.ShouldBeLoggedOut)
        {
            await Logout();
        }

        base.OnResume();
    }

    protected async override void OnStart()
    {
        var user = Preferences.Default.Get("user", "");

        if (user == "")
        {
            return;
        }

        var dUser = JsonConvert.DeserializeObject<User>(user);

        if (dUser.ShouldBeLoggedOut)
        {
            await Logout();
        }
        else
        {
            MainPage = new HomePage();
        }

        base.OnStart();
    }

    private async Task Logout()
    {
        this.MainPage = new MainPage();
        Preferences.Default.Set("user", "");
    }
}
