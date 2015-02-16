using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Spawn
    {
        public static Color RandomColor()
        {
            int random = Utils.Random(1, 6);

            switch (random)
            {
                case 1:
                    return Color.Red;
                case 2:
                    return Color.White;
                case 3:
                    return Color.Blue;
                case 4:
                    return Color.Brown;
                case 5:
                    return Color.Cyan;
                default:
                    return Color.Black;
            }
            
        }

        public static enumBallPositions RandomPosition()
        {
            int random = Utils.Random(1, 9);

            switch (random)
            {
                case 1:
                    return enumBallPositions.Top;
                case 2:
                    return enumBallPositions.Bottom;
                case 3:
                    return enumBallPositions.Left;
                case 4:
                    return enumBallPositions.Right;
                case 5:
                    return enumBallPositions.TopLeft;
                case 6:
                    return enumBallPositions.TopRight;
                case 7:
                    return enumBallPositions.BottomLeft;
                case 8:
                    return enumBallPositions.BottomRight;
                default:
                    return enumBallPositions.Bottom;
            }
        }
    }
}
