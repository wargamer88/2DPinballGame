using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{	

	static void Main() {
		new MyGame().Start();
	}

	private OuterCircle _outerCircle;
    private Ball _ball;
    private Collisions _collisions = new Collisions();

    private Vec2 _gravity = new Vec2(0,1);

	private Vec2 _previousPosition;
	private Canvas _canvas;

	public MyGame () : base(1366, 768, false, false)
	{
		_canvas = new Canvas (width, height);
		AddChild (_canvas);

        _outerCircle = new OuterCircle(384, new Vec2(width/2, height/2), Color.Yellow);
        AddChild(_outerCircle);

        _ball = new Ball(30, new Vec2(width / 2, height / 2), null, _gravity, Color.Green);
		AddChild (_ball);

        Orbs _orb = new Orbs(this);
        AddChild(_orb);



		_ball.velocity = new Vec2 (1, 1);
		//_ball.velocity = new Vec2 (37.3f, 103.7f);
		_previousPosition = _ball.position.Clone ();
	}

	void Update () {
		targetFps = Input.GetMouseButton (0) ? 1 : 60;
        ChangeGravity();

		_ball.Step ();
        _ball.acceleration = _gravity;

        float maxSpeed = 2;

        if (_ball.velocity.x > maxSpeed) _ball.velocity.x = maxSpeed;
        if (_ball.velocity.y > maxSpeed) _ball.velocity.y = maxSpeed;
        if (_ball.velocity.x < -maxSpeed) _ball.velocity.x = -maxSpeed;
        if (_ball.velocity.y < -maxSpeed) _ball.velocity.y = -maxSpeed;

        _ball = _collisions.OuterCircleCollisionTest(_outerCircle, _ball);

		_previousPosition = _ball.position.Clone ();
	}

    void ChangeGravity()
    {
        if (Input.GetKeyDown(Key.UP))
        {
            _gravity = new Vec2(0, -1);
        }
        if (Input.GetKeyDown(Key.DOWN))
        {
            _gravity = new Vec2(0, 1);
        }
        if (Input.GetKeyDown(Key.LEFT))
        {
            _gravity = new Vec2(-1, 0);
        }
        if (Input.GetKeyDown(Key.RIGHT))
        {
            _gravity = new Vec2(1, 0);
        }
    }


}

