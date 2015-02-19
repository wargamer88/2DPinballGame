using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;

namespace GXPEngine
{
    class GameOverMenu : GameObject
    {
        private bool _allowDestruction = false;

        public bool AllowDestruction { get { return _allowDestruction; } }

        private Sprite _menu;
        private float _score = 0;

        public GameOverMenu(float Score)
        {
            _score = Score;
            _menu = new Sprite(@"Assets\Menu\Gameover.png");
            AddChild(_menu);
        }

        void Update()
        {
            if (Input.GetKey(Key.M))
            {
                _allowDestruction = true;
            }
            if (Input.GetKey(Key.H))
            {
                _menu.Destroy();
                _menu = null;
                _menu = new Sprite(@"Assets\Menu\Highscore.png");
                AddChild(_menu);
                Console.Clear();
            }
        }
    }
}
