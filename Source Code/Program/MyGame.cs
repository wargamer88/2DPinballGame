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

    private MainMenu _menu;
    private Level _level;
    private float _lastScore;
    private GameOverMenu _gameOverMenu;

	public MyGame () : base(1366, 768, false, false)
	{
        SoundManager.PlayMusic(Music.MENU);
        _menu = new MainMenu();
        AddChild(_menu);

        SoundManager.onAfterFadeMusic += checkstatus;

        
	}

	void Update () {
        targetFps = Input.GetMouseButton(0) ? 1600 : 60;

        CheckState();
	}

    void CheckState()
    {
        #region Menu
        if (_menu != null)
        {
            if (_menu.StartLevel)
            {
                _menu.Destroy();
                _menu = null;

                _level = new Level();
                AddChild(_level);
                SoundManager.StopMusic(true);
            }
        } 
        #endregion
        #region LevelState
        if (_level != null)
        {
            _lastScore = _level.Score;

            if (_level.GameOver)
            {
                _level.Destroy();
                _level = null;
                SoundManager.StopMusic(true);
                

                _gameOverMenu = new GameOverMenu(_lastScore);
                AddChild(_gameOverMenu);
            }
            if (_level.BackToMenu)
            {
                _level.Destroy();
                _level = null;
                SoundManager.StopMusic(true);

                _menu = new MainMenu();
                AddChild(_menu);
            }
        } 
        #endregion
        #region GameOverMenu
        if (_gameOverMenu != null)
        {
            if (_gameOverMenu.AllowDestruction)
            {
                _gameOverMenu.Destroy();
                _gameOverMenu = null;

                _menu = new MainMenu();
                AddChild(_menu);
            }
            else if (_gameOverMenu.StartLevel)
            {
                _gameOverMenu.Destroy();
                _gameOverMenu = null;

                _level = new Level();
                AddChild(_level);
                SoundManager.StopMusic(true);
            }
        } 
        #endregion
    }

    void checkstatus(string action)
    {
        if (action == "musicFaded")
        {
            if (_level != null)
            {
                SoundManager.PlayMusic(Music.INGAME);
            }
            if (_gameOverMenu != null || _menu != null)
            {
                SoundManager.PlayMusic(Music.MENU);
            }
        }
    }
}

