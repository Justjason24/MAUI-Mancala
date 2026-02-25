using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{
    internal class PitDrawable : IDrawable
    {
        public int PebbleCount { get; set; }
        public void Draw(ICanvas canvas, RectF rect)
        {
            float pitRadius = Math.Min(rect.Width, rect.Height) / 2f;

            // 1. Draw pit background
            canvas.FillColor = Color.FromArgb("#ffc18c");
            canvas.FillCircle(rect.Center.X, rect.Center.Y, pitRadius);

            canvas.StrokeColor = Colors.Azure;
            canvas.StrokeSize = 2;
            canvas.DrawCircle(rect.Center.X, rect.Center.Y, pitRadius);


            double radius = 6;
            //double padding = .5;

            var rng = new Random();

            for (int i = 0; i < PebbleCount; i++)
            {
                double x = (rng.NextDouble() * (rect.Width - radius * 2)) + radius;
                double y = (rng.NextDouble() * (rect.Height - radius * 2)) + radius;

                //double x = i++;
                //double y = i++;

                canvas.FillColor = Colors.White;
                canvas.FillCircle((float)x, (float)y, (float)radius);

                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 2;
                canvas.DrawCircle((float)x, (float)y, (float)radius);


            }
        }
    }
}
