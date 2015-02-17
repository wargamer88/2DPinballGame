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

        private Sprite Menu;

        public GameOverMenu()
        {
            Menu = new Sprite(@"Assets\Menu\Gameover.png");
            AddChild(Menu);

        }
    }
}
