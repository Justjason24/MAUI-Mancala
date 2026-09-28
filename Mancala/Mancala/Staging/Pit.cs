using Mancala.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala.Staging
{
    internal class Pit
    {
        public float X;
        public float Y;
        public float SideLength = 80;
        public int PebbleCount;
        public string Name = "";

        public List<Pebble> Pebbles = new List<Pebble>();

        public Pit(float x, float y, string name)
        {
            this.X = x;
            this.Y = y;
            this.Name = name;
        }
    }
}
