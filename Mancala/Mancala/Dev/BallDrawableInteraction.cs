using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace Mancala.Dev
{
    public class BallDrawableInteraction : IDrawable
    {

        public class JYBall
        {
            public float BallX = 100;
            public float BallY = 300;
            public float SpeedX = 3.0f;
            public float SpeedY = 3.0f;

            public int BallRadius = 35;

            public Microsoft.Maui.Graphics.Color ballColor = Colors.White;
        }

        public List<JYBall> balls = new List<JYBall>
        {
            new JYBall { BallX = 100, BallY = 300, SpeedX = 3.0f, SpeedY = 3.0f, BallRadius = 35 },
            new JYBall { BallX = 200, BallY = 400, SpeedX = 3.0f, SpeedY = 3.0f, BallRadius = 35 },
        };


        public void UpdateBall(RectF bounds)
        {
            foreach(var ball in balls)
            {
                ball.BallX += ball.SpeedX;
                ball.BallY += ball.SpeedY;

                if (ball.BallX < 0 || ball.BallX >= bounds.Width - ball.BallRadius)
                    ball.SpeedX = ball.SpeedX * -1;

                if (ball.BallY < 0 || ball.BallY >= bounds.Height - ball.BallRadius)
                    ball.SpeedY = ball.SpeedY * -1;
            }

        }

        public void ChangeColorIfHit(double x, double y)
        {
            foreach( var ball in balls)
            {
                var distance = Math.Sqrt((x - ball.BallX) * (x - ball.BallX) + (y - ball.BallY) * (y - ball.BallY));

                if (distance < ball.BallRadius)
                    ball.ballColor = Colors.Red;
            }
        }


        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.FillColor = Colors.Purple;
            canvas.FillRectangle(dirtyRect);


            foreach( var ball in balls)
            {
                canvas.FillColor = ball.ballColor;
                canvas.FillEllipse(ball.BallX, ball.BallY, ball.BallRadius, ball.BallRadius);
            }

        }
    }
}
