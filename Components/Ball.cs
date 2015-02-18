using System;
using System.Drawing;

namespace GXPEngine
{
	public class Ball : Canvas
	{
        private Vec2 _position;
        private Vec2 _velocity;
        private Vec2 _acceleration;

        //Animation------------------------------------------------------------//
        private int _lightType = 5;
        public AnimSprite GraphicsSprite;                       
        private double _frame = 0;                              
        private int _firstFrame = 0;                            
        private int _lastFrame = 15;                            
        private double _frameSpeed = 0.4;                       
        public double FrameSpeed { set { _frameSpeed = value; } }
        public double Frame { get { return _frame; } set { _frame = value; } }          
        public int FirstFrame { set { _firstFrame = value; } }  
        public int LastFrame { set { _lastFrame = value; } }
        public int LightType { get { return _lightType; } set { _lightType = value; } }
        //--------------------------------------------------------------------//

		public readonly int radius;
		private Color _ballColor;

        public Ball(int pRadius, Vec2 pPosition = null, Vec2 pVelocity = null, Vec2 pAcceleration = null, Color? pColor = null)
            : base(pRadius * 2, pRadius * 2)
		{
			radius = pRadius;
			SetOrigin (radius, radius);
            this.alpha = 0.0f;

			_position = pPosition ?? Vec2.zero;
			_velocity = pVelocity ?? Vec2.zero;
            _acceleration = pAcceleration;
			_ballColor = pColor ?? Color.Blue;

            GraphicsSprite = new AnimSprite(@"Assets\Light Ball animation.png", 15, 1);
            _frameSpeed = 0.4;
            GraphicsSprite.height = this.height+40;
            GraphicsSprite.width = this.width+40;
            GraphicsSprite.SetXY(-radius-20, -radius-20);
            AddChild(GraphicsSprite);

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

            _velocity.Add(_acceleration);
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

        void SetAnimationRange(int first, int last)
        {
            _firstFrame = first;
            _lastFrame = last;
        }

        public void UpdateAnimation()
        {
            _frame = _frame + _frameSpeed;
            if (_frame >= _lastFrame + 1.0 && _frame != 3)
            {
                _frame = _firstFrame;
            }
            if (_frame < _firstFrame && _frame != 3)
            {
                _frame = _firstFrame;
            }
            GraphicsSprite.SetFrame((int)_frame);
        }

	}
}

