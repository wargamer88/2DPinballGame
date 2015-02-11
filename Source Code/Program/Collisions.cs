using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Collisions
    {
        public Collisions() {}

        public Ball OuterCircleCollisionTest(OuterCircle OC, Ball ball)
        {
            Vec2 Difference = OC.position.Clone().Sub(ball.position.Clone());

            float distance = Difference.Length();

            if (distance > (ball.radius + (OC.radius - (ball.radius * 2))))
            {
                float separation = ball.radius + (OC.radius - (ball.radius * 2)) - distance;
                Vec2 normal = Difference.Normalize();
                Vec2 impulse = normal.Clone().Scale(separation);

                ball.position.Sub(impulse);
                ball.velocity.Reflect(normal);
            }
            return ball;
        }

        public bool OuterCircleCollisionTestBool(OuterCircle OC, Ball orb)
        {
            Vec2 Difference = OC.position.Clone().Sub(orb.position.Clone());

            float distance = Difference.Length();

            if (distance > (orb.radius + (OC.radius+10)))
            {
                return true;
            }
            return false;
        }

        public Ball OrbBallCollision(Ball Orb, Ball ball)
        {
            Vec2 Difference = Orb.position.Clone().Sub(ball.position.Clone());
            float distance = Difference.Length();

            if (distance < (ball.radius + Orb.radius))
            {
                float separation = ball.radius + Orb.radius - distance;
                Vec2 normal = Difference.Normalize();
                Vec2 impulse = normal.Clone().Scale(separation);

                ball.position.Sub(impulse);
                ball.velocity.Reflect(normal);
            }
            return ball;
        }
    }
}
