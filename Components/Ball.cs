using System;
using System.Drawing;

namespace GXPEngine
{
	public class Ball : Canvas
	{
        private Vec2 _position;
        private Vec2 _velocity;
        private Vec2 _acceleration;

		public readonly int radius;
		private Color _ballColor;

        public Ball(int pRadius, Vec2 pPosition = null, Vec2 pVelocity = null, Vec2 pAcceleration = null, Color? pColor = null)
            : base(pRadius * 2, pRadius * 2)
		{
			radius = pRadius;
			SetOrigin (radius, radius);

			_position = pPosition ?? Vec2.zero;
			_velocity = pVelocity ?? Vec2.zero;
            _acceleration = pAcceleration;
			_ballColor = pColor ?? Color.Blue;

			draw ();
			Step ();
		}

		private void draw() {
			graphics.Clear (Color.Empty);
			graphics.FillEllipse (
				new SolidBrush (_ballColor),
				0, 0, 2 * radius, 2 * radius
			);
		}

		public void Step(bool skipVelocity = false) {
			if (_position == null || _velocity == null)
				return;

            _velocity = _acceleration;
			if (!skipVelocity) _position.Add (_velocity);
            
            

			x = _position.x;
			y = _position.y;
		}

		public Color ballColor {
			get {
				return _ballColor;
			}

			set {
				_ballColor = value;
				draw ();
			}
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

        public Vec2 acceleration
        {
            set
            {
                _acceleration = value ?? Vec2.zero;
            }
            get
            {
                return _acceleration;
            }
        }

        public Vec2 velocity
        {
            set
            {
                _velocity = value ?? Vec2.zero;
            }
            get
            {
                return _velocity;
            }
        }

	}
}

