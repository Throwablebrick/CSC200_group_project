using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Input;

namespace mgTest;

public class Player
{
	private const double MAXSPEED = 6.0f;
	private const double ACCELERATE = 0.1f;
	private const double DECELERATE = 0.15f;
	private const double GRAVITY = 250.0f;
	private const double JUMP = 1000.0f;

	public int X;
	public int Y;
	public Vector2 velocity;

	public Rectangle hitbox;
	public Point Camera;
	public Point Middle;
	public Point screenPos;

	private string atlas_path = "images/atlas-definition.xml"
	private string animation_name = "jellyfish-animation"
	private TextureAtlas atlas
	public AnimatedSprite sprite;

	public bool OnGround;

	public Player()
	{
		X = 0;
		Y = 0;
		velocity = new Vector2(0,0);
		hitbox = new Rectangle(0,0,128,128);//change to be sprite dimenstions when a sprite is implimented
		Middle = new Point(GraphicsDevice.PresentationParameters.BackBufferWidth/2, GraphicsDevice.PresentationParameters.BackBufferHeight/2)
		Camera = new Point(-Middle.X ,-Middle.Y);
		screenPos = new Point(X - Camera.X ,Y - Camera.Y);
	}
	public Player(int x, int y, int width, int height,)//set position of player(and rectangle) and rectangle width height. Don't know what values will be useful initally.
	{
		X = x;
		Y = y;
		velocity = new Vector2(0,0);
		hitbox = new Rectangle(x,y, width, height);
		screenPos = new Point(0,0);
	}

	public void LoadContent(ContentManager content)
	{
		atlas = TextureAtlas.FromFile(content, atlas_path);
		sprite = temp.CreateAnimatedSprite(animation_name);
		sprite.Scale = new Vector2(4.0f, 4.0f);
	}

	public void UpdatePrePhysics(GameTime gameTime, InputManager input)
	{
		velocity.y += GRAVITY;
		KeyboardInput(input);
	}

	public void KeyboardInput(InputManager input)
	{
		bool nothingPressed = true;
		if (input.Keyboard.IsKeyDown(Keys.Space))
		{
			if (OnGround)
			{
				velocity.Y -= JUMP;
			}
		}
		//
		//if (input.Keyboard.IsKeyDown(Keys.W) || input.Keyboard.IsKeyDown(Keys.Up))
		//if (input.Keyboard.IsKeyDown(Keys.S) || input.Keyboard.IsKeyDown(Keys.Down))
		//
		if (input.Keyboard.IsKeyDown(Keys.A) || input.Keyboard.IsKeyDown(Keys.Left))
		{
			if (velocity.X > -MAXSPEED)
			{
				velocity.X -= ACCELERATE;
			}
			nothingPressed = false;
		}

		if (input.Keyboard.IsKeyDown(Keys.D) || input.Keyboard.IsKeyDown(Keys.Right))
		{
			if (velocity.X < MAXSPEED)
			{
				velocity.X += ACCELERATE;
			}
			nothingPressed = false;
		}
		if (nothingPressed)
		{
			if (velocity.X > 0)
			{
				velocity.X -= DECELERATE;
			}
			else if (velocity.x < 0)
			{
				velocity.X += DECELERATE;
			}
		}
	}
}
