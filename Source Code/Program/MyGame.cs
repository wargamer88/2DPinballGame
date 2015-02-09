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
	private Ball _ball;
    private List<Ball> _linecaps;

	private Vec2 _previousPosition;
	private Canvas _canvas;

	public MyGame () : base(800, 600, false, false)
	{
		_canvas = new Canvas (width, height);
		AddChild (_canvas);

		_lines = new List<NLineSegment> ();

		AddLine (new Vec2 (0, 0), new Vec2 (width, 0));	
		AddLine (new Vec2 (width, 0), new Vec2 (width, height));	
		AddLine (new Vec2 (width, height), new Vec2 (0, height));	
		AddLine (new Vec2 (0, height), new Vec2 (0,0));

        AddLine(new Vec2(200, 200), new Vec2(width - 200, height - 200));

        _linecaps = new List<Ball>();

        _ball = new Ball(1, new Vec2(200, 200), null, Color.Yellow);
        _linecaps.Add(_ball);
        AddChild(_ball);
        _ball = new Ball(1, new Vec2(width - 200, height - 200), null, Color.Yellow);
        _linecaps.Add(_ball);
        AddChild(_ball);

		_ball = new Ball (30, new Vec2 (width / 2, 3 * height / 4), null, Color.Green);
		AddChild (_ball);

		_ball.velocity = new Vec2 (10, 10);
		//_ball.velocity = new Vec2 (37.3f, 103.7f);
		_previousPosition = _ball.position.Clone ();
	}

	private void AddLine (Vec2 start, Vec2 end) {
		NLineSegment line = new NLineSegment (start, end, 0xff00ff00, 4);
		AddChild (line);
		_lines.Add (line);
	}

	void Update () {
		targetFps = Input.GetMouseButton (0) ? 1000 : 60;

		_ball.Step ();

		for (int i = 0; i < _lines.Count; i++) {
			lineCollisionTest (_lines [i]);
		}


		//draw line
		_canvas.graphics.DrawLine (
			Pens.White, _previousPosition.x, _previousPosition.y, _ball.position.x, _ball.position.y
		);

		_previousPosition = _ball.position.Clone ();
	}

    void lineCollisionTest(NLineSegment line)
    {
        Vec2 ball2line = _ball.position.Clone().Sub(line.start.Clone()); // distance ballcenter to line START

        Vec2 lineVector = line.end.Clone().Sub(line.start.Clone()).Normalize(); // line length measured in 1 unit
        Vec2 lineNormal = line.end.Clone().Sub(line.start.Clone()).Normal(); // normal of line (rotated 90 degrees from start to end)
        float lineLength = line.end.Clone().Sub(line.start.Clone()).Length(); // line length, total length of the line

        float ballDistanceAlongLine = ball2line.Dot(lineVector); // distance from start of the line to the ball using DOT
        float ballDistanceToLine = ball2line.Dot(lineNormal); // distance from ballcenter to line using DOT(with angle of the normal of the line)

        if (ballDistanceAlongLine < 0) ballDistanceAlongLine = 0; //min --ballDistanceAlongLine-- 
        if (ballDistanceAlongLine > lineLength) ballDistanceAlongLine = lineLength;//max --ballDistanceAlongLine-- using length of the line

        Vec2 projection = line.start.Clone().Add(lineVector.Scale(ballDistanceAlongLine)); // SCALAR PROJECTION -- ball vector to lineA 'draws line to lineA (pointQ on lineA)' -- lineA start moves to pointQ
        Vec2 difference = _ball.position.Clone().Sub(projection); //difference between ballcenter and projection 'pointQ' 

        if (difference.Length() < _ball.radius) // difference length (smaller than) ball.radius
        {

            difference = difference.SetLength(_ball.radius); // set length of difference to radius of the ball
            _ball.position = projection.Add(difference); // set ball position = projection 'pointQ' + difference
            _ball.velocity.Reflect(lineVector.Clone().Normal()); // reflect velocity
        }
        else
        {
            for (int i = 0; i < _linecaps.Count; i++)
            {
                lineCapCollisionTest(_linecaps[i]);
            }
        }

        _ball.x = _ball.position.x;
        _ball.y = _ball.position.y;
	}

    void lineCapCollisionTest(Ball lineCap)
    {
        Vec2 Difference = lineCap.position.Clone().Sub(_ball.position.Clone());
        float distance = Difference.Length();

        if (distance < (_ball.radius + lineCap.radius))
        {
            float separation = _ball.radius + lineCap.radius - distance;
            Vec2 normal = Difference.Normalize();
            Vec2 impulse = normal.Clone().Scale(separation);

            _ball.position.Sub(impulse);
            _ball.velocity.Reflect(normal);

        }
    }


}

