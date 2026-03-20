using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{
    public class PebbleDrawable : IDrawable
    {
        //public Color PebbleColor { get; set; } = Colors.White; // for now 
        //public void Draw(ICanvas canvas, RectF rect)
        //{
        //    float radius = Math.Min(rect.Width, rect.Height) / 2f;
        //    canvas.FillColor = PebbleColor;
        //    canvas.FillCircle(rect.Center.X, rect.Center.Y, radius);
        //    canvas.StrokeColor = Colors.Black;
        //    canvas.StrokeSize = 2;
        //    canvas.DrawCircle(rect.Center.X, rect.Center.Y, radius);

        //}

        public int PebbleCount { get; set; }

        public double Radius = 6;

        public void Draw(ICanvas canvas, RectF rect)
        {
            if (PebbleCount <= 0) return;

            bool debug = true;

            if (!debug)
            {
                DrawPebblesAlgo(canvas, rect);
            }
            else
            {
                DrawPebblesDebug(canvas, rect);
            }
        }

        private void DrawPebblesAlgo(ICanvas canvas, RectF rect)
        {
            var rng = new Random();
            var pebbles = new List<(double X, double Y)>();

            foreach (var pebble in Enumerable.Range(1, PebbleCount))
            {
                if (pebble == 1)
                {
                    pebbles.Add((0, 0));
                }
                else if (pebble == 2)
                {
                    var randSeed = rng.NextDouble() * 6;
                    var someAlgoX = pebbles[0].X + (Radius * Math.Cos(randSeed));
                    var someAlgoY = pebbles[0].Y + (Radius * Math.Sin(randSeed));
                    someAlgoX += someAlgoX > 0 ? Radius - 1.5 : -Radius + 1.5;
                    someAlgoY += someAlgoY > 0 ? Radius - 1.5 : -Radius + 1.5;
                    pebbles.Add((Math.Round(someAlgoX, 3), Math.Round(someAlgoY, 0)));
                }
                else
                {
                    var recentPebbles = pebble <= 7
                        ? new List<(double, double)> { pebbles[0], pebbles.Last() }
                        : pebbles.GetRange(pebble - 6, 2);

                    var sin60 = Math.Sin(Math.PI / 3);
                    var cos60 = Math.Cos(Math.PI / 3);
                    var (x1, y1) = recentPebbles[0];
                    var (x2, y2) = recentPebbles[1];

                    var x3a = Math.Round(x1 + cos60 * (x2 - x1) - sin60 * (y2 - y1), 3);
                    var y3a = Math.Round(y1 + sin60 * (x2 - x1) + cos60 * (y2 - y1), 3);
                    var x3b = Math.Round(x1 + cos60 * (x2 - x1) + sin60 * (y2 - y1), 3);
                    var y3b = Math.Round(y1 - sin60 * (x2 - x1) + cos60 * (y2 - y1), 3);

                    if (pebbles.Any(p => p.X == x3a) || pebbles.Any(p => p.Y == y3a))
                        pebbles.Add((x3b, y3b));
                    else
                        pebbles.Add((x3a, y3a));
                }
            }

            foreach (var (px, py) in pebbles)
            {
                float cx = (float)(rect.Center.X + px);
                float cy = (float)(rect.Center.Y + py);
                canvas.FillColor = Colors.White;
                canvas.FillCircle(cx, cy, (float)Radius);
                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 2;
                canvas.DrawCircle(cx, cy, (float)Radius);
            }
        }

        private void DrawPebblesDebug(ICanvas canvas, RectF rect)
        {
            if (PebbleCount != 4) return;

            if (PebbleCount == 4)
            {
                double x = 25;
                double y = 25;

                canvas.FillColor = Colors.White;
                canvas.FillCircle((float)x, (float)y, (float)Radius);

                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 2;
                canvas.DrawCircle((float)x, (float)y, (float)Radius);

                x = 25;
                y = 35;

                canvas.FillColor = Colors.White;
                canvas.FillCircle((float)x, (float)y, (float)Radius);

                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 2;
                canvas.DrawCircle((float)x, (float)y, (float)Radius);

                x = 35;
                y = 25;

                canvas.FillColor = Colors.White;
                canvas.FillCircle((float)x, (float)y, (float)Radius);

                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 2;
                canvas.DrawCircle((float)x, (float)y, (float)Radius);

                x = 35;
                y = 35;

                canvas.FillColor = Colors.White;
                canvas.FillCircle((float)x, (float)y, (float)Radius);

                canvas.StrokeColor = Colors.Black;
                canvas.StrokeSize = 2;
                canvas.DrawCircle((float)x, (float)y, (float)Radius);
            }
        }
    }
}
