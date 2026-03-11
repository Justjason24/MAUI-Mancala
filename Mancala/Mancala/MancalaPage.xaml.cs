
using Microsoft.Maui.Controls.Shapes;

namespace Mancala;

public partial class MancalaPage : ContentPage
{
    private List<GraphicsView> _allPits;
    private int _currentPitIndex; // Keep track of the current pit's index for distribution
    private Dictionary<GraphicsView, Rect> _pitLayoutBounds = new Dictionary<GraphicsView, Rect>();
    public MancalaPage()
    {
        InitializeComponent();
        _allPits = new List<GraphicsView>
        {
            Pit00, Pit10, Pit20, Pit30, Pit40, Pit50,
            Pit51, Pit41, Pit31, Pit21, Pit11, Pit01
        };
        this.SizeChanged += (s, e) => CalculatePitPositions();

    }

    private async void OnPitTapped(object sender, TappedEventArgs e)
    {
        // We're specifically interested in the pit that was tapped, which is 'sender'
        if (sender is GraphicsView tappedPit)
        {

            // Pit 51 index is 6 as of now (bottom right pit)
            _currentPitIndex = _allPits.IndexOf(tappedPit);

            if (tappedPit.Drawable is PitDrawable pitDrawable)
            {
                int pebblesToMove = pitDrawable.PebbleCount;
                if (pebblesToMove > 0)
                {
                    await DistributePebbles(pebblesToMove);
                    // Clear the pebbles from the tapped pit
                    pitDrawable.PebbleCount = 0;
                    tappedPit.Invalidate(); 


                }
                else
                {
                    Console.WriteLine($"Pit {_currentPitIndex} has no pebbles to move.");
                    // TODO something here later
                }
            }
        }
    }
    // Calculates the absolute position of each pit relative to the MancalaBoardLayout grid
    private void CalculatePitPositions()
    {
        if (_allPits[0].Width <= 0) return;

        _pitLayoutBounds.Clear();
        // Get PitsGrid's position relative to MancalaBoardLayout
        Rect pitsGridBounds = PitsGrid.Bounds; // Bounds of PitsGrid relative to MancalaBoardLayout
        foreach (var pit in _allPits)
        {

            Rect pitBoundsInPitsGrid = pit.Bounds;

            // Thanks internet for this
            Rect pitAbsoluteBoundsInMancalaBoardLayout = new Rect(
                pitsGridBounds.X + pitBoundsInPitsGrid.X,
                pitsGridBounds.Y + pitBoundsInPitsGrid.Y,
                pitBoundsInPitsGrid.Width,
                pitBoundsInPitsGrid.Height
            );
            _pitLayoutBounds[pit] = pitAbsoluteBoundsInMancalaBoardLayout;

            if(pitAbsoluteBoundsInMancalaBoardLayout.X != 0)
                Console.WriteLine("Something happened here I guess");
        }
        Console.WriteLine("Calculated pit positions.");
    }
    private async Task DistributePebbles(int pebblesToDistribute)
    {
        Console.WriteLine($"Starting to distribute {pebblesToDistribute} pebbles from pit index {_currentPitIndex}.");
        // Ensure positions are calculated before starting animation
        // This acts as a fallback if SizeChanged hasn't fired yet or needed recalculation.
        if (_pitLayoutBounds.Count == 0)
        {
            CalculatePitPositions();
        }

        Rect currentPebbleStartBounds = _pitLayoutBounds[_allPits[_currentPitIndex]];
        for (int i = 0; i < pebblesToDistribute; i++)
        {
            // Move to the next pit in the sequence for distribution
            _currentPitIndex = (_currentPitIndex + 1) % _allPits.Count; // Love when I get to use modulus in the wild
            GraphicsView destinationPit = _allPits[_currentPitIndex]; // THIS IS CORRECT

            Rect currentPebbleDestinationBounds = _pitLayoutBounds[destinationPit];

            var animatingPebble = new GraphicsView
            {
                HeightRequest = 10, 
                WidthRequest = 10,
                InputTransparent = true, 
                ZIndex = 100, // max this baby out to put at the top. (who thought z index was only for css)
                Drawable = new PebbleDrawable { PebbleColor = Colors.AntiqueWhite }
            }; 



            MancalaBoardLayout.Add(animatingPebble);

            // TODO uncomment later. IT doesn't work well but it's a good reference
            //animatingPebble.TranslationX = 55;
            //animatingPebble.TranslationY = 55;

            //animatingPebble.TranslationX = currentPebbleStartBounds.X + (currentPebbleStartBounds.Width / 2) - (animatingPebble.WidthRequest / 2);
            //animatingPebble.TranslationY = currentPebbleStartBounds.Y + (currentPebbleStartBounds.Height / 2) - (animatingPebble.WidthRequest / 2);

            // This is Pit51's X, Y. Trying to get the animation pebble to start here.
            animatingPebble.TranslationX = 60;
            animatingPebble.TranslationY = 360;


            //double targetX = currentPebbleDestinationBounds.X + (currentPebbleDestinationBounds.Width / 2) - (animatingPebble.WidthRequest / 2);
            //double targetY = currentPebbleDestinationBounds.Y + (currentPebbleDestinationBounds.Height / 2) - (animatingPebble.WidthRequest / 2);

            double targetX = 0;
            double targetY = 280;
            // TranslateTo moves relative to the element's *current* position.

            //await animatingPebble.TranslateTo(
            //    targetX - animatingPebble.TranslationX,
            //    targetY - animatingPebble.TranslationY,
            //    2000, Easing.Linear); // 200ms animation duration

            await animatingPebble.TranslateTo(
                60,
                200,
                2000, Easing.Linear);

            //Update the actual destination pit's pebble count
            if (destinationPit.Drawable is PitDrawable destPitDrawable)
            {
                destPitDrawable.PebbleCount++;
                destinationPit.Invalidate(); 
            }
            //Clean up: remove the temporary animating pebble from the UI
            MancalaBoardLayout.Remove(animatingPebble);

            // Update the start bounds for the *next* pebble's animation to be the current destination's bounds.
            // This creates a cascading effect where pebbles appear to follow each other.
            currentPebbleStartBounds = currentPebbleDestinationBounds;
            await Task.Delay(50);
        }
        Console.WriteLine("Pebble distribution complete.");
    }
}