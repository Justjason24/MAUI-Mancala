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
        public int[] GameBoard = new int[] {0, 4, 4, 4, 4, 4, 4, 0, 4, 4, 4, 4, 4, 4};

        public Dictionary<string, int> GameBoardDictionary = new Dictionary<string, int>();
        public string CurrentMove = "";

        public GameState()
        {
            GameBoardDictionary.Add("TopRightStore", 0);
            GameBoardDictionary.Add("Pit00", 1);
            GameBoardDictionary.Add("Pit01", 2);
            GameBoardDictionary.Add("Pit02", 3);
            GameBoardDictionary.Add("Pit03", 4);
            GameBoardDictionary.Add("Pit04", 5);
            GameBoardDictionary.Add("Pit05", 6);
            GameBoardDictionary.Add("BottomLeft", 7);
            GameBoardDictionary.Add("Pit15", 8);
            GameBoardDictionary.Add("Pit14", 9);
            GameBoardDictionary.Add("Pit13", 10);
            GameBoardDictionary.Add("Pit12", 11);
            GameBoardDictionary.Add("Pit11", 12);
            GameBoardDictionary.Add("Pit10", 13);
        }

        public void ConvertPitClickedToMove(string pitClicked)
        {
            string pitNumberString = pitClicked.Replace("Pit", "");

            // these shouldn't be exceptions in the future. I dont want the game to crash if there was a misclick or accident
            if (pitNumberString.StartsWith("0") && this.CurrentPlayer == "Right")
                throw new Exception("Invalid Move");

            if (pitNumberString.StartsWith("1") && this.CurrentPlayer == "Left")
                throw new Exception("Invalid Move");

            this.CurrentMove = pitClicked;
        }

        public void Update()
        {
            int pitClicked = Convert.ToInt32(Char.GetNumericValue(CurrentMove.Last()));

            int arrayIndexToStart = GameBoardDictionary[CurrentMove];

            int numPebblesToMove = GameBoard[arrayIndexToStart];

            GameBoard[arrayIndexToStart] = 0; // clear the pebbles from selected pit

            for (int i = 0; i < numPebblesToMove; i++)
            {
                arrayIndexToStart++;

                if (arrayIndexToStart > 13)
                    arrayIndexToStart = 0; 

                GameBoard[arrayIndexToStart]++;
            }

            if (CurrentPlayer == "Left")
                CurrentPlayer = "Right";
            else
                CurrentPlayer = "Left";
        }
    }
}
