using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{	

	static void Main() {
		new MyGame().Start();
	}

    private Orbs _orbs;

	private OuterCircle _outerCircle;
    private Ball _ball;
    private Collisions _collisions = new Collisions();

    private Vec2 _gravity = new Vec2(0,1);

	private Vec2 _previousPosition;
	private Canvas _canvas;

	public MyGame () : base(1366, 768, true, false)
	{
		_canvas = new Canvas (width, height);
		AddChild (_canvas);

        _outerCircle = new OuterCircle(384, new Vec2(width/2, height/2), Color.Yellow);
        AddChild(_outerCircle);

        _ball = new Ball(10, new Vec2(width / 2, height / 2), null, _gravity, Color.Green);
		AddChild (_ball);

        _orbs = new Orbs(this);
        AddChild(_orbs);

		_ball.velocity = new Vec2 (1, 1);
		//_ball.velocity = new Vec2 (37.3f, 103.7f);
		_previousPosition = _ball.position.Clone ();
	}

	void Update () {
		targetFps = Input.GetMouseButton (0) ? 1600 : 60;
        ChangeGravity();
        _orbs.StepOrbs();

		_ball.Step ();
        _ball.acceleration = _gravity;

        float maxSpeed = 2;

        if (_ball.velocity.x > maxSpeed) _ball.velocity.x = maxSpeed;
        if (_ball.velocity.y > maxSpeed) _ball.velocity.y = maxSpeed;
        if (_ball.velocity.x < -maxSpeed) _ball.velocity.x = -maxSpeed;
        if (_ball.velocity.y < -maxSpeed) _ball.velocity.y = -maxSpeed;

        _ball = _collisions.OuterCircleCollisionTest(_outerCircle, _ball);

        foreach (Ball orb in _orbs._orbList)
        {
            _ball = _collisions.OrbBallCollision(orb, _ball);
        }

		_previousPosition = _ball.position.Clone ();
	}

    void ChangeGravity()
    {
        if (Input.GetKeyDown(Key.DOWN))
        {
            _gravity.x = 0;
            if (_gravity.y <= 0)
            {
                _gravity.y = 0;
            }
            _gravity.y = _gravity.y + 0.2f;
        }
        if (Input.GetKeyDown(Key.UP))
        {
            _gravity.x = 0;
            if (_gravity.y >= 0)
            {
                _gravity.y = 0;
            }
            _gravity.y = _gravity.y - 0.2f;
        }
        if (Input.GetKeyDown(Key.LEFT))
        {
            _gravity.y = 0;
            if (_gravity.x >= 0)
            {
                _gravity.x = 0;
            }
            _gravity.x = _gravity.x - 0.2f;
        }
        if (Input.GetKeyDown(Key.RIGHT))
        {
            _gravity.y = 0;
            if (_gravity.x <= 0)
            {
                _gravity.x = 0;
            }
            _gravity.x = _gravity.x + 0.2f;
        }
    }


}

