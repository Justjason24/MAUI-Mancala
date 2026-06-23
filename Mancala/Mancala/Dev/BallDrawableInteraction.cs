using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala.Dev
{
    public class BallDrawableInteraction : IDrawable
    {
        public float BallX = 100;
        public float BallY = 300;
        private float SpeedX = 3.0f;
        private float SpeedY = 3.0f;

        public int BallRadius = 35;

        Microsoft.Maui.Graphics.Color ballColor = Colors.White;

        public void UpdateBall(RectF bounds)
        {
            BallX += SpeedX;
            BallY += SpeedY;

            if (BallX < 0 || BallX >= bounds.Width - BallRadius)
                SpeedX = SpeedX * -1;

            if (BallY < 0 || BallY >= bounds.Height - BallRadius)
                SpeedY = SpeedY * -1;


        }

        public void ChangeColor()
        {
            ballColor = Colors.Red;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.FillColor = Colors.Purple;
            canvas.FillRectangle(dirtyRect);


            canvas.FillColor = ballColor;
            canvas.FillEllipse(BallX, BallY, BallRadius, BallRadius);
        }
    }
}
