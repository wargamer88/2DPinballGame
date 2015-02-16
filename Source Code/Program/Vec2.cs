using System;

namespace GXPEngine
{
	public class Vec2 
	{
		public static Vec2 zero { get { return new Vec2(0,0); }}
		public static Vec2 temp = new Vec2 ();

		public float x = 0;
		public float y = 0;

		public Vec2 (float pX = 0, float pY = 0)
		{
			x = pX;
			y = pY;
		}

		public override string ToString ()
		{
			return String.Format ("({0}, {1})", x, y);
		}
			
		public Vec2 Add (Vec2 other) {
			x += other.x;
			y += other.y;
			return this;
		}

		public Vec2 Sub (Vec2 other) {
			x -= other.x;
			y -= other.y;
			return this;
		}

		public float Length() {
			return (float)Math.Sqrt (x * x + y * y);
		}

		public Vec2 Normalize () {
			if (x == 0 && y == 0) {
				return this;
			} else {
				return Scale (1/Length ());
			}
		}

		public Vec2 Clone() {
			return new Vec2 (x, y);
		}
	
		public Vec2 Scale (float scalar) {
			x *= scalar;
			y *= scalar;
			return this;
		}

		public Vec2 Set (float pX, float pY) {
			x = pX;
			y = pY;
			return this;
		}

        public Vec2 Normal()
        {
            double angle = Math.PI * 90 / 180;

            float Px = (float)(x * Math.Cos(angle) - y * Math.Sin(angle));
            float Py = (float)(x * Math.Sin(angle) + y * Math.Cos(angle));

            return new Vec2(Px, Py).Normalize();
			
			//Normal = (-y,x).Normalize
        }

        public float Dot(Vec2 other)
        {
		    //Dot = v1.x * v2.x + v1.y * v2.y
            return this.x * other.x + this.y * other.y;
        }

        public Vec2 SetLength(float length)
        {
            Vec2 result = new Vec2(x, y).Normalize().Scale(length);
            return result;
        }

        public double GetAngleRadians()
        {
            return Math.Atan2(y, x);
        }

        public double GetAngleDegrees()
        {
            double radians = Math.Atan2(y, x);
            return radians * 180 / Math.PI;
        }

        public void SetAngleDegrees(double angleAsDegrees)
        {
            double angle = Math.PI * angleAsDegrees / 180;

            float length = Length();
            x = (float)Math.Cos(angle) * length;
            y = (float)Math.Sin(angle) * length;
        }

        public void SetAngleRadians(double angleAsRadians)
        {
            float length = Length();
            x = (float)Math.Cos(angleAsRadians) * length;
            y = (float)Math.Sin(angleAsRadians) * length;
        }

        public void RotateDegrees(double degrees)
        {
            double angle = Math.PI * degrees / 180;

            float x2 = (float)(x * Math.Cos(angle) - y * Math.Sin(angle));
            float y2 = (float)(x * Math.Sin(angle) + y * Math.Cos(angle));

            x = x2;
            y = y2;
        }

        public void RotateRadians(double radians)
        {
            float x2 = (float)(x * Math.Cos(radians) - y * Math.Sin(radians));
            float y2 = (float)(x * Math.Sin(radians) + y * Math.Cos(radians));

            x = x2;
            y = y2;
        }

        public Vec2 Reflect(Vec2 normal, float bounciness = 1)
        {
		    //v' = 2 * (v . n) * n - v;  (. = Dot, n = Normal) <--- Perfect Reflection
            Vec2 Result = this.Sub(normal.Clone().Scale((1 + bounciness) * this.Dot(normal)));
            return Result;
        }
	}
}

