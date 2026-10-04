using Mancala.Staging;
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

        public Dictionary<string, int> GameBoardDictionary = new Dictionary<string, int>()
        {
            ["TopRightStore"] = 0,
            ["Pit00"] = 1,
            ["Pit01"] = 2,
            ["Pit02"] = 3,
            ["Pit03"] = 4,
            ["Pit04"] = 5,
            ["Pit05"] = 6,
            ["BottomLeft"] = 7,
            ["Pit15"] = 8,
            ["Pit14"] = 9,
            ["Pit13"] = 10,
            ["Pit12"] = 11,
            ["Pit11"] = 12,
            ["Pit10"] = 13,
        };
        public string CurrentMove = "";

        //TODO: Make it a stack, not a dictionary
        public Dictionary<string, int> PitsToUpdate = new Dictionary<string, int>();

        public string debugMessage = "starting string";

        public List<Pebble> Pebbles = new List<Pebble>()
        {
            new(95, 150, 5, 95, 150, 1),
            new(95, 160, 5, 95, 160, 2),
            new(105, 150, 5, 105, 150, 3),
            new(105, 160, 5, 105, 160, 4),

            new(95, 240, 5, 95, 240, 5),
            new(95, 250, 5, 95, 250, 6),
            new(105, 240, 5, 105, 240, 7),
            new(105, 250, 5, 105, 250, 8),

            new(95, 329, 5, 95, 329, 9),
            new(95, 339, 5, 95, 339, 10),
            new(105, 329, 5, 105, 329, 11),
            new(105, 339, 5, 105, 339, 12),

            new(95, 419, 5, 95, 419, 13),
            new(95, 429, 5, 95, 429, 14),
            new(105, 419, 5, 105, 419, 15),
            new(105, 429, 5, 105, 429, 16),

            new(95, 508, 5, 95, 508, 17),
            new(95, 518, 5, 95, 518, 18),
            new(105, 508, 5, 105, 508, 19),
            new(105, 518, 5, 105, 518, 20),

            new(95, 598, 5, 95, 598, 21),
            new(95, 608, 5, 95, 608, 22),
            new(105, 598, 5, 105, 598, 23),
            new(105, 608, 5, 105, 608, 24),


            new(245, 150, 5, 245, 150, 25),
            new(245, 160, 5, 245, 160, 26),
            new(255, 150, 5, 255, 150, 27),
            new(255, 160, 5, 255, 160, 28),

            new(245, 240, 5, 245, 240, 29),
            new(245, 250, 5, 245, 250, 30),
            new(255, 240, 5, 255, 240, 31),
            new(255, 250, 5, 255, 250, 32),

            new(245, 329, 5, 245, 329, 33),
            new(245, 339, 5, 245, 339, 34),
            new(255, 329, 5, 255, 329, 35),
            new(255, 339, 5, 255, 339, 36),

            new(245, 419, 5, 245, 419, 37),
            new(245, 429, 5, 245, 429, 38),
            new(255, 419, 5, 255, 419, 39),
            new(255, 429, 5, 255, 429, 40),

            new(245, 508, 5, 245, 508, 41),
            new(245, 518, 5, 245, 518, 42),
            new(255, 508, 5, 255, 508, 43),
            new(255, 518, 5, 255, 518, 44),

            new(245, 598, 5, 245, 598, 45),
            new(245, 608, 5, 245, 608, 46),
            new(255, 598, 5, 255, 598, 47),
            new(255, 608, 5, 255, 608, 48)
        };

        public List<Staging.Pit> Pits = new List<Staging.Pit>()
        {
            new(60, 115, "Pit00"),
            new(60, 205, "Pit01"),
            new(60, 294, "Pit02"),
            new(60, 384, "Pit03"),
            new(60, 473, "Pit04"),
            new(60, 563, "Pit05"),

            new(210, 115, "Pit10"),
            new(210, 205, "Pit11"),
            new(210, 294, "Pit12"),
            new(210, 384, "Pit13"),
            new(210, 473, "Pit14"),
            new(210, 563, "Pit15")
        };

        public List<Pebble> PebblesToMove = new List<Pebble>();


        public GameState()
        {
            SetPebbletPitRelationship();
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

        /// <summary>
        ///     The goal of this method is the following:
        /// 
        ///         - Update the class property GameBoard[] (
        ///         - Update the PitsToUpdate. Each one will be getting one more pebble
        ///         - Set next players turn
        ///         - Obtain List<Pebbles> that belong to selectedPit
        /// </summary>
        public void Update()
        {
            

            PitsToUpdate.Clear();
            PebblesToMove.Clear();

            int arrayIndexToStart = GameBoardDictionary[CurrentMove];

            int numPebblesToMove = GameBoard[arrayIndexToStart];

            GameBoard[arrayIndexToStart] = 0; // clear the pebbles from selected pit

            for (int i = 0; i < numPebblesToMove; i++)
            {
                arrayIndexToStart++;

                if (arrayIndexToStart > 13)
                    arrayIndexToStart = 0; 

                GameBoard[arrayIndexToStart]++;

                PitsToUpdate.Add(GameBoardDictionary.Where(x => x.Value == arrayIndexToStart).First().Key, 1);
            }

            if (CurrentPlayer == "Left")
                CurrentPlayer = "Right";
            else
                CurrentPlayer = "Left";


            this.PebblesToMove = GameHelper.CreateCopyOfPebbles(this.Pits, this.CurrentMove);



        }

        public void SetPebbletPitRelationship()
        {

            int counter = 0;

            foreach (var pit in Pits)
            {

                for (int i = 1; i <= 4; i++)
                {

                    // gotta figure out how to count up
                    int pebbleIdToGet = counter + i;
                    var pebble = Pebbles.Where(x => x.ID == pebbleIdToGet).FirstOrDefault();
                    pit.Pebbles.Add(pebble);
                }

                counter += 4; // because each pit starts with 4 pebbles
            }
        }
    }
}
