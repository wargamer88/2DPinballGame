using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GXPEngine
{
    class Crystal : Ball
    {
        private Game _game;
        private OuterCircle _outerCircle;
        private Collisions _collisions;

        public Crystal(Game game, OuterCircle outerCircle)
            : base(20, Vec2.zero, Vec2.zero, Vec2.zero, Color.Black)
        {
            _game = game;
            _outerCircle = outerCircle;
            _collisions = new Collisions();
            RespawnCrystal();
            GraphicsSprite.Destroy();
            GraphicsSprite = new AnimSprite(@"Assets\Crystal.png", 13, 1);
            GraphicsSprite.height = this.height + 80;
            GraphicsSprite.width = this.width + 80;
            GraphicsSprite.SetXY(radius - 80, - radius - 40);
            AddChild(GraphicsSprite);
            Frame = 0;
            FirstFrame = 0;
            LastFrame = 13;
            FrameSpeed = 0.2;
        }

        public void RespawnCrystal()
        {
            bool allowedPosition = true;
            do
            {
                this.position = new Vec2(
                    _game.width / 2 + Utils.Random(-(_outerCircle.width / 2), (_outerCircle.width / 2)),
                    _game.height / 2 + Utils.Random(-(_outerCircle.height / 2), (_outerCircle.height / 2))
                    );

                this.Step();

                allowedPosition = _collisions.OuterCircleCollisionTestCrystalBool(_outerCircle, this);
            } while (allowedPosition);
        }
    }
}
