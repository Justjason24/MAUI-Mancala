using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{
    internal class MancalaDrawable : IDrawable
    {
       
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            // lmao i still don't know why docs refer to it as a direct rectangle 

            float width = dirtyRect.Width; //400
            float height = dirtyRect.Height; //600
            float margin = 20;
            float pitPadding = 3;

            float bankHeight = height * 0.15f; // 60 maybe this should be 15f , not 10f
            float pitAreaHeight = height * 0.60f; // 360
            float pitAreaRowHeight = pitAreaHeight / 6; // 60

            float circleRadius = (pitAreaRowHeight / 2) - pitPadding; //27

            canvas.StrokeColor = Colors.SaddleBrown;
            canvas.StrokeSize = 4;

            //var topBankRect = new RectF(margin, margin, width - (margin * 2), bankHeight);
            // var centerX = width - (margin * 2) - bankWidth (200)
            var bankWidth = 300;
            var topBankRect = new RectF((width - bankWidth) / 2 , margin, bankWidth, bankHeight);
            canvas.DrawRoundedRectangle(topBankRect, 20);

            // lets draw a single circle
            // the height of the pit drawable area is 360, the width is the bank width from above
            // divie 360 into 6ths so 60 is the height area we're working whith in this example

            var testPadding = 10;

            //canvas.DrawCircle(100, 180, 40);
            //canvas.DrawCircle(100, 260 + testPadding, 25);

            var startingY = 150;
            for (int i = 0; i < 6; i++)
            {

                canvas.DrawCircle(110, startingY, 25);
                startingY = (int)(startingY + pitAreaRowHeight);

            }

            Console.WriteLine();
            //for (int row = 0; row < 6; row++)
            //{
            //    // Calculate the central Y coordinate for this row of pits
            //    float yPosCenter = margin + bankHeight + (row * pitAreaRowHeight) + (pitAreaRowHeight / 2);

            //    // Left Pit Center
            //    float leftXPosCenter = margin + circleRadius + pitPadding;
            //    canvas.DrawCircle(leftXPosCenter, yPosCenter, circleRadius);

            //    // Right Pit Center
            //    float rightXPosCenter = width - margin - circleRadius - pitPadding;
            //    canvas.DrawCircle(rightXPosCenter, yPosCenter, circleRadius);
            //}

            float bottomBankY = height - bankHeight - margin;
            var bottomBankRect = new RectF(margin, bottomBankY, width - (margin * 2), bankHeight);
            canvas.DrawRoundedRectangle(bottomBankRect, 20);

            Console.WriteLine();
        }
    }
}
