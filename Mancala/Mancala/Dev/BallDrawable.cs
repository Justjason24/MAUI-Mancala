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
        public float SpeedY { get; set; } = 4f;

        public int debugConter = 0;

        public void UpdateState(RectF bounds)
        {
            BallX += SpeedX;
            BallY += SpeedY;

            // Simple boundary collision checking
            if (BallX <= 0 || BallX >= bounds.Width - 40)
            {
                SpeedX *= -1;
                debugConter++;
            }
                
            if (BallY <= 0 || BallY >= bounds.Height - 40)
            {
                SpeedY *= -1;
            }

            if(bounds.Width > 0)
                Console.WriteLine("how??");
                
        }

        // The Render function
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.FillColor = Colors.DarkSlateGray;
            canvas.FillRectangle(dirtyRect);


            canvas.FillColor = Colors.Coral;
            canvas.FillEllipse(BallX, BallY, 40, 40);
        }
    }
}
