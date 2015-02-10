using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GXPEngine;

namespace GXPEngine
{
    class OuterCircle : Canvas
    {
        private Vec2 _position;

        public readonly int radius;
        private Color _ballColor;

        public OuterCircle(int pRadius, Vec2 pPosition = null, Color? pColor = null)
            : base(pRadius * 2, pRadius * 2)
		{
			radius = pRadius;
			SetOrigin (radius, radius);

			_position = pPosition ?? Vec2.zero;
			_ballColor = pColor ?? Color.Blue;

			draw ();

            x = _position.x;
            y = _position.y;
		}

        private void draw()
        {
            graphics.Clear(Color.Empty);
            graphics.FillEllipse(
                new SolidBrush(_ballColor),
                0, 0, 2 * radius, 2 * radius
            );
        }

        public Vec2 position
        {
            set
            {
                _position = value ?? Vec2.zero;
            }
            get
            {
                return _position;
            }
        }
    }
}
