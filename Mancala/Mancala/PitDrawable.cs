using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{
    internal class PitDrawable : IDrawable
    {
        public void Draw(ICanvas canvas, RectF rect)
        {
            float pitRadius = Math.Min(rect.Width, rect.Height) / 2f;

            canvas.FillColor = Color.FromArgb("#ffc18c");
            canvas.FillCircle(rect.Center.X, rect.Center.Y, pitRadius);

            canvas.StrokeColor = Colors.Azure;
            canvas.StrokeSize = 2;
            canvas.DrawCircle(rect.Center.X, rect.Center.Y, pitRadius);
        }
    }
}
