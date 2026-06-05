using Mancala.Dev;
using System.Diagnostics;

namespace Mancala;

public partial class TestPage : ContentPage
{
    private bool _isGameRunning = false;
    private readonly Stopwatch _stopwatch = new();
    private double _lastFrameTime = 0;
    private BallDrawable _ballDrawable;

    public TestPage()
	{
		InitializeComponent();
        _ballDrawable = new BallDrawable();
        GameCanvas.Drawable = _ballDrawable;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ballDrawable = (BallDrawable)GameCanvas.Drawable;
        StartGameLoop();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _isGameRunning = false; // Stop loop when navigating away
    }

    private void StartGameLoop()
    {
        _isGameRunning = true;
        _stopwatch.Start();
        _lastFrameTime = _stopwatch.Elapsed.TotalSeconds;

        // Kick off the loop safely on the main thread
        Dispatcher.Dispatch(async () => await GameLoop());
    }

    private async Task GameLoop()
    {
        while (_isGameRunning)
        {
            // 1. Calculate Delta Time (time elapsed since last frame)
            double currentTime = _stopwatch.Elapsed.TotalSeconds;
            double deltaTime = currentTime - _lastFrameTime;
            _lastFrameTime = currentTime;

            // 2. Fetch layout size and update game state
            RectF bounds = new RectF(0, 0, (float)GameCanvas.Width, (float)GameCanvas.Height);
           _ballDrawable.UpdateState(bounds, deltaTime);

            // 3. Force the GraphicsView to repaint immediately
            GameCanvas.Invalidate();

            // 4. Yield back to the system to let the OS process UI inputs and sync with display
            // Task.Delay(1) tells the thread scheduler to breathe for a split ms, matching refresh speeds closely.
            await Task.Delay(1);
        }
    }
}