
using Microsoft.Maui.Controls.Shapes;

namespace Mancala;

public partial class MancalaPage : ContentPage
{
	public MancalaPage()
	{
        InitializeComponent();
    }

    private void OnPitTapped(object sender, EventArgs e)
    {
        if(sender is GraphicsView tappedPit && tappedPit.Equals(Pit51))
        {
            if(tappedPit.Drawable is PitDrawable pitDrawable)
            {
                int pebblesToMove = pitDrawable.PebbleCount; // so moving 4 to start

                if(pebblesToMove > 0)
                {
                    pitDrawable.PebbleCount = 0;
                    tappedPit.Invalidate(); // Do I move this around with the line above?
                    Console.WriteLine("Pit 51 clicked");
                    DistributePebbles(tappedPit, pebblesToMove);
                }
            }
        }


    }

    private void DistributePebbles(GraphicsView startingPit, int pebblesToMove)
    {
        DisplayAlert("Alert", "Pit 51 tapped!", "OK");
    }

}