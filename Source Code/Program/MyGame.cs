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
    private GameOverMenu _gameOverMenu;

	public MyGame () : base(1366, 768, false, false)
	{
        _level = new Level();
        AddChild(_level);
	}

	void Update () {
        targetFps = Input.GetMouseButton(0) ? 1600 : 60;

        CheckState();
	}

    void CheckState()
    {
        #region LevelState
        if (_level != null)
        {
            _lastScore = _level.Score;

            if (_level.GameOver)
            {
                _level.Destroy();
                _level = null;

                _gameOverMenu = new GameOverMenu();
                AddChild(_gameOverMenu);
            }
        } 
        #endregion
        if (_gameOverMenu != null)
        {
            if (_gameOverMenu.AllowDestruction)
            {
                _gameOverMenu.Destroy();
                _gameOverMenu = null;

                _level = new Level();
                AddChild(_level);
            } 
        }
    }
}

