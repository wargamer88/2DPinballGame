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

        //resolution on 1366 x 768
        private Vec2 positionTop;
        private Vec2 positionBottom;
        private Vec2 positionRight;
        private Vec2 positionLeft;
        private Vec2 positionTopLeft;
        private Vec2 positionTopRight;
        private Vec2 positionBottomLeft;
        private Vec2 positionBottomRight;

        public Orbs(Game game) : base(game.width,game.height)
        {
            _orbList = new List<Ball>();

            positionTop = new Vec2(1366 / 2, -30);
            positionBottom = new Vec2(1366 / 2, 768 + 30);
            positionRight = new Vec2((1366 / 2) + (768 / 2) + 30, 768 / 2);
            positionLeft = new Vec2((1366 / 2) - (768 / 2) - 30, 768 / 2);

            //red - fire orb
            _orb = new Ball(30, positionLeft, new Vec2(0, orbMovingSpeed), new Vec2(0, orbAcceleration), Color.Red);
            _orbList.Add(_orb);
            AddChild(_orb);
            //huh

            //gray - wind orb
            _orb = new Ball(30, new Vec2(game.width / 2, 0), new Vec2(0, orbMovingSpeed), new Vec2(0, orbAcceleration), Color.Gray);
            _orbList.Add(_orb);
            AddChild(_orb);

            //cyan - lightning orb

            //brown - earth orb

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
