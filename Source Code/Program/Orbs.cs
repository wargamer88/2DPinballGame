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


        private Vec2 velocityTopToBottom;
        private Vec2 velocityBottomToTop;
        private Vec2 velocityRightToLeft;
        private Vec2 velocityLeftToRight;
        private Vec2 velocityTopLeftToBottomRight;
        private Vec2 velocityTopRightToBottomLeft;
        private Vec2 velocityBottomLeftToTopRight;
        private Vec2 velocityBottomRightToTopLeft;

        private Vec2 accelerationTopToBottom;
        private Vec2 accelerationBottomToTop;
        private Vec2 accelerationRightToLeft;
        private Vec2 accelerationLeftToRight;
        private Vec2 accelerationTopLeftToBottomRight;
        private Vec2 accelerationTopRightToBottomLeft;
        private Vec2 accelerationBottomLeftToTopRight;
        private Vec2 accelerationBottomRightToTopLeft;

        public Orbs(Game game)
            : base(game.width, game.height)
        {
            _orbList = new List<Ball>();

            positionTop = new Vec2(1366 / 2, -30);
            positionBottom = new Vec2(1366 / 2, 768 + 30);
            positionRight = new Vec2((1366 / 2) + (768 / 2) + 30, 768 / 2);
            positionLeft = new Vec2((1366 / 2) - (768 / 2) - 30, 768 / 2);
            positionTopLeft = new Vec2(1366 / 8 * 3, 768 / 8);
            //positionTopRight                = new Vec2
            //positionBottomLeft              = new Vec2
            //positionBottomRight             = new Vec2

            velocityTopToBottom = new Vec2(0, orbMovingSpeed);
            velocityBottomToTop = new Vec2(0, -orbMovingSpeed);
            velocityRightToLeft = new Vec2(-orbMovingSpeed, 0);
            velocityLeftToRight = new Vec2(orbMovingSpeed, 0);
            velocityTopLeftToBottomRight = new Vec2(new Vec2(orbMovingSpeed, orbMovingSpeed).Length(), new Vec2(orbMovingSpeed, orbMovingSpeed).Length());
            //velocityTopRightToBottomLeft    = new Vec2
            //velocityBottomLeftToTopRight    = new Vec2
            //velocityBottomRightToTopLeft    = new Vec2

            accelerationTopToBottom = new Vec2(0, orbAcceleration);
            accelerationBottomToTop = new Vec2(0, -orbAcceleration);
            accelerationRightToLeft = new Vec2(-orbAcceleration, 0);
            accelerationLeftToRight = new Vec2(orbAcceleration, 0);
            accelerationTopLeftToBottomRight = new Vec2(new Vec2(orbAcceleration, orbAcceleration).Length(), new Vec2(orbAcceleration, orbAcceleration).Length());
            //accelerationTopRightToBottomLeft= new Vec2
            //accelerationBottomLeftToTopRight= new Vec2
            //accelerationBottomRightToTopLeft= new Vec2
            positionTop = new Vec2(1366 / 2, -30);
            positionBottom = new Vec2(1366 / 2, 768 + 30);
            positionRight = new Vec2((1366 / 2) + (768 / 2) + 30, 768 / 2);
            positionLeft = new Vec2((1366 / 2) - (768 / 2) - 30, 768 / 2);

            //red - fire orb
            _orb = new Ball(30, positionTop, velocityTopToBottom, accelerationTopToBottom, Color.Red);
            _orb = new Ball(30, positionLeft, new Vec2(0, orbMovingSpeed), new Vec2(0, orbAcceleration), Color.Red);
            _orbList.Add(_orb);
            AddChild(_orb);

            //gray - wind orb
            _orb = new Ball(30, positionBottom, velocityBottomToTop, accelerationBottomToTop, Color.Gray);
            _orb = new Ball(30, new Vec2(game.width / 2, 0), new Vec2(0, orbMovingSpeed), new Vec2(0, orbAcceleration), Color.Gray);
            _orbList.Add(_orb);
            AddChild(_orb);

            //cyan - lightning orb
            _orb = new Ball(30, positionRight, velocityRightToLeft, accelerationRightToLeft, Color.Cyan);
            _orbList.Add(_orb);
            AddChild(_orb);

            //brown - earth orb
            _orb = new Ball(30, positionLeft, velocityLeftToRight, accelerationLeftToRight, Color.Brown);
            _orbList.Add(_orb);
            AddChild(_orb);

            //blue - water orb
            _orb = new Ball(30, positionTopLeft, velocityTopLeftToBottomRight, accelerationTopLeftToBottomRight, Color.Blue);
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
