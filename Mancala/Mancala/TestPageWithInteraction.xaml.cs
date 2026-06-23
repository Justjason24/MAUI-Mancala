using Mancala.Dev;
using System.Threading.Tasks.Dataflow;

namespace Mancala;

public partial class TestPageWithInteraction : ContentPage
{
    private bool gameRunning = false;
    private BallDrawableInteraction testBallDrawable;

    public TestPageWithInteraction()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        testBallDrawable = (BallDrawableInteraction)MyGameScreen.Drawable;
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

            RectF bounds = new RectF(0, 0, (float)MyGameScreen.Width, (float)MyGameScreen.Height);
            testBallDrawable.UpdateBall(bounds);

            // 3. Force the GraphicsView to repaint immediately
            MyGameScreen.Invalidate();

            await Task.Delay(2); // i think this is 500 fps? 1000 ms / 2ms 
        }
    }

    private void OnPointerPressed(object sender, PointerEventArgs e)
    {
        Console.WriteLine("I am here");
        var point = e.GetPosition((View)sender);

        float ballx = testBallDrawable.BallX;
        float bally = testBallDrawable.BallY;
        float ballRadius = testBallDrawable.BallRadius;

        double x = point.Value.X;
        double y = point.Value.Y;

        // thank you old greek Pythagoreas and the internet
        var distance = Math.Sqrt((x - ballx) * (x - ballx) + (y - bally) * (y - bally));

        if(distance < ballRadius)
        {
            testBallDrawable.ChangeColor();
        }

        Console.WriteLine("stop here");
    }

}