using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
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
	public Player Player;
	public List<Platform> platforms;

	public string LevelPath;

	public Level(string path)
	{
		LevelPath = path;
	}

	public override void Initialize()
	{
		platforms = new List<Platform>();
		LoadLevel(LevelPath);
		base.Initialize();
	}

	public override void LoadContent()
	{
		Player.LoadContent(Core.Content);
		foreach (Platform plattt in platforms)
		{
			plattt.LoadContent(Core.Content);
		}
	}

	public override void Update(GameTime gameTime)
	{
		Player.UpdatePreCollision(gameTime);

		Player.WorldPosition += Player.Velocity;
		Player.hitbox.X = (int)Player.WorldPosition.X;
		Player.hitbox.Y = (int)Player.WorldPosition.Y;

		Player.OnGround = false;

		foreach (Platform plattt in platforms)
		{
			if (Player.hitbox.Intersects(plattt.CollisionBox))
            {
                if (Player.Velocity.Y > 0 && Player.hitbox.Bottom > plattt.CollisionBox.Top)
				{
					Player.WorldPosition.Y = plattt.CollisionBox.Top - Player.hitbox.Height;
					Player.Velocity.Y = 0;
					Player.OnGround = true;
                }

				else if (Player.Velocity.Y < 0)
                {
                    Player.WorldPosition.Y = plattt.CollisionBox.Bottom;
					Player.Velocity.Y = 0;
                }

				else if (Player.Velocity.X != 0)
                {
                    if (Player.Velocity.X > 0)
                    {
                        Player.WorldPosition.X = plattt.CollisionBox.Left - Player.hitbox.Width;
					}
					else
					{
						Player.WorldPosition.X = plattt.CollisionBox.Right;
                    }
					
					Player.Velocity.X = 0;
                }

				Player.hitbox.X = (int)Player.WorldPosition.X;
				Player.hitbox.Y = (int)Player.WorldPosition.Y;
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
			plattt.CalculateScreenPosition(Player.CameraPosition);
			plattt.Draw(Core.SpriteBatch);
		}

		Core.SpriteBatch.End();
		base.Draw(gameTime);
	}
//how this file format is formatted
//copy from the existing level1.xml file and keep the file in the levels directory
//edit the player's starting location and don't add anything else there
//for platforms just copy the Platform tag for as many times as you need platforms
//you can change the x and y values for it's position
//setting the atlasPath and platformName values need to coincide with an edit to a coresponding .xml file in images/
//fortunately you don't need to make a new xml file for each differently sized platform
//just edit the platforms.png and add the start and end pixels for that platform to the platforms.xml file
//when you edit the platforms.xml file make sure that you name this new platform region and that you match it with the name(platformName) in the level xml file
//
//if you need an editor to make pixel art in there's this website https://www.pixilart.com/draw
//if you care and want to get fancy you can also get aseprite(it's 20$ but also open source so you can just compile it for free(this is intended))
//though that's easier said than done but fyi that's an option.
//the scale should *probably* stay at 4. It should at least be consistent between all the art
	private void LoadLevel(string levelPath)
	{
		string filePath = Path.Combine(Content.RootDirectory, levelPath);

		try
		{
			using (Stream stream = TitleContainer.OpenStream(filePath))
			{
				using(XmlReader reader = XmlReader.Create(stream))
				{
					XDocument doc = XDocument.Load(reader);
					XElement root = doc.Root;

					var layers = root.Element("Players")?.Elements("Player"); //this is redundant but I can garentee(ish) it will work
					if (layers != null)
					{
						foreach (var layer in  layers)
						{
							float x = float.Parse(layer.Attribute("x")?.Value ?? "0");
							float y = float.Parse(layer.Attribute("y")?.Value ?? "0");
							Vector2 pos = new Vector2(x,y);
							Player = new Player(pos);
						}
					}

					var plats = root.Element("Platforms")?.Elements("Platform"); 
					if (plats != null)
					{
						foreach (var plat in plats)
						{
							float x = float.Parse(plat.Attribute("x")?.Value ?? "0");
							float y = float.Parse(plat.Attribute("y")?.Value ?? "0");
							Vector2 pos = new Vector2(x,y);
							string atlasPath = plat.Attribute("atlasPath")?.Value;
							string platName = plat.Attribute("platformName")?.Value;
							float scale = float.Parse(plat.Attribute("scale")?.Value ?? "4");

							Platform p = new Platform(pos, atlasPath, platName, scale);
							platforms.Add(p);
						}
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
