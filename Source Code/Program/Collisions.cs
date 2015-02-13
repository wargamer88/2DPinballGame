using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Collisions
    {
        public Collisions() {}

        public bool OuterCircleCollisionTest(OuterCircle OC, Ball ball)
        {
            Vec2 Difference = OC.position.Clone().Sub(ball.position.Clone());

            float distance = Difference.Length();

            if (distance > (ball.radius + (OC.radius - (ball.radius * 2))))
            {
                return true;
            }
            return false;
        }

        public bool OuterCircleCollisionTestBool(OuterCircle OC, Orb orb)
        {
            Vec2 Difference = OC.position.Clone().Sub(orb.position.Clone());

            float distance = Difference.Length();

            if (distance > (orb.radius + (OC.radius+10)))
            {
                return true;
            }
            return false;
        }
        public bool OuterCircleCollisionTestCrystalBool(OuterCircle OC, Crystal crystal)
        {
            Vec2 Difference = OC.position.Clone().Sub(crystal.position.Clone());

            float distance = Difference.Length();

            if (distance > (crystal.radius + (OC.radius + 10)))
            {
                return true;
            }
            return false;
        }

        public bool BallCollisionTestCrystalBool(Ball ball, Crystal crystal)
        {
            Vec2 Difference = crystal.position.Clone().Sub(ball.position.Clone());

            float distance = Difference.Length();
            

            if (distance < (crystal.radius + ball.radius))
            {
                Console.WriteLine(distance);
                return true;
            }
            return false;
        }

        public bool OrbBallCollisionTest(Orb Orb, Ball ball)
        {
            Vec2 Difference = Orb.position.Clone().Sub(ball.position.Clone());
            float distance = Difference.Length();

            if (distance < (ball.radius + Orb.radius))
            {
                return true;
            }
            return false;
        }

        public Ball OrbBallCollision(Orb Orb, Ball ball)
        {
            Vec2 Difference = ball.position.Clone().Sub(Orb.position.Clone());
            float distance = Difference.Length();

            if (distance < (ball.radius + Orb.radius))
            {
                Vec2 normal = Difference.Clone().Normalize();
                Vec2 separation = normal.Clone().Scale((distance - (ball.radius + Orb.radius)));
                ball.position.Sub(separation);
                //Orb.position.Add(separation);
                
                Vec2 relativeVelocity = ball.velocity.Clone().Sub(Orb.velocity);
                float scalar = relativeVelocity.Clone().Dot(normal);
                Vec2 impulse = normal.Clone().Scale(scalar);
                //Factor1 = the Mass of Ball1 (Bounciness), Factor2 = The mass of Orb (bounciness)
                float factor1 = 2.0f;
                float factor2 = 0.0f;
                ball.velocity = ball.velocity.Sub(impulse.Clone().Scale(factor1));
                Orb.velocity = Orb.velocity.Add(impulse.Clone().Scale(factor2));
            }
            return ball;
        }

        
    }
}

/*
float separation = ball.radius + Orb.radius - distance;
                Vec2 normal = Difference.Normalize();
                Vec2 impulse = normal.Clone().Scale(separation);

                ball.position.Sub(impulse);
                ball.velocity.Reflect(normal);
*/