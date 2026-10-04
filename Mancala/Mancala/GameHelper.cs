using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{
    public class GameHelper
    {

        public static string DeterminePitHit(Models.GameState gameState, double x, double y)
        {
            foreach (var pit in gameState.Pits)
            {
                if ((x > pit.X && x < pit.X + pit.SideLength) && (y > pit.Y && y < pit.Y + pit.SideLength))
                    return pit.Name;
            }

            return "";
        }
    }
}
