using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GXPEngine
{
    class Orbs : Canvas
    {
        public List<Ball> _orbList;
        Ball _orb;
        const float orbMovingSpeed = 1f;
        const float orbAcceleration = 0.0001f;

        public Orbs(Game game) : base(game.width,game.height)
        {
            _orbList = new List<Ball>();

            //red - fire orb
            _orb = new Ball(30, new Vec2(game.width / 2, 0), new Vec2(0, orbMovingSpeed), new Vec2(0, orbAcceleration), Color.Red);
            _orbList.Add(_orb);
            AddChild(_orb);

            //blue - water orb
            _orb = new Ball(30, new Vec2(game.width, game.height / 2), new Vec2(-orbMovingSpeed, 0), new Vec2(-orbAcceleration, 0), Color.Blue);
            _orbList.Add(_orb);
            AddChild(_orb);
        }

        public void StepOrbs(){
            foreach (Ball orb in _orbList)
            {
                orb.Step();
            }
        }

    }
}
