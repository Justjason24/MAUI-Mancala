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

            //for (int i = 0; i < PebbleCount; i++)
            //{
            //    //double x = (rng.NextDouble() * (rect.Width - radius * 2)) + radius;
            //    //double y = (rng.NextDouble() * (rect.Height - radius * 2)) + radius;

            //    double x = 30;
            //    double y = 30;

            //    canvas.FillColor = Colors.White;
            //    canvas.FillCircle((float)x, (float)y, (float)radius);

            //    canvas.StrokeColor = Colors.Black;
            //    canvas.StrokeSize = 2;
            //    canvas.DrawCircle((float)x, (float)y, (float)radius);


            //}


            // TODO - REWORK THIS LATER!!! 
            if(PebbleCount == 1)
            {
                double x = 30;
                double y = 30;

                canvas.FillColor = Colors.White;
                canvas.FillCircle((float)x, (float)y, (float)radius);

                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 2;
                canvas.DrawCircle((float)x, (float)y, (float)radius);
            }

            else if(PebbleCount == 2)
            {
                double x = 25;
                double y = 30;

                canvas.FillColor = Colors.White;
                canvas.FillCircle((float)x, (float)y, (float)radius);

                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 2;
                canvas.DrawCircle((float)x, (float)y, (float)radius);

                 x = 35;
                 y = 30;

                canvas.FillColor = Colors.White;
                canvas.FillCircle((float)x, (float)y, (float)radius);

                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 2;
                canvas.DrawCircle((float)x, (float)y, (float)radius);
            }

            else if(PebbleCount == 3)
            {
                double x = 25;
                double y = 35;

                canvas.FillColor = Colors.White;
                canvas.FillCircle((float)x, (float)y, (float)radius);

                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 2;
                canvas.DrawCircle((float)x, (float)y, (float)radius);

                x = 35;
                y = 35;

                canvas.FillColor = Colors.White;
                canvas.FillCircle((float)x, (float)y, (float)radius);

                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 2;
                canvas.DrawCircle((float)x, (float)y, (float)radius);

                x = 30;
                y = 25;

                canvas.FillColor = Colors.White;
                canvas.FillCircle((float)x, (float)y, (float)radius);

                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 2;
                canvas.DrawCircle((float)x, (float)y, (float)radius);
            }

            else if (PebbleCount == 4)
            {
                double x = 25;
                double y = 25;

                canvas.FillColor = Colors.White;
                canvas.FillCircle((float)x, (float)y, (float)radius);

                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 2;
                canvas.DrawCircle((float)x, (float)y, (float)radius);

                x = 25;
                y = 35;

                canvas.FillColor = Colors.White;
                canvas.FillCircle((float)x, (float)y, (float)radius);

                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 2;
                canvas.DrawCircle((float)x, (float)y, (float)radius);

                x = 35;
                y = 25;

                canvas.FillColor = Colors.White;
                canvas.FillCircle((float)x, (float)y, (float)radius);

                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 2;
                canvas.DrawCircle((float)x, (float)y, (float)radius);

                x = 35;
                y = 35;

                canvas.FillColor = Colors.White;
                canvas.FillCircle((float)x, (float)y, (float)radius);

                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 2;
                canvas.DrawCircle((float)x, (float)y, (float)radius);
            }
            else
            {
                for (int i = 0; i < PebbleCount; i++)
                {
                    double x = (rng.NextDouble() * (rect.Width - radius * 2)) + radius;
                    double y = (rng.NextDouble() * (rect.Height - radius * 2)) + radius;

                    canvas.FillColor = Colors.White;
                    canvas.FillCircle((float)x, (float)y, (float)radius);

                    canvas.StrokeColor = Colors.Black;
                    canvas.StrokeSize = 2;
                    canvas.DrawCircle((float)x, (float)y, (float)radius);


                }
            }
        }
    }
}
