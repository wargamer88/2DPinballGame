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
