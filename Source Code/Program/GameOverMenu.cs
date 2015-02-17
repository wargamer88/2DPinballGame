using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class GameOverMenu : GameObject
    {
        private int _width = 1366;
        private int _height = 768;
        private bool _allowDestruction = false;

        public bool AllowDestruction { get { return _allowDestruction; } }

        private Sprite Menu;

        public GameOverMenu()
        {
            Menu = new Sprite(@"Assets\Menu\Gameover.png");
            AddChild(Menu);

        }

        void Update()
        {
            if (Input.GetKey(Key.M))
            {
                _allowDestruction = true;
            }
            if (Input.GetKey(Key.H))
            {
                Menu.Destroy();
                Menu = null;
                Menu = new Sprite(@"Assets\Menu\Highscore.png");
                AddChild(Menu);
            }

        }
    }
}
