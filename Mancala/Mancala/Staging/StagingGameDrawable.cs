using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
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
            new(100, 150, 5, 100, 150, 1),
            new(100, 160, 5, 100, 160, 2),
            new(92, 155, 5, 92, 155, 3),
            new(108, 155, 5, 108, 155, 4)
        };

        public List<Staging.Pit> pits = new List<Staging.Pit>()
        {
            new(60, 115, "Pit00"),
            new(60, 205, "Pit01"),
            new(60, 294, "Pit02"),
            new(60, 384, "Pit03"),
            new(60, 473, "Pit04"),
            new(60, 563, "Pit05"),

            new(210, 115, "Pit10"),
            new(210, 205, "Pit11"),
            new(210, 294, "Pit12"),
            new(210, 384, "Pit13"),
            new(210, 473, "Pit14"),
            new(210, 563, "Pit15")
        };

        public void Draw(ICanvas canvas, RectF rect)
        {
            // set up pit pebble relationship
            if(!hasGameBegun)
            {
                SetPebbletPitRelationship();
                hasGameBegun = true;
            }

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
            SetPebbleCoordsNextTick();
            DrawPebbles(canvas);


        }

        public void SetPebbleCoordsNextTick()
        {
            foreach (var pebble in pebbles)
            {

                if (pebble.X != pebble.DestinationX)
                {
                    pebble.X += 5;
                }

                if(pebble.Y != pebble.DestinationY)
                {
                    pebble.Y += 5;
                }
            }
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

        public void DebugSetPebbleDestination()
        {
            // for right now I'm saying move all the pebbles down a bit.
            foreach (var pebblesToMove in pebbles)
            {
                pebblesToMove.DestinationY += 100;
            }
        }


        public void DebugHitOnStore(double x, double y)
        {
            if ((x > 10 && x < 350) && (y > 10 && y < 110))
            {
                //DebugSetPebbleDestination(); // this works - nothing more than animation test. 
                DebugAnimatePebblesToPit(pebbles.Where(x => x.ID < 5).ToList() ,"Pit01");
            }
        }

        public void SetPebbletPitRelationship()
        {
            
            int counter = 0;

            foreach(var pit in pits)
            {

                for(int i = 1; i <= 4; i++)
                {

                    // gotta figure out how to count up
                    int pebbleIdToGet = counter + i;
                    var pebble = pebbles.Where(x => x.ID == pebbleIdToGet).FirstOrDefault();
                    pit.Pebbles.Add(pebble);
                }

                counter += 4; // because each pit starts with 4 pebbles
            }
        }

        public void DebugAnimatePebblesToPit(List<Pebble> pebbles, string desiredPitName)
        {
            // TODO: Working on this
            // The output of this should be to set each pebble's desired X and Y.
            var destinationPit = pits.Where(x => x.Name.Trim() == desiredPitName).FirstOrDefault();

            if (destinationPit == null)
                throw new Exception("Could not find pit");


            var allPebblesToMove = new List<Pebble>();

            var pebblesAlreadyInPit = destinationPit.Pebbles.Where(x => x.ID != null).ToList();

            allPebblesToMove.AddRange(pebbles);
            allPebblesToMove.AddRange(pebblesAlreadyInPit);



            if (allPebblesToMove.Count == 0)
                throw new Exception("idk how this happened");

            if(allPebblesToMove.Count == 1)
                Console.WriteLine("todo");




        }

        public Tuple<double, double> CalculateMidPointFromPitBounds(Staging.Pit pit)
        {
            double x_midpoint = (pit.X + pit.SideLength) - pit.X;
            double y_midpoint = (pit.Y + pit.SideLength) - pit.Y;

            return new Tuple<double, double>(x_midpoint, y_midpoint);
        }
    }
}
