using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Input;

namespace mgTest;

public class Player
{
	private const float MAX_SPEED = 6.0f;
	private const float ACCELERATION = 0.5f;
	private const float DECELERATION = 0.15f;
	private const float GRAVITY = 150.0f;
	private const float JUMP_FORCE = 30.0f;
	private const float CAMERA_SMOOTHING = 0.1f;

	public Vector2 Velocity;
	public Vector2 WorldPosition; // position in game world
	public Vector2 ScreenPosition; // where to draw on screen. Calculated from world position and camera
	public Vector2 CameraPosition; 

	public Rectangle hitbox; // for detection (in world coordinates)

	public AnimatedSprite Sprite;

	public bool OnGround; // flags


	private string atlas_path = "images/atlas-definition.xml";
	private string animation_name = "jellyfish-animation";

	public Player()
	{
		WorldPosition = Vector2.Zero;
		ScreenPosition = Vector2.Zero;
		Velocity = Vector2.Zero;
		//hitbox = new Rectangle(0,0,64,68);
		CameraPosition = Vector2.Zero;
        OnGround = false;
    }
	public Player(Vector2 startPosition)//set position of player(and rectangle) and rectangle width height. Don't know what values will be useful initally.
	{
		WorldPosition = startPosition;
		Velocity = Vector2.Zero;
		//hitbox = new Rectangle((int)startPosition.X, (int)startPosition.Y, width, height); should be the width and height of the sprite which is only loaded in in LoadContent so will be set there
		CameraPosition = Vector2.Zero;
		ScreenPosition = startPosition - CameraPosition;
		OnGround = false;
	}

	public void LoadContent(ContentManager content)
	{
		TextureAtlas atlas = TextureAtlas.FromFile(content, atlas_path);
		Sprite = atlas.CreateAnimatedSprite(animation_name);
		Sprite.Scale = new Vector2(4.0f, 4.0f);
		
		hitbox = new Rectangle((int)WorldPosition.X, (int)WorldPosition.Y, (int)Sprite.Width, (int)Sprite.Height);
	}

	public void UpdatePreCollision(GameTime gameTime)
	{
		Sprite.Update(gameTime);

		Velocity.Y += GRAVITY * (float)gameTime.ElapsedGameTime.TotalSeconds;

		HandleInput();
	}

	public void UpdatePostCollision(GameTime gameTime)
	{
		WorldPosition += Velocity;

		// Update hitbox to match player position
		hitbox.X = (int)WorldPosition.X;
		hitbox.Y = (int)WorldPosition.Y;
	}

	public void UpdateCamera(int screenWidth, int screenHeight)
	{
		Vector2 targetCameraPosition = new Vector2(
			WorldPosition.X - screenWidth / 2 + hitbox.Width / 2,
			WorldPosition.Y - screenHeight / 2 + hitbox.Height / 2
		);

		CameraPosition = Vector2.Lerp(CameraPosition, targetCameraPosition, CAMERA_SMOOTHING);
	}
	
	// Calculate screen position from world position and camera
	public void CalculateScreenPosition()
	{
		ScreenPosition = WorldPosition - CameraPosition;
	}
	private void HandleInput()
	{
		bool movingHorizontally = false;

		//jump
		if (Core.Input.Keyboard.WasKeyJustPressed(Keys.Space) && OnGround)
		{
			Velocity.Y = -JUMP_FORCE;
			OnGround = false;
		}

		//Left
		if (Core.Input.Keyboard.IsKeyDown(Keys.A) || Core.Input.Keyboard.IsKeyDown(Keys.Left))
		{
			if (Velocity.X > -MAX_SPEED)
			{
				Velocity.X -= ACCELERATION;
			}
			movingHorizontally = true;
		}

		// Right
		if (Core.Input.Keyboard.IsKeyDown(Keys.D) || Core.Input.Keyboard.IsKeyDown(Keys.Right))
		{
			if (Velocity.X < MAX_SPEED)
			{
				Velocity.X += ACCELERATION;
			}
			movingHorizontally = true;
		}

		//Creating deceleration 
		if (!movingHorizontally)
		{
			if (Velocity.X > 0)
			{
				Velocity.X -= DECELERATION;
				if (Velocity.X < 0) Velocity.X = 0;
			}
			else if (Velocity.X < 0)
			{
				Velocity.X += DECELERATION;
				if (Velocity.X > 0) Velocity.X = 0;
			}
		}
	}

	public void Draw(SpriteBatch spriteBatch)
	{
		Sprite.Draw(spriteBatch, ScreenPosition);
	}
	
	public Vector2 GetCenter()
    {
		return new Vector2(hitbox.Center.X, hitbox.Center.Y);
    }
}
