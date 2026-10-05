using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala.Models
{
    public class Store
    {
        public string Name;
        public float X;
        public float Y;
        public float Width;
        public float Height = 100;
        public List<Pebble> Pebbles = new List<Pebble>();

        public Store(float x, float y, float width, string name)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Name = name;
        }
    }
}
