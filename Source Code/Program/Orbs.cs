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

        public Orbs(Game game) : base(game.width,game.height)
        {
            _orbList = new List<Ball>();

            //red - fire orb
            _orb = new Ball(30, new Vec2(game.width / 2, 0), new Vec2(0, 1), new Vec2(0,1), Color.Red);
            _orbList.Add(_orb);
            AddChild(_orb);
        }

    }
}
