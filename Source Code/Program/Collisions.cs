using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Collisions
    {
        public Collisions()
        {
        }
        
        public Ball OuterCircleCollisionTest(OuterCircle OC, Ball ball)
        {
            Vec2 Difference = OC.position.Clone().Sub(ball.position.Clone());
            float distance = Difference.Length();

            if (distance > (ball.radius + OC.radius))
            {
                float separation = ball.radius + OC.radius - distance;
                Vec2 normal = Difference.Normalize();
                Vec2 impulse = normal.Clone().Scale(separation);

                ball.position.Sub(impulse);
                ball.velocity.Reflect(normal);
            }
            return ball;
        }
    }
}
