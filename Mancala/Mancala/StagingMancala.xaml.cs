using Mancala.Models;
using Mancala.Staging;

namespace Mancala;

public partial class StagingMancala : ContentPage
{
    private bool gameRunning = false;
    private StagingGameDrawable gameDrawable;

    public GameState GameState = new Mancala.Models.GameState();

    public StagingMancala()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        gameDrawable = (StagingGameDrawable)StagingGameScreen.Drawable;

        //PSEUDO data binding. The Drawable and this class are pointed to the same instance.
        var gameState = new GameState();
        gameDrawable.GameState = gameState;
        this.GameState = gameState;

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
            await Task.Delay(20);
        }
    }

    private void OnPointerPressed(object sender, PointerEventArgs e)
    {
        Console.WriteLine("I am here");
        var point = e.GetPosition((View)sender);

        double x = point.Value.X;
        double y = point.Value.Y;

        string pitClicked = GameHelper.DeterminePitHit(this.GameState, x, y);

        gameDrawable.DebugHitOnStore(x, y);

        

        if(!String.IsNullOrEmpty(pitClicked))
        {
            GameState.ConvertPitClickedToMove(pitClicked);
            GameState.Update();

            gameDrawable.UpdateUI(this.GameState);
        }

        Console.WriteLine("idk");
    }


}