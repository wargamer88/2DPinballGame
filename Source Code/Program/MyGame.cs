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

    //Effects
    //-Fire:
    private bool _fireEffect = false;
    private int _fireTimer = 0;
    //-Water:
    private bool _waterEffect = false;
    private int _waterTimer = 0;
    //-Lightning
    private bool _lightningEffect = false;
    private int _lightningTimer = 0;


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

		_ball.velocity = new Vec2 (0, 0);
		//_ball.velocity = new Vec2 (37.3f, 103.7f);
		_previousPosition = _ball.position.Clone ();
	}

	void Update () {
		targetFps = Input.GetMouseButton (0) ? 1600 : 60;
        ChangeGravity();
        _orbs.StepOrbs();
        SpawnOrbs();

        _ball.acceleration = _gravity;

        CheckMaxSpeed();
        Collisions();
        CheckEffects();
        _ball.Step();
        
	}

    void CheckEffects()
    {
        if (_fireEffect == true)
        {
            _fireTimer++;
            if (_fireTimer >= 45)
            {
                _fireEffect = false;
                _fireTimer = 0;
            }
            _ball.velocity.Scale(2);
        }

        if (_waterEffect == true)
        {
            _waterTimer++;
            if (_waterTimer >= 125)
            {
                _waterEffect = false;
                _waterTimer = 0;
            }
            _ball.velocity.Scale(0.85f);
        }

        if (_lightningEffect == true)
        {
            _lightningTimer++;

            if (_lightningTimer >= 60)
            {
                _lightningEffect = false;
                _lightningTimer = 0;
            }
            _ball.velocity = Vec2.zero;
        }
    }

    void Collisions()
    {
        #region Ball&Outercirle Collision
        bool hitEdge = _collisions.OuterCircleCollisionTest(_outerCircle, _ball);
        if (hitEdge == true)
        {
            _ball.Destroy();
            _ball = new Ball(30, new Vec2(width / 2, height / 2), null, _gravity, Color.Green);
            Console.WriteLine("You Died!");
            AddChild(_ball);
        } 
        #endregion

        #region Orb/OuterCirle/Ball Collision Old One
        int removeBallIndex = -1;
        foreach (Orb orb in _orbs._orbList)
        {
            bool destroy = _collisions.OuterCircleCollisionTestBool(_outerCircle, orb);
            if (_collisions.OuterCircleCollisionTestBool(_outerCircle, orb))
            {
                removeBallIndex = orb.Index;
            }
            //_ball = _collisions.OrbBallCollision(orb, _ball);

        }
        if (removeBallIndex != -1)
        {
            _orbs._orbList[removeBallIndex].Destroy();
            _orbs._orbList.RemoveAt(removeBallIndex);
        }

        _previousPosition = _ball.position.Clone(); 
        #endregion

        #region Orb/Ball Collision New One
        bool collision = false;
        foreach (Orb orb in _orbs._orbList)
        {
            collision = _collisions.OrbBallCollisionTest(orb, _ball);
            if (collision)
            {
                string color = orb.ballColor.Name;
                switch (color)
                {
                    case "Red":
                        _fireEffect = true;
                        break;
                    case "White":
                        //FK This
                        break;
                    case "Cyan":
                        _lightningEffect = true;
                        break;
                    case "Brown":
                        _ball = _collisions.OrbBallCollision(orb, _ball);
                        break;
                    case "Blue":
                        _waterEffect = true;
                        break;
                }
            }
        }

        #endregion
    }

    void CheckMaxSpeed()
    {
        float maxSpeed = 2;

        if (_ball.velocity.x > maxSpeed) _ball.velocity.x = maxSpeed;
        if (_ball.velocity.y > maxSpeed) _ball.velocity.y = maxSpeed;
        if (_ball.velocity.x < -maxSpeed) _ball.velocity.x = -maxSpeed;
        if (_ball.velocity.y < -maxSpeed) _ball.velocity.y = -maxSpeed;
    }

    void SpawnOrbs() //Debug
    {
        _timer++;
        if (_timer == 66)
        {
            if (_orbs._orbList.Count < 7)
            {
                bool correctPosition = false;
                enumBallPositions newPosition = Spawn.RandomPosition();
                if (_orbs._orbList.Count > 3)
                {
                    do
                    {
                        newPosition = Spawn.RandomPosition();

                        for (int i = 0; i < _orbs._orbList.Count; i++)
                        {
                            if (i >= _orbs._orbList.Count - 3)
                            {
                                if (_orbs._orbList[i].positionEnum == newPosition)
                                {
                                    correctPosition = false;
                                }
                                else
                                {
                                    correctPosition = true;
                                }
                            }
                        }
                    } while (correctPosition == false);
                }

                _orbs.CreateOrb(Spawn.RandomColor(), newPosition, 30);
            }
            _timer = 0;
        }
    }

    void ChangeGravity()
    {
        if (Input.GetKeyDown(Key.LEFT)) { _gravity = new Vec2(-2, 0); }
        if (Input.GetKeyDown(Key.RIGHT)) { _gravity = new Vec2(2, 0); }
        if (Input.GetKeyDown(Key.UP)) { _gravity = new Vec2(0, -2); }
        if (Input.GetKeyDown(Key.DOWN)) { _gravity = new Vec2(0, 2); }
    }


}

