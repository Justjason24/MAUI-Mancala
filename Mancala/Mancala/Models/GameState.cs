using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala.Models
{
    public class GameState
    {
        // this is correct. left side gets bottom pit because it's counter-clockwise

        public string CurrentPlayer = "Left";
        public int[] LeftBottomPlayerPits = new int[] {4, 4, 4, 4, 4, 4};
        public int[] RightTopPlayerPits = new int[] { 4, 4, 4, 4, 4, 4 };
        public string CurrentMove = "";

        public void ConvertPitClickedToMove(string pitClicked)
        {
            pitClicked = pitClicked.Replace("Pit", "");

            // these shouldn't be exceptions in the future. I dont want the game to crash if there was a misclick or accident
            if (pitClicked.StartsWith("0") && this.CurrentPlayer == "Right")
                throw new Exception("Invalid Move");

            if (pitClicked.StartsWith("1") && this.CurrentPlayer == "Left")
                throw new Exception("Invalid Move");

            this.CurrentMove = pitClicked;
        }

        public void Update()
        {
            int pitClicked = Convert.ToInt32(CurrentMove.ToCharArray().Last());


        }
    }
}
