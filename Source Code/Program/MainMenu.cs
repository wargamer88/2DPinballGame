using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class MainMenu : GameObject
    {
        private bool _startLevel = false;

        public bool AllowDestruction { get { return AllowDestruction; } }
        public bool StartLevel { get { return _startLevel; } }       

        private Sprite Menu;

        public  MainMenu()
        {
            Menu = new Sprite(@"Assets\Menu\Menu.png");
            AddChild(Menu);
        }

        void Update()
        {
            if (Input.GetKey(Key.S))
            {
                _startLevel = true;
            }
            if (Input.GetKey(Key.I))
            {
                Menu.Destroy();
                Menu = null;
                Menu = new Sprite(@"Assets\Menu\How to.png");
                AddChild(Menu);
            }
            if (Input.GetKey(Key.M))
            {
                Menu.Destroy();
                Menu = null;
                Menu = new Sprite(@"Assets\Menu\Menu.png");
                AddChild(Menu);
            }
        }
    }
}
