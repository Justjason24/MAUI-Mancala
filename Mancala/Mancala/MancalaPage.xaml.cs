
using Microsoft.Maui.Controls.Shapes;

namespace Mancala;

public partial class MancalaPage : ContentPage
{
	public MancalaPage()
	{
        InitializeComponent();
    }


    private async void Button_Clicked(object sender, EventArgs e)
    {
        // Navigate to the specified URL in the system browser.
        await Launcher.Default.OpenAsync("https://espn.com");
    }

    private async void Pit51_Tapped(object sender, EventArgs e)
    {
        // Move the pebble from Pit51 (row 5, col 1) to Pit41 (row 4, col 1)
        Pebble.TranslationX = 0;
        Pebble.TranslationY = 0;

        // Animate upward by one row (approx 70px depending on spacing)
        await Pebble.TranslateTo(0, -70, 400, Easing.CubicInOut);

        // Snap pebble into Pit41 grid cell
        Grid.SetRow(Pebble, 4);
        Grid.SetColumn(Pebble, 1);

        // Reset translation after moving grid cell
        Pebble.TranslationX = 0;
        Pebble.TranslationY = 0;
    }

    private async void TestMethod_Tapped(object sender, EventArgs e)
    {
        //var board = new int[14];

        //var idk = (View)sender;
        //var mancalaBoard = (Layout)idk.Parent; // YES We have the children objects in this variable! 

        //var pieces = mancalaBoard.Children.ToList();
        //var piece = (Shape)pieces.FirstOrDefault().id

        if(sender is Grid grid)
        {
            Console.WriteLine();
        }

        //if (sender is View view)
        //{
        //    var mancalaBoardGrid = MancalaHelper.GetParentGrid(view);

        //    foreach (var child in mancalaBoardGrid.Children)
        //    {
        //        int row = Grid.GetRow((View)child);
        //        int col = Grid.GetColumn((View)child);
        //        string name = (View)child.

        //        Console.WriteLine();
        //    }


        //}

        if (sender is View view)
        {
            var mancalaBoardGrid = MancalaHelper.GetParentGrid(view);

            var children = MancalaHelper.GetGridChildrenWithPositions(mancalaBoardGrid);
            var listedChildren = children.ToList();

            foreach (var child in children)
            {
                Console.WriteLine();
            }

        }

        Console.WriteLine();
        Block.TranslationX = 0;
        Block.TranslationY = 0;

        await Block.TranslateTo(0, 70, 400, Easing.BounceOut);

        Grid.SetRow(Block, 1);
        Grid.SetColumn(Block, 0);

        Block.TranslationX = 0;
        Block.TranslationY = 0;

    }


}