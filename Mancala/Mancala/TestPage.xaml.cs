using Mancala.Dev;
using System.Diagnostics;

namespace Mancala;

public partial class TestPage : ContentPage
{
    private bool gameRunning = false;
    private BallDrawable testBallDrawable;

    public TestPage()
	{
		InitializeComponent();

	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        testBallDrawable = (BallDrawable)GameCanvas.Drawable;
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

            RectF bounds = new RectF(0, 0, (float)GameCanvas.Width, (float)GameCanvas.Height);
           testBallDrawable.UpdateState(bounds);

            // 3. Force the GraphicsView to repaint immediately
            GameCanvas.Invalidate();

            await Task.Delay(2); // i think this is 500 fps? 1000 ms / 2ms 
        }
    }
}