
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

    private async void Pit51_Tapped(object sender, EventArgs e)
    {
        // Move the pebble from Pit51 (row 5, col 1) to Pit41 (row 4, col 1)
        Pebble.TranslationX = 0;
        Pebble.TranslationY = 0;

        // Animate upward by one row (approx 70px depending on spacing)
        await Pebble.TranslateTo(0, -70, 400, Easing.CubicInOut);

        // Snap pebble into Pit41 grid cell
        Grid.SetRow(Pebble, 4);
        Grid.SetColumn(Pebble, 1);

        // Reset translation after moving grid cell
        Pebble.TranslationX = 0;
        Pebble.TranslationY = 0;
    }


}