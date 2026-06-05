using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala.Dev
{
    class BallDrawable : IDrawable
    {
        public float BallX { get; set; } = 300f;
        public float BallY { get; set; } = 100f;
        public float SpeedX { get; set; } = 3f;
        public float SpeedY { get; set; } = 3f;

        public void UpdateState(RectF bounds, double deltaTime)
        {
            // Multiply by deltaTime if you want time-consistent movement, 
            // or just apply raw frame velocity for simple tests.
            BallX += SpeedX;
            BallY += SpeedY;

            // Simple boundary collision checking
            if (BallX <= 0 || BallX >= bounds.Width - 40) SpeedX *= -1;
            if (BallY <= 0 || BallY >= bounds.Height - 40) SpeedY *= -1;
        }

        // The Render function
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            // Clear frame (handled by GraphicsView, but good to think about background color)
            canvas.FillColor = Colors.DarkSlateGray;
            canvas.FillRectangle(dirtyRect);

            // Draw our animated object
            canvas.FillColor = Colors.Coral;
            canvas.FillEllipse(BallX, BallY, 40, 40);
        }
    }
}
