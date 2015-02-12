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
    private int _timer = 0; //DEBUG

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
		targetFps = Input.GetMouseButton (0) ? 1600 : 60;
        ChangeGravity();
        _orbs.StepOrbs();

        _timer++;
        if (_timer ==66)
        {
            if (_orbs._orbList.Count < 7)
            {
                enumBallPositions newPosition = Spawn.RandomPosition();
                //TODO: add position check

                _orbs.CreateOrb(Spawn.RandomColor(), Spawn.RandomPosition(), 30);
            }
            _timer = 0;
        }
        
        
		_ball.Step ();
        _ball.acceleration = _gravity;

        float maxSpeed = 2;

        if (_ball.velocity.x > maxSpeed) _ball.velocity.x = maxSpeed;
        if (_ball.velocity.y > maxSpeed) _ball.velocity.y = maxSpeed;
        if (_ball.velocity.x < -maxSpeed) _ball.velocity.x = -maxSpeed;
        if (_ball.velocity.y < -maxSpeed) _ball.velocity.y = -maxSpeed;

        _ball = _collisions.OuterCircleCollisionTest(_outerCircle, _ball);

        int removeBallIndex = -1;
        foreach (Orb orb in _orbs._orbList)
        {
            bool destroy = _collisions.OuterCircleCollisionTestBool(_outerCircle, orb);
            if (_collisions.OuterCircleCollisionTestBool(_outerCircle, orb))
            {
                removeBallIndex = orb.Index;
            }
            _ball = _collisions.OrbBallCollision(orb, _ball);

        }
        if (removeBallIndex != -1)
        {
            _orbs._orbList[removeBallIndex].Destroy();
            _orbs._orbList.RemoveAt(removeBallIndex);
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

