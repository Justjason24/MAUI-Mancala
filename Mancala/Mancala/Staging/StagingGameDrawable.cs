//using Android.Content.Res;
// using AndroidX.Activity;
using Mancala.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Mancala.Staging
{
    internal class StagingGameDrawable : IDrawable
    {
        public int debugCounter = 1;
        public GameState GameState = new GameState();

        public void Draw(ICanvas canvas, RectF rect)
        {
            DrawBackground(canvas, rect);

            DrawGameStores(canvas, rect);

            DrawDebugRectsAroundStore(canvas, rect);

            // pit logic
            float pitRadius = 40;
            var workingVerticalSpace = rect.Height - this.GameState.Stores.Sum(s => s.Height) - 10 - 10; // 10 padding on top store and bottom
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

            //DrawDebugPitRects(canvas);
            SetPebbleCoordsNextTick();
            DrawPebbles(canvas);


        }

        public void SetPebbleCoordsNextTick()
        {
            foreach (var pebble in GameState.Pebbles)
            {

                if (pebble.X != pebble.DestinationX)
                {
                    Console.WriteLine("debug");

                    if (pebble.X < pebble.DestinationX)
                    {
                        if (Math.Abs(pebble.X - pebble.DestinationX) > 5)
                            pebble.X += 5;

                        else
                            pebble.X++;
                    }
                    
                    else
                    {
                        if (Math.Abs(pebble.X - pebble.DestinationX) > 5)
                            pebble.X -= 5;
                        else
                            pebble.X--;
                    }
                }

                if(pebble.Y != pebble.DestinationY)
                {
                    Console.WriteLine("debug");


                    if (pebble.Y < pebble.DestinationY)
                    {
                        if (Math.Abs(pebble.Y - pebble.DestinationY) > 5)
                            pebble.Y += 5;

                        else
                            pebble.Y++;
                    }

                    else
                    {
                        if (Math.Abs(pebble.Y - pebble.DestinationY) > 5)
                            pebble.Y -= 5;
                        else
                            pebble.Y--;
                    }
                        
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

            canvas.StrokeColor = Colors.Maroon;
            canvas.DrawRectangle(90, 145, 20, 20); 

            canvas.DrawRectangle(90, 235, 20, 20); // pebblesBoundX, pebblesBoundY, pebblesBoundSideLength x 2

            // start in top left and go gown {pebblesBounds} then move to top right
            //canvas.FillColor = Colors.CornflowerBlue;
            //canvas.FillCircle(95, 240, 5);
            //canvas.FillCircle(95, 250, 5);
            //canvas.FillCircle(105, 240, 5);
            //canvas.FillCircle(105, 250, 5);

        }

        public void DrawPebbles(ICanvas canvas)
        {
            canvas.FillColor = Colors.Violet;
            foreach(var pebble in GameState.Pebbles)
            {
                canvas.FillCircle(pebble.X, pebble.Y, pebble.Radius);
            }
        }

        public void DebugHitOnStore(double x, double y)
        {
            if ((x > 10 && x < 350) && (y > 10 && y < 110))
            {
                GameState.debugMessage = "CHANGED AGAIN";
                //DebugSetPebbleDestination(); // this works - nothing more than animation test. 
                if (debugCounter == 1)
                {
                    //var testPebblesToMove = GameState.Pebbles.Where(x => x.ID < 3).ToList();

                    //foreach (var pebble in testPebblesToMove)
                    //{
                    //    DebugAnimatePebblesToPit(pebble, "Pit04");

                    //}

                    var testPebbleToMove = GameState.Pebbles.Where(x => x.ID > 24 && x.ID <= 30).ToList();

                    foreach (var pebble in testPebbleToMove)
                    {
                        AnimatePebbleToStore(pebble, "TopRightStore");

                    }

                    //AnimatePebbleToStore(testPebbleToMove, "TopRightStore");
                    debugCounter++;

                }
                else if(debugCounter == 2)
                {
                    var testPebbleToMove = GameState.Pebbles.Where(x => x.ID < 8).ToList();
                    //AnimatePebbleToStore(testPebbleToMove, "BottomLeftStore");
                    //var testPebblesToMove = GameState.Pebbles.Where(x => x.ID > 45).ToList();

                    foreach (var pebble in testPebbleToMove)
                    {
                        AnimatePebbleToStore(pebble, "BottomLeftStore");

                    }
                    debugCounter++;
                }
                else if (debugCounter == 3)
                {
                    var testPebblesToMove = GameState.Pebbles.Where(x => x.ID >= 41 && x.ID <= 44).ToList();

                    foreach (var pebble in testPebblesToMove)
                    {
                        DebugAnimatePebblesToPit(pebble, "Pit04");

                    }

                    debugCounter++;
                }
            }
        }

        public void DebugAnimatePebblesToPit(Pebble pebbleToMove, string desiredPitName)
        {
            // The output of this should be to set each pebble's desired X and Y.
            var destinationPit = GameState.Pits.Where(x => x.Name.Trim() == desiredPitName).FirstOrDefault();

            if (destinationPit == null)
                throw new Exception("Could not find pit");


            var allPebblesToMove = new List<Pebble>();

            var pebblesAlreadyInPit = destinationPit.Pebbles?.Where(x => x != null).ToList();

            allPebblesToMove.AddRange(pebbleToMove);
            allPebblesToMove.AddRange(pebblesAlreadyInPit);



            if (allPebblesToMove.Count == 0)
                throw new Exception("idk how this happened");

            // determine how many pebbles need to fit in a 'square' within the pit bounds.
            var pebblesBounds = Math.Ceiling(Math.Sqrt(allPebblesToMove.Count()));
            var pebblesBoundsSideLength = pebblesBounds * 10.00; // 5 because that's the pebble's radius so the dimater (width) is 10

            var distanceOnEachSide = (destinationPit.SideLength - pebblesBoundsSideLength) / 2;

            var pebblesBoundX = destinationPit.X + distanceOnEachSide;
            var pebblesBoundY = destinationPit.Y + distanceOnEachSide;


            var firstPebbleX = (float)(pebblesBoundX + 5.00); //95
            var firstPebbleY = (float)(pebblesBoundY + 5.00); //240

            int pebblesPlaced = 0;
            int x_scale = 0;
            int y_scale = 10;

            foreach(var pebble in allPebblesToMove)
            {
                if(pebblesPlaced == 0)
                {
                    pebble.DestinationX = firstPebbleX;
                    pebble.DestinationY = firstPebbleY;
                    pebblesPlaced++;
                    continue;
                }

                if(pebblesPlaced % pebblesBounds == 0)
                {
                    x_scale += 10;
                    y_scale = 0;
                }

                pebble.DestinationX = firstPebbleX + x_scale;
                pebble.DestinationY = firstPebbleY + y_scale;
                y_scale += 10;
                pebblesPlaced++;

            }

            // TODO . This below needs to be moved.
            destinationPit.Pebbles.Add(pebbleToMove);

            Console.WriteLine();


        }

        public void AnimatePebbleToStore(Pebble pebbleToMove, string desiredStoreName)
        {
            // The output of this should be to set each pebble's desired X and Y.
            var destinationStore = GameState.Stores.Where(x => x.Name.Trim() == desiredStoreName).FirstOrDefault();

            if (destinationStore == null)
                throw new Exception("Could not find pit");

            var allPebblesToMove = new List<Pebble>();

            var pebblesAlreadyInStore = destinationStore.Pebbles.Where(x => x != null).ToList();

            allPebblesToMove.AddRange(pebbleToMove);
            allPebblesToMove.AddRange(pebblesAlreadyInStore);

            if (allPebblesToMove.Count == 0)
                throw new Exception("idk how this happened");


            Console.WriteLine("Let some maths begin");
            // Remember, a pebble's radius is 5 (diamter is 10)
            // Convert Destination X, and Y to ints so there are no decimals.

            int x_scale = 10;
            int startingX = 0;
            int pebblesPlaced = 0;

            foreach (var pebble in allPebblesToMove)
            {
                if(pebblesPlaced == 0)
                {
                    startingX = Convert.ToInt32((destinationStore.Width / 2) + destinationStore.X);
                    pebble.DestinationX = startingX;
                    pebble.DestinationY = Convert.ToInt32((destinationStore.Height / 2) + destinationStore.Y);
                    pebblesPlaced++;
                    continue;
                }

                // 1 % 2 = 1 
                // 2 % 2 = 0
                // 3 % 2 = 1
                if(pebblesPlaced % 2 == 1)
                {
                    pebble.DestinationX = startingX + x_scale;
                    pebble.DestinationY = Convert.ToInt32((destinationStore.Height / 2) + destinationStore.Y);
                    pebblesPlaced++;
                }
                else if(pebblesPlaced % 2 == 0)
                {
                    pebble.DestinationX = startingX - x_scale;
                    pebble.DestinationY = Convert.ToInt32((destinationStore.Height / 2) + destinationStore.Y);
                    x_scale += 10;
                    pebblesPlaced++;
                }

            }

            destinationStore.Pebbles.Add(pebbleToMove);

            Console.WriteLine("Did I do it yet??");
        }

        public void UpdateUI()
        {
            if (this.GameState.PlacesToUpdate.Count != this.GameState.PebblesToMove.Count)
                throw new Exception("Moving more pebbles than pits or vice versa");


            for(int i = 0; i < this.GameState.PebblesToMove.Count; i++)
            {
                var pebble = this.GameState.PebblesToMove[i];
                string placeToUpdate = GameState.PlacesToUpdate.ElementAt(i);

                if(placeToUpdate.Contains("Pit"))
                    DebugAnimatePebblesToPit(pebble, placeToUpdate);


                else
                    AnimatePebbleToStore(pebble, placeToUpdate);

            }

        }

        public void DrawBackground(ICanvas canvas, RectF rect)
        {
            canvas.FillColor = Colors.Coral;
            canvas.FillRectangle(rect);
        }

        public void DrawGameStores(ICanvas canvas, RectF rect)
        {
            foreach (var store in this.GameState.Stores)
            {
                canvas.FillColor = Colors.White;
                canvas.StrokeColor = Colors.Black;
                canvas.FillRoundedRectangle(store.X, store.Y, rect.Width - 20, store.Height, 25);

            }
        }

        public void DrawDebugRectsAroundStore(ICanvas canvas, RectF dirtyRect)
        {
            canvas.StrokeColor = Colors.MediumPurple;
            canvas.FillColor = Colors.MediumOrchid;
            canvas.DrawRectangle(10, 10, 355.2F, 100);
            canvas.DrawRectangle(10, 648.8F, 355.2F, 100);
        }
    }
}
