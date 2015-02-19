using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class MainMenu : GameObject
    {
        private bool _startLevel = false;
        private Sprite _arrows;
        private Sprite _arrows2;
        private bool _move = false;
		private bool _up = false;
        private bool _down = false;

        public bool AllowDestruction { get { return AllowDestruction; } }
        public bool StartLevel { get { return _startLevel; } }       

        private Sprite Menu;

        public  MainMenu()
        {
            Menu = new Sprite(@"Assets\Menu\Menu.png");
            AddChild(Menu);

            _arrows = new Sprite(@"Assets\Menu\pointers.png");
            AddChild(_arrows);
            _arrows.visible = false;
            _arrows2 = new Sprite(@"Assets\Menu\pointers.png");
            AddChild(_arrows2);
            _arrows2.visible = false;
        }

        void Update()
        {
            Arrows();
            if (_up)
            {

                if (Input.GetKeyDown(Key.SPACE))
                {
                    _startLevel = true;
                }


            }
            if (_down)
            {

                if (Input.GetKeyDown(Key.SPACE))
                {
                    _arrows.Destroy();
                    _arrows = null;
                    _arrows2.Destroy();
                    _arrows2 = null;
                    _move = false;
                    _up = false;
                    _down = false;
                    Menu.Destroy();
                    Menu = null;
                    Menu = new Sprite(@"Assets\Menu\How to.png");
                    AddChild(Menu);
                }
            }
            if (Input.GetKey(Key.M))
            {
                Menu.Destroy();
                Menu = null;
                Menu = new Sprite(@"Assets\Menu\Menu.png");
                AddChild(Menu);
                _arrows = new Sprite(@"Assets\Menu\pointers.png");
                AddChild(_arrows);
                _arrows.visible = false;
                _arrows2 = new Sprite(@"Assets\Menu\pointers.png");
                AddChild(_arrows2);
                _arrows2.visible = false;
            }
        }

        void Arrows()
        {
            if (Input.GetKeyDown(Key.UP))
            {
                Console.WriteLine("UP Pressed");
                _arrows.SetXY(290, 300);
                _arrows2.SetXY(920, 300);
                _arrows2.Mirror(true, false);
                _arrows.visible = true;
                _arrows2.visible = true;
                _up = true;
                _down = false;
                _move = true;
            }

            if (Input.GetKeyDown(Key.DOWN))
            {
                _arrows.SetXY(290, 500);
                _arrows2.SetXY(920, 500);
                _arrows2.Mirror(true, false);
                _arrows.visible = true;
                _arrows2.visible = true;
                _up = false;
                _down = true;
                _move = true;

            }
        }
    }

}
