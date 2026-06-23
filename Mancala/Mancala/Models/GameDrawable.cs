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

        public void Draw(ICanvas canvas, RectF rect)
        {
            // set background color
            canvas.FillColor = Colors.Tan;
            canvas.FillRectangle(rect);

            // stores logic
            float pitHeight = 100f;
            var GameStores = new List<Store>
            {
                new Store {X = 10, Y = 10, Width = rect.Width - 20, Height = pitHeight, CornerRadius = 25},
                new Store {X = 10, Y = rect.Height - pitHeight - 10, Width = rect.Width - 20, Height = pitHeight, CornerRadius = 25}
            };

            stores = GameStores;

            foreach(var store in GameStores)
            {
                canvas.FillColor = Colors.White;
                canvas.StrokeColor = Colors.Black;
                canvas.FillRoundedRectangle(store.X, store.Y, rect.Width - 20, store.Height, store.CornerRadius);
                
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
