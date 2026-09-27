using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Mancala.Models;

namespace Mancala.Staging
{
    internal class StagingGameDrawable : IDrawable
    {

        public static string value = "Test";
        public static bool hasGameBegun = false;

        public List<Pebble> pebbles = new List<Pebble>()
        {
            new(100, 150, 5),
            new(100, 160, 5),
            new(92, 155, 5),
            new(108, 155, 5)
        };

        public void Draw(ICanvas canvas, RectF rect)
        {
            // set background color
            canvas.FillColor = Colors.Coral;
            canvas.FillRectangle(rect);

            // draw game stores

            //set up the data for the stores
            float storeHeight = 100f;
            var GameStores = new List<Store>
            {
                new Store {X = 10, Y = 10, Width = rect.Width - 20, Height = storeHeight, CornerRadius = 25}, // top store
                new Store {X = 10, Y = rect.Height - storeHeight - 10, Width = rect.Width - 20, Height = storeHeight, CornerRadius = 25} // bottom store
            };


            // actually draw the the stores
            foreach (var store in GameStores)
            {
                canvas.FillColor = Colors.White;
                canvas.StrokeColor = Colors.Black;
                canvas.FillRoundedRectangle(store.X, store.Y, rect.Width - 20, store.Height, store.CornerRadius);

            }


            // pit logic
            float pitRadius = 40;
            var workingVerticalSpace = rect.Height - GameStores.Sum(s => s.Height) - 10 - 10; // 10 padding on top store and bottom
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


                canvas.FillColor = Colors.White;
                canvas.FillCircle(100, pitY, 40); // first pit drawn (top left) center point is at 100, 154, 40 = radius
                Console.WriteLine();
            }

            pitY = (verticalPointer + 110) / 2.0f;

            // right column of pits
            for (int i = 0; i < 6; i++) // six pits
            {
                if (i != 0)
                {
                    pitY += verticalSpacePerPitToWorkWith;
                }
                canvas.FillColor = Colors.White;
                canvas.FillCircle(250, pitY, 40);
                Console.WriteLine();
            }

            DrawDebugPitRects(canvas);
            DrawPebbles(canvas);


        }

        public void DrawDebugPitRects(ICanvas canvas)
        {
            //89.4 is the y delta

            // left pits
            canvas.DrawRectangle(60, 115, 80, 80);
            canvas.DrawRectangle(60, 205, 80, 80);
            canvas.DrawRectangle(60, 294, 80, 80);
            canvas.DrawRectangle(60, 384, 80, 80);
            canvas.DrawRectangle(60, 473, 80, 80);
            canvas.DrawRectangle(60, 563, 80, 80);

            // right pits
            canvas.DrawRectangle(210, 115, 80, 80);
            canvas.DrawRectangle(210, 205, 80, 80);
            canvas.DrawRectangle(210, 294, 80, 80);
            canvas.DrawRectangle(210, 384, 80, 80);
            canvas.DrawRectangle(210, 473, 80, 80);
            canvas.DrawRectangle(210, 563, 80, 80);

        }

        public void DrawPebbles(ICanvas canvas)
        {
            canvas.FillColor = Colors.Violet;
            foreach(var pebble in pebbles)
            {
                canvas.FillCircle(pebble.X, pebble.Y, pebble.Radius);
            }
        }

        public void DebugAnimate()
        {
            // I'm going to go down to the bottom pit. 
            // I need to take all pebbles, and change their Y value and redraw

            foreach(var pebblesToMove in pebbles)
            {
                if (pebblesToMove.Y > 300)
                    break;

                pebblesToMove.Y += 50;
            }
        }

        public void CheckIfPitIsHit(double x, double y)
        {
            Console.WriteLine("Figure out what pit was hit.");
        }


        public void DebugHitOnStore(double x, double y)
        {
            if ((x > 10 && x < 350) && (y > 10 && y < 110))
            {
                DebugAnimate();
            }
        }
    }
}
