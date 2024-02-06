using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Ispitpredaja.Bussiness;

namespace Ispitpredaja;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

	private async void RegisterUserButton(object sender, EventArgs e)
	{
        RegisterService registerService = new RegisterService();
        RegisterDTO registerDto = new RegisterDTO
        {
            Email = this.Email.Text,
            Password = this.Password.Text,
            Username = this.Username.Text,
            FirstName = this.FirstName.Text,
            LastName = this.LastName.Text
        };

        if (!string.IsNullOrEmpty(this.Address.Text))
        {
            registerDto = new RegisterDTO
            {
                Email = this.Email.Text,
                Password = this.Password.Text,
                Username = this.Username.Text,
                FirstName = this.FirstName.Text,
                LastName = this.LastName.Text,
                Address = this.Address.Text,
                AddressNumber = Int32.Parse(this.AddressNumber.Text),

            };
        }

        bool isRegistred = registerService.RegisterUser(registerDto);
        if (isRegistred)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            var snackbarOptions = new SnackbarOptions
            {
                BackgroundColor = Colors.Green,
                TextColor = Colors.White,
                ActionButtonTextColor = Colors.Gray,
                CornerRadius = new CornerRadius(10),
            };

            string text = "Successful registration";
            string actionButtonText = "Close";
            TimeSpan duration = TimeSpan.FromSeconds(5);

            var snackbar = Snackbar.Make(text, null, actionButtonText, duration, snackbarOptions);

            await snackbar.Show(cancellationTokenSource.Token);
            Application.Current.MainPage = new MainPage();
        }
    }

    private void GoToLoginPage(object sender, EventArgs e)
    {
        Application.Current.MainPage = new MainPage();//new NavigationPage(new MainPage());
    }
}