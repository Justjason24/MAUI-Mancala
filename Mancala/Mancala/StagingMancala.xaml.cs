using Mancala.Models;
using Mancala.Staging;

namespace Mancala;

public partial class StagingMancala : ContentPage
{
    private bool gameRunning = false;
    private StagingGameDrawable gameDrawable;

    public StagingMancala()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        gameDrawable = (StagingGameDrawable)StagingGameScreen.Drawable;
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
            StagingGameScreen.Invalidate();
            await Task.Delay(100);
        }
    }

    private void OnPointerPressed(object sender, PointerEventArgs e)
    {
        Console.WriteLine("I am here");
        var point = e.GetPosition((View)sender);

        double x = point.Value.X;
        double y = point.Value.Y;

        //gameDrawable.CheckIfStoreHit(x, y);

        gameDrawable.CheckIfPitIsHit(x, y);

        gameDrawable.DebugHitOnStore(x, y);

        Console.WriteLine("idk");
    }
}