namespace Ispitpredaja;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
	}

    private void OnHamburgerIconClicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = !Shell.Current.FlyoutIsPresented;
    }
}
