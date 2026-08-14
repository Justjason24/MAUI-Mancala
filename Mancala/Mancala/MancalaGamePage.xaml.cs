using Mancala.Models;

namespace Mancala;

public partial class MancalaGamePage : ContentPage
{
    private bool gameRunning = false;
    private GameDrawable gameDrawable;

    public MancalaGamePage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        gameDrawable = (GameDrawable)MyGameScreen.Drawable;
        StartGameLoop();
    }

    private void StartGameLoop()
    {
        gameRunning = true;
        Dispatcher.Dispatch(async () => await GameLoop());
    }

    private async Task GameLoop()
    {
        while (gameRunning)
        {

            await Task.Delay(2);
        }
    }

    private void OnPointerPressed(object sender, PointerEventArgs e)
    {
        Console.WriteLine("I am here");
        var point = e.GetPosition((View)sender);

        double x = point.Value.X;
        double y = point.Value.Y;

        gameDrawable.CheckIfStoreHit(x, y);

        gameDrawable.CheckIfPitIsHit(x, y);

        Console.WriteLine("idk");
    }
}