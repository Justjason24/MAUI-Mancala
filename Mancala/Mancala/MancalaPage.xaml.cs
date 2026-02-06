namespace Mancala;

public partial class MancalaPage : ContentPage
{
	public MancalaPage()
	{
        InitializeComponent();
    }


    private async void Button_Clicked(object sender, EventArgs e)
    {
        // Navigate to the specified URL in the system browser.
        await Launcher.Default.OpenAsync("https://espn.com");
    }

}