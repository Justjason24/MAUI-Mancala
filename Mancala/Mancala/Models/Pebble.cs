using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala.Models
{
    public class Pebble
    {
        public float X;
        public float Y;
        public float Radius;

        public float DestinationX;
        public float DestinationY;

        public int ID;

        public Pebble()
        {
                
        }

        public Pebble(float x, float y, float r)
        {
            this.X = x;
            this.Y = y;
            this.Radius = r;
        }

        public Pebble(float x, float y, float r, float destinationX, float destinationY, int id)
        {
            this.X = x;
            this.Y = y;
            this.Radius = r;

            this.DestinationX = destinationX;
            this.DestinationY = destinationY;

            this.ID = id;
        }
    }

    
}
