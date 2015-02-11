using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GXPEngine
{
    public enum enumBallPositions
    {
        Top,
        Bottom,
        Right,
        Left,
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight,
    }

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

            #region set positions
            positionTop                         = new Vec2(1366 / 2, -30);
            positionBottom                      = new Vec2(1366 / 2, 768 + 30);
            positionRight                       = new Vec2((1366 / 2) + (768 / 2) + 30, 768 / 2);
            positionLeft                        = new Vec2((1366 / 2) - (768 / 2) - 30, 768 / 2);
            positionTopLeft                     = new Vec2((1366 / 32 * 9) + 10, (768 / 32 * 3.75f) + 1);
            positionTopRight                    = new Vec2((1366 / 32 * 23) + 10, (768 / 32 * 3.75f) + 1);
            positionBottomLeft                  = new Vec2((1366 / 32 * 9) + 10, (768 / 32 * 28.25f) + 1);
            positionBottomRight                 = new Vec2((1366 / 32 * 23) + 10, (768 / 32 * 28.25f) + 1);
            #endregion

            #region set velocities
            velocityTopToBottom                 = new Vec2(0, orbMovingSpeed);
            velocityBottomToTop                 = new Vec2(0, -orbMovingSpeed);
            velocityRightToLeft                 = new Vec2(-orbMovingSpeed, 0);
            velocityLeftToRight                 = new Vec2(orbMovingSpeed, 0);
            velocityTopLeftToBottomRight        = new Vec2(orbMovingSpeed, orbMovingSpeed).Normalize().Scale(orbMovingSpeed);
            velocityTopRightToBottomLeft        = new Vec2(-orbMovingSpeed, orbMovingSpeed).Normalize().Scale(orbMovingSpeed);
            velocityBottomLeftToTopRight        = new Vec2(orbMovingSpeed, -orbMovingSpeed).Normalize().Scale(orbMovingSpeed);
            velocityBottomRightToTopLeft        = new Vec2(-orbMovingSpeed, -orbMovingSpeed).Normalize().Scale(orbMovingSpeed);
            #endregion

            #region set accelerations
            accelerationTopToBottom             = new Vec2(0, orbAcceleration);
            accelerationBottomToTop             = new Vec2(0, -orbAcceleration);
            accelerationRightToLeft             = new Vec2(-orbAcceleration, 0);
            accelerationLeftToRight             = new Vec2(orbAcceleration, 0);
            accelerationTopLeftToBottomRight    = new Vec2(orbAcceleration, orbAcceleration).Normalize().Scale(orbAcceleration);
            accelerationTopRightToBottomLeft    = new Vec2(-orbAcceleration, orbAcceleration).Normalize().Scale(orbAcceleration);
            accelerationBottomLeftToTopRight    = new Vec2(orbAcceleration, -orbAcceleration).Normalize().Scale(orbAcceleration);
            accelerationBottomRightToTopLeft    = new Vec2(-orbAcceleration, -orbAcceleration).Normalize().Scale(orbAcceleration);
            #endregion

            createOrbs();

        }

        public void StepOrbs(){
            foreach (Ball orb in _orbList)
            {
                orb.Step();
            }
        }

        private void createOrbs(){
            //red - fire orb
            CreateOrb(Color.Red, enumBallPositions.Top);

            //gray - wind orb
            CreateOrb(Color.LightSlateGray, enumBallPositions.Bottom);

            //cyan - lightning orb
            CreateOrb(Color.Cyan, enumBallPositions.Right);

            //brown - earth orb
            CreateOrb(Color.Brown, enumBallPositions.Left);

            //blue - water orb
            CreateOrb(Color.Blue, enumBallPositions.TopLeft);

            //orangeRed - explosion orb
            CreateOrb(Color.OrangeRed, enumBallPositions.TopRight);

            //orange - lava orb
            CreateOrb(Color.Orange, enumBallPositions.BottomLeft);

            //gray - steam orb
            CreateOrb(Color.Gray, enumBallPositions.BottomRight);
        }



        public void CreateOrb(Color color,enumBallPositions position, int radius = 30)
        {
            switch (position)
            {
                case enumBallPositions.Top:
                    _orb = new Ball(radius, positionTop.Clone(), velocityTopToBottom.Clone(), accelerationTopToBottom.Clone(), color);
                    _orbList.Add(_orb);
                    AddChild(_orb);
                    _orb = null;
                    break;
                case enumBallPositions.Bottom:
                    _orb = new Ball(radius, positionBottom.Clone(), velocityBottomToTop.Clone(), accelerationBottomToTop.Clone(), color);
                    _orbList.Add(_orb);
                    AddChild(_orb);
                    _orb = null;
                    break;
                case enumBallPositions.Left:
                    _orb = new Ball(radius, positionLeft.Clone(), velocityLeftToRight.Clone(), accelerationLeftToRight.Clone(), color);
                    _orbList.Add(_orb);
                    AddChild(_orb);
                    break;
                case enumBallPositions.Right:
                    _orb = new Ball(radius, positionRight.Clone(), velocityRightToLeft.Clone(), accelerationRightToLeft.Clone(), color);
                    _orbList.Add(_orb);
                    AddChild(_orb);
                    break;
                case enumBallPositions.TopLeft:
                    _orb = new Ball(radius, positionTopLeft.Clone(), velocityTopLeftToBottomRight.Clone(), accelerationTopLeftToBottomRight.Clone(), color);
                    _orbList.Add(_orb);
                    AddChild(_orb);
                    break;
                case enumBallPositions.TopRight:
                    _orb = new Ball(radius, positionTopRight.Clone(), velocityTopRightToBottomLeft.Clone(), accelerationTopRightToBottomLeft.Clone(), color);
                    _orbList.Add(_orb);
                    AddChild(_orb);
                    break;
                case enumBallPositions.BottomLeft:
                    _orb = new Ball(radius, positionBottomLeft.Clone(), velocityBottomLeftToTopRight.Clone(), accelerationBottomLeftToTopRight.Clone(), color);
                    _orbList.Add(_orb);
                    AddChild(_orb);
                    break;
                case enumBallPositions.BottomRight:
                    _orb = new Ball(30, positionBottomRight.Clone(), velocityBottomRightToTopLeft.Clone(), accelerationBottomRightToTopLeft.Clone(), Color.Gray);
                    _orbList.Add(_orb);
                    AddChild(_orb);
                    break;
                default:
                    Console.WriteLine("---------Not added the ball: " + position.ToString() + " -----------");
                    break;
            }
        }

    }
}
