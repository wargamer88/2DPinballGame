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

    private Level _level;
    private float _lastScore;
    private bool _gameOver = false;
    private GameOverMenu _gameOverMenu;

	public MyGame () : base(1366, 768, false, false)
	{
        _level = new Level();
        AddChild(_level);
	}

	void Update () {
        targetFps = Input.GetMouseButton(0) ? 1600 : 60;

        if (_level != null)
        {
            _lastScore = _level.Score;

            if (_level.GameOver)
            {
                _gameOver = true;
                _level.Destroy();
                _level = null;
            }
        }
        if (_gameOver)
        {
            _gameOverMenu = new GameOverMenu();
        }
	}
}

