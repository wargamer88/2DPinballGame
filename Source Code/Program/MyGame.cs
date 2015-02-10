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

	public MyGame () : base(1366, 768, false, false)
	{
		_canvas = new Canvas (width, height);
		AddChild (_canvas);

        _outerCircle = new OuterCircle(384, new Vec2(width/2, height/2), Color.Yellow);
        AddChild(_outerCircle);

        _ball = new Ball(30, new Vec2(width / 2, height / 2), null, _gravity, Color.Green);
		AddChild (_ball);

        _orbs = new Orbs(this);
        AddChild(_orbs);

		_ball.velocity = new Vec2 (1, 1);
		//_ball.velocity = new Vec2 (37.3f, 103.7f);
		_previousPosition = _ball.position.Clone ();
	}

	void Update () {
		targetFps = Input.GetMouseButton (0) ? 160 : 60;
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

		_previousPosition = _ball.position.Clone ();
	}

    void ChangeGravity()
    {
        if (Input.GetKeyDown(Key.UP))
        {
            _gravity = new Vec2(0, 0);
            Game.main.OnAfterStep += GravityUP;
        }
        else if (Input.GetKeyDown(Key.DOWN))
        {
            _gravity = new Vec2(0, 0);
            Game.main.OnAfterStep += GravityDOWN;
        }
        else if (Input.GetKeyDown(Key.LEFT))
        {
            _gravity = new Vec2(0, 0);
            Game.main.OnAfterStep += GravityLEFT;
        }
        else if (Input.GetKeyDown(Key.RIGHT))
        {
            _gravity = new Vec2(0, 0);
            Game.main.OnAfterStep += GravityRIGHT;
        }
    }
    private void GravityUP()
    {
        _gravity.Add(new Vec2(0, -0.2f));

        if (_gravity.y <= -1)
        {
            _gravity = new Vec2(0, -1);
            Game.main.OnAfterStep -= GravityUP;
        }
    }

    private void GravityDOWN()
    {
        _gravity.Add(new Vec2(0, 0.2f));

        if (_gravity.y >= 1)
        {
            _gravity = new Vec2(0, 1);
            Game.main.OnAfterStep -= GravityDOWN;
        }
    }

    private void GravityLEFT()
    {
        _gravity.Add(new Vec2(-0.2f, 0));

        if (_gravity.x <= -1)
        {
            _gravity = new Vec2(-1, 0);
            Game.main.OnAfterStep -= GravityLEFT;
        }
    }

    private void GravityRIGHT()
    {
        _gravity.Add(new Vec2(0.2f, 0));

        if (_gravity.x >= 1)
        {
            _gravity = new Vec2(1, 0);
            Game.main.OnAfterStep -= GravityRIGHT;
        }
    }


}

