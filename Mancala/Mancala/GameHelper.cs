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

        public static List<Mancala.Staging.Pit> GetLeftOrRightPits(List<Mancala.Staging.Pit> pits, string currentPlayer)
        {
            if (currentPlayer.Trim() != "Left" && currentPlayer.Trim() != "Right")
                throw new Exception("Current ");

            var pitsToReturn = new List<Mancala.Staging.Pit>();

            // All pits on the leftside are named "Pit0X" and the right is "Pit1X"
            // Will look through pit names and if the har at position 4 is a '0' we know it's left and vice versa;

            char leftOrRightChar;

            if (currentPlayer.Trim() == "Left")
                leftOrRightChar = '0';
            else
                leftOrRightChar = '1';

            foreach(var pit in pits)
            {
                if (pit.Name.ToCharArray().ElementAt(3) == leftOrRightChar)
                    pitsToReturn.Add(pit);
            }

            return pitsToReturn.OrderBy(x => x.Name).ToList();
        }

        public static string CheckForOppositeCapture(GameState gameState, string PitToCheck)
        {
            if (PitToCheck.Contains("Store"))
                return "";

            var pit = gameState.Pits.Where(x => x.Name == PitToCheck).First();

            if(pit.Pebbles.Count == 0)
            {
                char oppositePitChar = '\0';

                if (pit.Name.ElementAt(3) == '0')
                    oppositePitChar = '1';

                else
                    oppositePitChar = '0';

                var charArray = pit.Name.ToCharArray();
                charArray[3] = oppositePitChar;

                return new string(charArray);
                 
            }

            return "";
        }
    }
}
