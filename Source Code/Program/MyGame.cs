using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{	

	static void Main() {
		new MyGame().Start();
	}

	private List<NLineSegment> _lines;
	private Ball _outerCircle;
    private Ball _ball;
    private List<Ball> _linecaps;

	private Vec2 _previousPosition;
	private Canvas _canvas;

	public MyGame () : base(1366, 768, false, false)
	{
		_canvas = new Canvas (width, height);
		AddChild (_canvas);

        _outerCircle = new Ball(384, new Vec2(width/2, height/2), null, Color.Yellow);
        AddChild(_outerCircle);

		_ball = new Ball (30, new Vec2 (width / 2, 3 * height / 4), null, Color.Green);
		AddChild (_ball);

		_ball.velocity = new Vec2 (10, 10);
		//_ball.velocity = new Vec2 (37.3f, 103.7f);
		_previousPosition = _ball.position.Clone ();
	}

	void Update () {
		targetFps = Input.GetMouseButton (0) ? 1000 : 60;

		_ball.Step ();

        OuterCircleCollisionTest(_outerCircle);

		_previousPosition = _ball.position.Clone ();
	}

    void OuterCircleCollisionTest(Ball ball)
    {
        Vec2 Difference = ball.position.Clone().Sub(_ball.position.Clone());
        float distance = Difference.Length();

        if (distance < (_ball.radius + ball.radius))
        {
            float separation = _ball.radius + ball.radius - distance;
            Vec2 normal = Difference.Normalize();
            Vec2 impulse = normal.Clone().Scale(separation);

            _ball.position.Sub(impulse);
            _ball.velocity.Reflect(normal);

        }
    }


}

