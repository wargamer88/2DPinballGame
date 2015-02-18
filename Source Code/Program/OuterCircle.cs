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

        //Animation------------------------------------------------------------//
        public AnimSprite GraphicsSprite;
        public AnimSprite GraphicsSprite2;
        public AnimSprite GraphicsSprite3;
        public AnimSprite GraphicsSprite4;

        private double _frame = 0;
        private int _firstFrame = 0;
        private int _lastFrame = 14;
        private double _frameSpeed = 0.4;
        public double FrameSpeed { set { _frameSpeed = value; } }
        public double Frame { get { return _frame; } set { _frame = value; } }
        public int FirstFrame { set { _firstFrame = value; } }
        public int LastFrame { set { _lastFrame = value; } }
        //--------------------------------------------------------------------//

        public OuterCircle(int pRadius, Vec2 pPosition = null, Color? pColor = null)
            : base(pRadius * 2, pRadius * 2)
		{
			radius = pRadius;
			SetOrigin (radius, radius);
            this.alpha = 0.0f;

			_position = pPosition ?? Vec2.zero;
			_ballColor = pColor ?? Color.Blue;

            //Back with Clouds
            GraphicsSprite = new AnimSprite(@"Assets\Back with clouds.png", 1, 1);
            GraphicsSprite.height = this.height;
            GraphicsSprite.width = this.width;
            GraphicsSprite.SetOrigin(GraphicsSprite.width / 2, GraphicsSprite.height / 2);
            AddChild(GraphicsSprite);

            //Stars1
            GraphicsSprite2 = new AnimSprite(@"Assets\star.png", 1, 1);
            GraphicsSprite2.height = this.height;
            GraphicsSprite2.width = this.width;
            GraphicsSprite2.SetOrigin(GraphicsSprite2.width / 2, GraphicsSprite2.height / 2);
            AddChild(GraphicsSprite2);

            //Stars2
            GraphicsSprite3 = new AnimSprite(@"Assets\star 2.png", 1, 1);
            GraphicsSprite3.height = this.height;
            GraphicsSprite3.width = this.width;
            GraphicsSprite3.SetOrigin(GraphicsSprite3.width / 2, GraphicsSprite3.height / 2);
            AddChild(GraphicsSprite3);

            //Stars3
            GraphicsSprite4 = new AnimSprite(@"Assets\star 3.png", 1, 1);
            GraphicsSprite4.height = this.height;
            GraphicsSprite4.width = this.width;
            GraphicsSprite4.SetOrigin(GraphicsSprite4.width / 2, GraphicsSprite4.height / 2);
            AddChild(GraphicsSprite4);
            

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
