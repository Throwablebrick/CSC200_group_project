using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Input;
using MonoGameLibrary.Scenes;

namespace mgTest;

public class Game1 : Core
{
    public Game1() : base("Quinticential game", 1280, 720, false)
    {
    }

    protected override void Initialize()
	{
        base.Initialize();

		ChangeScene(new Level("levels/level1.xml"));
    }
}
