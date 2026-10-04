using Mancala.Models;
using Mancala.Staging;
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

        public static List<Models.Pebble> CreateCopyOfPebblesFromSelectedPit(List<Mancala.Staging.Pit> currentPits, string currentMove)
        {
            var pebblesToRemoveFromSelectedPit = new List<Pebble>();

            var pitToRemoveFrom = currentPits.Where(x => x.Name == currentMove).First();

            foreach (var pebble in pitToRemoveFrom.Pebbles)
            {
                pebblesToRemoveFromSelectedPit.Add(pebble);
            }

            return pebblesToRemoveFromSelectedPit;
        }
    }
}
