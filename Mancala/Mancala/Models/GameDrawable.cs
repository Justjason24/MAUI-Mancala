using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Mancala.Models
{
    internal class GameDrawable : IDrawable
    {
        public List<Store> stores = new List<Store>();
        public List<Pit> pits = new List<Pit>();
        public static int[] LeftPitPebbleCount = [4, 7, 4, 2, 4, 4];
        public static int[] RightPitPebbleCount = [4, 4, 4, 4, 4, 4];

        public static string value = "Test";

        public void Draw(ICanvas canvas, RectF rect)
        {
            // set background color
            canvas.FillColor = Colors.Tan;
            canvas.FillRectangle(rect);

            pits.Clear(); // lol if I dont have this then I add 12 more pits each frame of the game and my computer blows up 

            // draw game stores

            //set up the data for the stores
            float storeHeight = 100f;
            var GameStores = new List<Store>
            {
                new Store {X = 10, Y = 10, Width = rect.Width - 20, Height = storeHeight, CornerRadius = 25}, // top store
                new Store {X = 10, Y = rect.Height - storeHeight - 10, Width = rect.Width - 20, Height = storeHeight, CornerRadius = 25} // bottom store
            };

            stores = GameStores;

            // actually draw them
            foreach (var store in GameStores)
            {
                canvas.FillColor = Colors.White;
                canvas.StrokeColor = Colors.Black;
                canvas.FillRoundedRectangle(store.X, store.Y, rect.Width - 20, store.Height, store.CornerRadius);

            }


            // pit logic
            float pitRadius = 40;
            var workingVerticalSpace = rect.Height - stores.Sum(s => s.Height) - 10 - 10; // 10 padding on top store and bottom
            var verticalSpacePerPitToWorkWith = workingVerticalSpace / 6; // 89.3
            var verticalPointer = 110 + verticalSpacePerPitToWorkWith;
            float pitY = (verticalPointer + 110) / 2.0f;

            // left column of pits
            // this is more than spaghetti code, this is a whole olive garden create your own pasta special
            for (int i = 0; i < 6; i++) // six pits
            {
                // we know where to place the first pit so just increment the next location by the verticalSpaceToworkwithperPit
                if (i != 0)
                {
                    pitY += verticalSpacePerPitToWorkWith;
                }

                pits.Add(new Pit { X = 100, Y = pitY, Radius = pitRadius, PebbleCount = LeftPitPebbleCount[i] });
                Console.WriteLine();
            }

            pitY = (verticalPointer + 110) / 2.0f;

            // right column of pits
            for (int i = 0; i < 6; i++) // six pits
            {
                // we know where to place the first pit so just increment the next location by the verticalSpaceToworkwithperPit
                if (i != 0)
                {
                    pitY += verticalSpacePerPitToWorkWith;
                }

                pits.Add(new Pit { X = 250, Y = pitY, Radius = pitRadius, PebbleCount = RightPitPebbleCount[i] });
                Console.WriteLine();
            }

            foreach (var pit in pits)
            {
                canvas.FillColor = Colors.White;
                canvas.FillCircle(pit.X, pit.Y, pit.Radius);
                DrawPebbles(canvas, pit);
            }


            // Pebble Logic


            Console.WriteLine();
        }

        public void CheckIfStoreHit(double x, double y)
        {
            foreach (var store in stores)
            {
                if (x >= store.X && x <= store.X + store.Width && y >= store.Y && y <= store.Y + store.Height)
                {

                    value = "New value";
                    LeftPitPebbleCount[5] = 1;
                    Console.WriteLine("Pit was clicked");
                }
            }
        }

        public void CheckIfPitIsHit(double x, double y)
        {
            Console.WriteLine("Figure out what pit was hit.");
        }

        public void DrawPebbles(ICanvas canvas, Pit pit)
        {
            if (pit.PebbleCount <= 0) return;

            float pebbleRadius = pit.Radius / 5f;
            float usableRadius = pit.Radius - pebbleRadius - 2f;
            var rng = new Random(pit.X.GetHashCode() ^ pit.Y.GetHashCode());

            List<(float x, float y)> positions = new();
            float spacing = pebbleRadius * 2.4f;

            // Shrink spacing until we have enough room for all pebbles
            while (positions.Count < pit.PebbleCount && spacing >= pebbleRadius * 1.1f)
            {
                positions.Clear();
                // Reset the RNG each attempt so jitter is consistent regardless of spacing used
                rng = new Random(pit.X.GetHashCode() ^ pit.Y.GetHashCode());

                for (float row = -usableRadius; row <= usableRadius; row += spacing)
                {
                    for (float col = -usableRadius; col <= usableRadius; col += spacing)
                    {
                        float xOffset = ((int)(row / spacing) % 2 != 0) ? spacing * 0.5f : 0f;
                        float cx = col + xOffset;
                        float cy = row;

                        cx += (float)(rng.NextDouble() - 0.5) * pebbleRadius * 0.8f;
                        cy += (float)(rng.NextDouble() - 0.5) * pebbleRadius * 0.8f;

                        float dist = MathF.Sqrt(cx * cx + cy * cy);
                        if (dist + pebbleRadius <= usableRadius)
                            positions.Add((pit.X + cx, pit.Y + cy));
                    }
                }

                spacing -= pebbleRadius * 0.1f; // tighten the grid and retry
            }

            // Draw all pebbles (or as many positions as we managed to generate)
            int toDraw = Math.Min(pit.PebbleCount, positions.Count);
            for (int i = 0; i < toDraw; i++)
            {
                var (px, py) = positions[i];

                canvas.FillColor = Color.FromArgb("#55000000");
                canvas.FillCircle(px + pebbleRadius * 0.3f, py + pebbleRadius * 0.3f, pebbleRadius);

                canvas.FillColor = Color.FromArgb("#C0956C");
                canvas.FillCircle(px, py, pebbleRadius);

                canvas.FillColor = Color.FromArgb("#AAFFFFFF");
                canvas.FillCircle(px - pebbleRadius * 0.3f, py - pebbleRadius * 0.3f, pebbleRadius * 0.4f);
            }
        }

        //public void DrawPebbles(ICanvas canvas, Pit pit)
        //{
        //    if(pit.PebbleCount == 4)
        //    {
                
        //    }
        //}
    }
}
