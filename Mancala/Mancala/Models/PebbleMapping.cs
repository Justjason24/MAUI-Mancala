using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala.Models
{
    public class PebbleMapping
    {
        string Name;

        int Row;

        int Column;

        public PebbleMapping(string name, int row, int col)
        {
            this.Name = name;
            this.Row = row;
            this.Column = col;
        }
    }
}
