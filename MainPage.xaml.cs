using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Ispitpredaja.Bussiness;
using Newtonsoft.Json;

namespace Ispitpredaja;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
        AuthService authService = new AuthService();
        User loginUser = authService.Auth(this.Email.Text, this.Password.Text);

        if (loginUser == null)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            var snackbarOptions = new SnackbarOptions
            {
                BackgroundColor = Colors.White,
                TextColor = Colors.Black,
                ActionButtonTextColor = Colors.Red,
                CornerRadius = new CornerRadius(10),
            };

            string text = "Error while logging.";
            string actionButtonText = "X";
            TimeSpan duration = TimeSpan.FromSeconds(3);

            var snackbar = Snackbar.Make(text, null, actionButtonText, duration, snackbarOptions);

            snackbar.Show(cancellationTokenSource.Token);
        }
        else
        {
            Preferences.Default.Set("jwt", loginUser.Token);
            Preferences.Default.Set("user", JsonConvert.SerializeObject(loginUser));

            if (loginUser.Role != "Admin")
            {
                Application.Current.MainPage = new UnauthorizedUserPage();
            }
            else
            {
                Application.Current.MainPage = new HomePage();
            }
        }
    }

    private void GoToRegisterPage(object sender, EventArgs e)
    {
        Application.Current.MainPage = new RegisterPage();
    }
}

