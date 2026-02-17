using Mancala.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{
    public class MancalaHelper
    {
        public static Grid GetParentGrid(View view)
        {
            Element parent = view.Parent;

            while (parent != null && parent is not Grid)
                parent = parent.Parent;

            return parent as Grid;
        }

        //public static IEnumerable<(View View, string Id, int Row, int Col)> GetGridChildrenWithPositions(Grid grid)
        //{
        //    foreach (var child in grid.Children)
        //    {
        //        if (child is View view)
        //        {
        //            int row = Grid.GetRow(view);
        //            int col = Grid.GetColumn(view);
        //            string id = view.StyleId; // This is the XAML x:Name

        //            yield return (view, id, row, col);
        //        }
        //    }
        //}

        public static IEnumerable<PebbleMapping> GetGridChildrenWithPositions(Grid grid)
        {
            foreach (var child in grid.Children)
            {
                if (child is View view)
                {
                    int row = Grid.GetRow(view);
                    int col = Grid.GetColumn(view);
                    string id = view.StyleId; // This is the XAML x:Name

                    yield return new PebbleMapping(id, row, col);
                }
            }
        }

    }
}
