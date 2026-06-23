using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala.Models
{
    internal class GameDrawable : IDrawable
    {
        public List<Store> stores = new List<Store>();
        public List<Pit> pits = new List<Pit>();

        public void Draw(ICanvas canvas, RectF rect)
        {
            // set background color
            canvas.FillColor = Colors.Tan;
            canvas.FillRectangle(rect);



            // stores logic
            float storeHeight = 100f;
            var GameStores = new List<Store>
            {
                new Store {X = 10, Y = 10, Width = rect.Width - 20, Height = storeHeight, CornerRadius = 25},
                new Store {X = 10, Y = rect.Height - storeHeight - 10, Width = rect.Width - 20, Height = storeHeight, CornerRadius = 25}
            };

            stores = GameStores;

            foreach(var store in GameStores)
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

            // this is more than spaghetti code, this is a whole olive garden create your own pasta special
            for (int i = 0; i < 6; i++) // six pits
            {     
                // we know where to place the first pit so just increment the next location by the verticalSpaceToworkwithperPit
                if(i != 0)
                {
                    pitY += verticalSpacePerPitToWorkWith;
                }

                pits.Add(new Pit { X = 100, Y = pitY, Radius = pitRadius });
                Console.WriteLine();
            }

            

            foreach(var pit in pits)
            {
                canvas.FillColor = Colors.White;
                canvas.FillCircle(pit.X, pit.Y, pit.Radius);
            }


            Console.WriteLine();
        }

        public void CheckIfStoreHit(double x, double y)
        {
            foreach (var store in stores)
            {
                if (x >= store.X && x <= store.X + store.Width && y >= store.Y && y <= store.Y + store.Height)
                {
                    Console.WriteLine("Pit was clicked");
                }
            }
        }
    }
}
