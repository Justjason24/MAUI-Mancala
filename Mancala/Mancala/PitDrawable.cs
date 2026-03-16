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


            bool debug = true;
            double radius = 6;
            if (!debug)
            {
                //double padding = .5;

                var rng = new Random();

                var pebbles = new List<Tuple<double, double>>();

                foreach (var pebble in Enumerable.Range(1, PebbleCount))
                {
                    if (pebble == 1)
                    {
                        pebbles.Add(new(0, 0));
                    }
                    else
                    {
                        if (pebble == 2)
                        {
                            var randSeed = (rng.NextDouble() * 6);

                            var someAlgoX = (pebbles[0].Item1 + (radius * Math.Cos(randSeed)));
                            var someAlgoY = (pebbles[0].Item2 + (radius * Math.Sin(randSeed)));

                            someAlgoX += someAlgoX > 0 ? radius - 1.5 : -radius + 1.5;
                            someAlgoY += someAlgoY > 0 ? radius - 1.5 : -radius + 1.5;

                            pebbles.Add(new(Math.Round(someAlgoX, 3), Math.Round(someAlgoY, 0)));
                        }
                        else
                        {
                            var recentPebbles = new List<Tuple<double, double>>();
                            if (pebble <= 7) //Get first (center) and last placed pebble
                            {
                                recentPebbles.Add(pebbles[0]);
                                recentPebbles.Add(pebbles.Last());
                            }
                            else
                                recentPebbles = pebbles.GetRange(pebble - 6, 2).ToList(); //Get 2 pebbles from first circle

                            var sin60 = Math.Sin(Math.PI / 3); // Approximately 0.866
                            var cos60 = Math.Cos(Math.PI / 3); // 0.5

                            var x1 = recentPebbles.First().Item1;
                            var x2 = recentPebbles.Last().Item1;
                            var y1 = recentPebbles.First().Item2;
                            var y2 = recentPebbles.Last().Item2;

                            // Possible point 1 (rotating P2 around P1 by 60 degrees)
                            var x3a = Math.Round(x1 + cos60 * (x2 - x1) - sin60 * (y2 - y1), 3);
                            var y3a = Math.Round(y1 + sin60 * (x2 - x1) + cos60 * (y2 - y1), 3);

                            // Possible point 2 (rotating P2 around P1 by -60 degrees)
                            var x3b = Math.Round(x1 + cos60 * (x2 - x1) + sin60 * (y2 - y1), 3);
                            var y3b = Math.Round(y1 - sin60 * (x2 - x1) + cos60 * (y2 - y1), 3);

                            if (pebbles.Select(x => x.Item1).Contains(x3a) || pebbles.Select(x => x.Item2).Contains(y3a))
                                pebbles.Add(new(x3b, y3b));
                            else
                                pebbles.Add(new(x3a, y3a));
                        }
                    }
                }

                foreach (var pebble in pebbles)
                {
                    double x = rect.Center.X + pebble.Item1;
                    double y = rect.Center.Y + pebble.Item2;
                    canvas.FillColor = Colors.White;
                    canvas.FillCircle((float)x, (float)y, (float)radius);
                    canvas.StrokeColor = Colors.Black;
                    canvas.StrokeSize = 2;
                    canvas.DrawCircle((float)x, (float)y, (float)radius);
                }
            }
            else
            {
                if (PebbleCount == 4)
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
            }

        }
    }
}
