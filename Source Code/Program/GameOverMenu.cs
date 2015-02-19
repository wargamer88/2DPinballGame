using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.VisualBasic;

namespace GXPEngine
{
    class GameOverMenu : GameObject
    {
        private bool _allowDestruction = false;
        private bool _startLevel = false;
        private TextField _txtScore;

        public bool AllowDestruction { get { return _allowDestruction; } }
        public bool StartLevel { get { return _startLevel; } }

        private Sprite _menu;
        private float _score = 0;

        public GameOverMenu(float Score)
        {
            _score = Score;
            _menu = new Sprite(@"Assets\Menu\Gameover.png");
            AddChild(_menu);

            _txtScore = TextField.CreateTextField("0000000000000");
            AddChild(_txtScore);
            _txtScore.text = "" + _score;
            _txtScore.color = 0x1248C4;
            _txtScore.x += 700;
            _txtScore.y += 375;
        }

        void Update()
        {
            if (Input.GetKey(Key.M))
            {
                _allowDestruction = true;
            }
            if (Input.GetKeyDown(Key.R))
            {
                _startLevel = true;
            }
        }
    }
}
