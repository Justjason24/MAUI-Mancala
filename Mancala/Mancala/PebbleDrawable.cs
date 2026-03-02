using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mancala
{
    public class PebbleDrawable : IDrawable
    {
        public Color PebbleColor { get; set; } = Colors.White; // for now 
        public void Draw(ICanvas canvas, RectF rect)
        {
            float radius = Math.Min(rect.Width, rect.Height) / 2f;
            canvas.FillColor = PebbleColor;
            canvas.FillCircle(rect.Center.X, rect.Center.Y, radius);
            canvas.StrokeColor = Colors.Black;
            canvas.StrokeSize = 2;
            canvas.DrawCircle(rect.Center.X, rect.Center.Y, radius);
        }
    }
}
