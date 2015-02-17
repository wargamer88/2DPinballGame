using System;
using GXPEngine;
using System.Drawing;
using System.Drawing.Text;
using System.Collections.Generic;

public class MyGame : Game
{	

	static void Main() {
		new MyGame().Start();
	}
    
    private TextField _txtScore;
    private float _score = 0;
    private TextField _txtMultiplier;
    private float _multiplier = 1;
    private int _multiplierTimer = 66;
    private TextField _txtTimer;
    private int _deathTimer = 0;
    private int _secondTimer = 0;
    private TextField _txtLives;
    private int _lives = 3;
    private bool _gameOver = false;
    private Font _customFont;

    private Orbs _orbs;
    private Crystal _crystal;

	private OuterCircle _outerCircle;
    private Sprite _outerCircleRing;
    private Ball _ball;
    private Collisions _collisions = new Collisions();

    private Vec2 _gravity = new Vec2(0, -0.2f);

	private Vec2 _previousPosition;
	private Canvas _canvas;
    private int _timer = 0; //DEBUG
    private Sprite _background;

    //Effects
    private int _effectsCollisionTimer = 0;
    //-Fire:
    private bool _fireEffect = false;
    private int _fireTimer = 0;
    //-Water:
    private bool _waterEffect = false;
    private int _waterTimer = 0;
    //-Lightning
    private bool _lightningEffect = false;
    private int _lightningTimer = 0;
    //-Wind
    private bool _windEffect = false;
    private int _windTimer = 0;
    //SpawnImmortality
    private bool _spawnImmortality = false;
    private int _immortalTimer = 0;

    private bool _createdForTheFirstTime = false;
    private bool _destroyBall = false;


	public MyGame () : base(1366, 768, false, false)
	{
		_canvas = new Canvas (width, height);
		AddChild (_canvas);

        _outerCircle = new OuterCircle(384, new Vec2(width/2, height/2), Color.Yellow);
        AddChild(_outerCircle);

        _crystal = new Crystal(this, _outerCircle);
        AddChild(_crystal);

        _ball = new Ball(30, new Vec2(width / 2, height / 2), null, _gravity, Color.Green);
        AddChild(_ball);

        _orbs = new Orbs(this);
        AddChild(_orbs);

        _ball.velocity = new Vec2(0, 0);
        _previousPosition = _ball.position.Clone();

        _background = new Sprite("Assets/background screen 2.png");
        AddChild(_background);

        _txtScore = TextField.CreateTextField("Score: 000000000000");
        AddChild(_txtScore);
        _txtScore.text = "Score: " +_score;

        _txtMultiplier = TextField.CreateTextField("Multiplier: 000000000000");
        AddChild(_txtMultiplier);
        _txtMultiplier.text = "Multiplier: " + _multiplier;
        _txtMultiplier.y = _txtScore.height + 1;

        _txtTimer = TextField.CreateTextField("Death Timer: 000000000000");
        AddChild(_txtTimer);
        _deathTimer = 10;
        _secondTimer = 1 * Utils.frameRate;
        _txtTimer.text = "Time Left: " + _deathTimer;
        _txtTimer.x = 1100;

        _txtLives = TextField.CreateTextField("Lives Left: 000000000000");
        AddChild(_txtLives);
        _txtLives.text = "Lives Left: " + _lives;
        _txtLives.x = 1100;
        _txtLives.y = _txtLives.height + 1;

        _outerCircleRing = new Sprite(@"Assets\Solo Ring.png");
        _outerCircleRing.x = _outerCircle.x+6;
        _outerCircleRing.y = _outerCircle.y+1;
        _outerCircleRing.SetScaleXY(1.005f, 1.005f);
        _outerCircleRing.SetOrigin(_outerCircleRing.width / 2, _outerCircleRing.height / 2);
        AddChild(_outerCircleRing);

        
	}

	void Update () {
        if (_gameOver)
        {
            return;
        }
        SoundManager.PlayMusic(Music.INGAME);
		targetFps = Input.GetMouseButton (0) ? 1600 : 60;
        ChangeGravity();
        _orbs.StepOrbs();
        SpawnOrbs();

        _ball.acceleration = _gravity;

        CheckMaxSpeed();
        CheckEffects();
        Collisions();
        _ball.Step();
        _ball.UpdateAnimation();
        _orbs.UpdateOrbAnimations();
        _crystal.UpdateAnimation();
        _outerCircle.GraphicsSprite.rotation += 0.05f;
        HudTimers();
	}

    void HudTimers()
    {
        #region Multiplier
        _multiplierTimer--;
        //Console.WriteLine(_multiplier);

        if (_multiplier >= 1.0f)
        {
            if (_multiplier >= 3.0f)
            {
                _multiplier = 3.0f;
            }
            
            if (_multiplierTimer < 0)
            {
                _multiplier -= 0.1f;
                _multiplierTimer = 66;
            }
            if (_multiplier < 1.0f)
            {
                _multiplier = 1.0f;
            }

        }
        _multiplier = (float)Math.Round(_multiplier, 1);
        _txtMultiplier.text = "Multiplier: " + _multiplier;
        #endregion
        
        #region DeathTimer
        _secondTimer--;
        if (_secondTimer <= 0)
        {
            _deathTimer--;
            if (_deathTimer > 10)
            {
                _deathTimer = 10;
            }
            else if (_deathTimer <= 0)
            {
                _destroyBall = true;
                _deathTimer = 10;
            }
            _secondTimer = 1 * Utils.frameRate;
            _txtTimer.text = "Time Left: " + _deathTimer;
        }
        #endregion
	}

    void CheckEffects()
    {
        #region OrbEffects
        if (_fireEffect == true)
        {
            _fireTimer++;
            if (_fireTimer >= 25)
            {
                _fireEffect = false;
                _fireTimer = 0;
            }
            _ball.velocity.Scale(2.0f);
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
                if (_outerCircleRing.rotation == 0)
                {
                    _gravity = new Vec2(0, -0.2f);
                }
                else if (_outerCircleRing.rotation == 90)
                {
                    _gravity = new Vec2(0.2f, 0);
                }
                else if (_outerCircleRing.rotation == 180)
                {
                    _gravity = new Vec2(0, 0.2f);
                }
                else if (_outerCircleRing.rotation == 270)
                {
                    _gravity = new Vec2(-0.2f, 0);
                }
            }
            else
            {
                _ball.velocity = Vec2.zero;
                _gravity = Vec2.zero;
            }
        }

        if (_windEffect)
        {
            _windTimer++;
            _ball.velocity.Scale(2);
            if (_windTimer >= 25)
            {
                _windEffect = false;
                _windTimer = 0;
            }
        }
        #endregion

        #region RespawnImmortality
        if (_spawnImmortality)
	    {
            _immortalTimer++;

            if (_immortalTimer >= 66)
            {
                _spawnImmortality = false;
                _immortalTimer = 0;
            }
	    }
        #endregion

        #region DestroyBall(DEATH)
        if (_destroyBall)
        {
            _ball.GraphicsSprite.Destroy();
            _ball.GraphicsSprite = new AnimSprite(@"Assets\Light ball death.png", 16, 1);
            _ball.GraphicsSprite.height = _ball.height + 40;
            _ball.GraphicsSprite.width = _ball.width + 40;
            _ball.GraphicsSprite.SetXY(-_ball.radius - 20, -_ball.radius - 20);
            _ball.AddChild(_ball.GraphicsSprite);
            if (!_createdForTheFirstTime)
            {
                _ball.Frame = 3;
                _multiplier = 1;
                _createdForTheFirstTime = true;
            }
            _ball.FirstFrame = 0;
            _ball.LastFrame = 15;
            _ball.FrameSpeed = 0.2;
            _ball.velocity = Vec2.zero;
            _gravity = Vec2.zero;

            if (_ball.Frame >= 15)
            {
                _destroyBall = false;
                _createdForTheFirstTime = false;
                _multiplier = 1;
                _ball.GraphicsSprite.Destroy();
                _ball.Destroy();

                if (_outerCircleRing.rotation == 0)
                {
                    _gravity = new Vec2(0, -0.2f);
                }
                else if (_outerCircleRing.rotation == 90)
                {
                    _gravity = new Vec2(0.2f, 0);
                }
                else if (_outerCircleRing.rotation == 180)
                {
                    _gravity = new Vec2(0, 0.2f);
                }
                else if (_outerCircleRing.rotation == 270)
                {
                    _gravity = new Vec2(-0.2f, 0);
                }


                _ball = new Ball(30, new Vec2(width / 2, height / 2), null, _gravity, Color.Green);
                _lives = _lives - 1;
                if (_lives <= 0)
                {
                    _lives = 0;
                    _gameOver = true;
                }
                _txtLives.text = "Lives Left: " + _lives;
                _deathTimer = 10;
                _spawnImmortality = true;

                AddChild(_ball);
                this.SetChildIndex(_ball, 2); 
            } 
        }
        #endregion
    }

    void Collisions()
    {
        if (_spawnImmortality)
        {
            return;
        }

        #region Ball&Outercirle Collision
        bool hitEdge = _collisions.OuterCircleCollisionTest(_outerCircle, _ball);
        if (hitEdge == true)
        {
            _destroyBall = true;
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
        _effectsCollisionTimer--;
        bool collision = false;
        foreach (Orb orb in _orbs._orbList)
        {
            collision = _collisions.OrbBallCollisionTest(orb, _ball);
            if (collision)
            {
                if (_effectsCollisionTimer <= 0)
                {
                    _multiplier += 0.5f;
                    _deathTimer += 3;
                }
                string color = orb.ballColor.Name;
                switch (color)
                {
                    case "Red":
                        if (_effectsCollisionTimer <= 0)
                        {
                            SoundManager.PlaySound(SoundEffect.FIRE);
                            _effectsCollisionTimer = 66;
                        }
                        _fireEffect = true;

                        break;
                    case "White":
                        if (_effectsCollisionTimer <= 0)
                        {
                            SoundManager.PlaySound(SoundEffect.WIND);
                            _effectsCollisionTimer = 66;
                        }
                        _ball.velocity = new Vec2(Utils.Random(-10, 10), Utils.Random(-10, 10));
                        _windEffect = true;
                        break;
                    case "Cyan":
                        if (_effectsCollisionTimer <= 0)
                        {
                            SoundManager.PlaySound(SoundEffect.LIGHTNING);
                            _effectsCollisionTimer = 66;
                        }
                        _lightningEffect = true;
                        
                        break;
                    case "Brown":
                        //Reflect gravity
                        if (_effectsCollisionTimer <= 0)
                        {
                           SoundManager.PlaySound(SoundEffect.EARTH);
                           _effectsCollisionTimer = 66;
                        }
                        
                        _ball = _collisions.OrbBallCollision(orb, _ball);
                        break;
                    case "Blue":
                        if (_effectsCollisionTimer <= 0)
                        {
                            SoundManager.PlaySound(SoundEffect.WATER);
                            _effectsCollisionTimer = 66;
                        }
                        _waterEffect = true;
                        break;
                }

            }
            else
            {
            }

        }

        #endregion

        #region CrystalCollision
        if (_collisions.BallCollisionTestCrystalBool(_ball, _crystal))
        {
            _crystal.RespawnCrystal();
            float scoreWithMultiplier = 1 * _multiplier;
            _deathTimer += 1;
            _score += scoreWithMultiplier;
            _txtScore.text = "Score: " + _score;
        } 
        #endregion
    }

    void CheckMaxSpeed()
    {
        float maxSpeed = 3;

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
            if (_orbs._orbList.Count < 5)
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
                _orbs.CreateOrb(Spawn.RandomColor(), newPosition, 40);
            }
            _timer = 0;
        }
    }

    void ChangeGravity()
    {
        if (Input.GetKey(Key.LEFT)) { _gravity = new Vec2(-0.2f, 0); _outerCircleRing.rotation = 270; }
        if (Input.GetKey(Key.RIGHT)) { _gravity = new Vec2(0.2f, 0); _outerCircleRing.rotation = 90; }
        if (Input.GetKey(Key.UP)) { _gravity = new Vec2(0, -0.2f); _outerCircleRing.rotation = 0; }
        if (Input.GetKey(Key.DOWN)) { _gravity = new Vec2(0, 0.2f); _outerCircleRing.rotation = 180; }
    }


}

