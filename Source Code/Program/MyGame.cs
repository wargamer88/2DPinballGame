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

	public MyGame () : base(1366, 768, false, false)
	{
        _level = new Level();
        AddChild(_level);
	}

	void Update () {
        targetFps = Input.GetMouseButton(0) ? 1600 : 60;
	}

    


}

