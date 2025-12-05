using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Input;
using MonoGameLibrary.Scenes;

namespace mgTest;

public class Level : Scene
{
	public Player player;
	public List<Platform> platforms;

	public string LevelPath;

	public Level(string path)
	{
		LevelPath = path
	}

	public override void Initialize()
	{
		platforms = new List<Platform>();
		LoadLevel(LevelPath);
		base.Initialize();
	}

	public override void LoadContent()
	{
		Player.LoadContent();
		foreach (Platform plattt in platforms)
		{
			plattt.LoadContent();
		}
	}

	public override void Update(GameTime gameTime)
	{
		Player.UpdatePreCollision(gameTime);

		foreach (Platform plattt in platforms)
		{
			if (Player.hitbox.Intersects(plattt.CollisionBox))
			{
				Player.WorldPosition -= Player.Velocity;
			}
			plattt.Update();
		}

		Player.UpdatePostCollision(gameTime);
		Player.UpdateCamera(1280,720);//see if I can do this without magic numbers later
		base.Update(gameTime);
	}

	public override void Draw(GameTime gameTime)
	{
		Core.GraphicsDevice.Clear(Color.CornflowerBlue);
		Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

		Player.CalculateScreenPosition();
		Player.Draw(Core.SpriteBatch);

		foreach (Platform plattt in platforms)
		{
			plattt.CalculateScreenPosition();
			plattt.Draw(Core.SpriteBatch);
		}

		Core.SpriteBatch.End();
		base.Draw(gameTime);
	}

	private void LoadLevel(string levelPath)
	{
		string filePath = Path.Combine(Content.RootDirectory, levelPath);

		try
		{
			using (StreamReader reader = new StreamReader(filePath))
			{
				string line;

				while ((line = reader.ReadLine()) != null)
				{
					if (line == "Player")
					{
						player = new Player(new Vector2(float.Parse(reader.ReadLine()), float.Parse(reader.ReadLine())));
					}
					if (line == "Platform")
					{
						Vector2 v = new Vector2(float.Parse(reader.ReadLine()), float.Parse(reader.ReadLine()));
						Platform p = new Platform(v, reader.ReadLine(), reader.ReadLine());
						platforms.Add(p);
					}
				}
			}
		}
		catch(Exception e)
		{
			Console.WriteLine("file could not be read");
			Console.WriteLine(e.Message);
		}
	}
}
