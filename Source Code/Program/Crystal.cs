using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GXPEngine
{
    class Crystal : Ball
    {
        private OuterCircle _outerCircle;
        private Collisions _collisions;

        private int _height = 0;
        private int _width = 0;
        private bool _allowFadeOut = false;
        private bool _allowFadeIn = false;
        public bool AllowFadeOut { get { return _allowFadeOut; } set { _allowFadeOut = value; } }
        public bool AllowFadeIn { get { return _allowFadeIn; } set { _allowFadeIn = value; } }

        public Crystal(int height, int width, OuterCircle outerCircle)
            : base(20, Vec2.zero, Vec2.zero, Vec2.zero, Color.Black)
        {
            _height = height;
            _width = width;
            _outerCircle = outerCircle;
            _collisions = new Collisions();
            RespawnCrystal();
            GraphicsSprite.Destroy();
            GraphicsSprite = new AnimSprite(@"Assets\Crystal.png", 12, 1);
            GraphicsSprite.height = this.height + 40;
            GraphicsSprite.width = this.width + 40;
            GraphicsSprite.SetXY(radius - 60, - radius - 20);
            AddChild(GraphicsSprite);
            Frame = 0;
            FirstFrame = 0;
            LastFrame = 12;
            FrameSpeed = 0.2;
        }

        public void RespawnCrystal()
        {
            bool allowedPosition = true;
            do
            {
                this.position = new Vec2(
                    _width / 2 + Utils.Random(-(_outerCircle.width / 2), (_outerCircle.width / 2)),
                    _height / 2 + Utils.Random(-(_outerCircle.height / 2), (_outerCircle.height / 2))
                    );

                this.Step();

                allowedPosition = _collisions.OuterCircleCollisionTestCrystalBool(_outerCircle, this);
            } while (allowedPosition);
        }
    }
}
