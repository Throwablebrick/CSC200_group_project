using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Input;

namespace mgTest;

public class Game1 : Core
{
	private Sprite _slime;
	private AnimatedSprite _jellyfish;
	private Vector2 _jellyfishPosition;
	private const float MOVE_SPEED = 5.0f;

    public Game1() : base("Quinticential game", 1280, 720, false)
    {
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
		TextureAtlas jellyfishSheet = TextureAtlas.FromFile(Content, "images/atlas-definition.xml");
		TextureAtlas slimeSheet = new TextureAtlas(Content.Load<Texture2D>("images/Slime"));

		slimeSheet.AddRegion("lime1", 0, 0, 14, 11);
		_slime = slimeSheet.CreateSprite("lime1");
		_slime.Scale = new Vector2(4.0f, 4.0f);

		_jellyfish = jellyfishSheet.CreateAnimatedSprite("jellyfish-animation");
		_jellyfish.Scale = new Vector2(4.0f, 4.0f);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

		_jellyfish.Update(gameTime);

		CheckKeyboardInput();

        base.Update(gameTime);
    }

	private void CheckKeyboardInput()
	{
		float speed = MOVE_SPEED;

		if (Input.Keyboard.WasKeyJustPressed(Keys.Space))
		{
			speed *= 1.5f;
		}

		if (Input.Keyboard.IsKeyDown(Keys.W) || Input.Keyboard.IsKeyDown(Keys.Up))
		{
			_jellyfishPosition.Y -= speed;
		}

		if (Input.Keyboard.IsKeyDown(Keys.S) || Input.Keyboard.IsKeyDown(Keys.Down))
		{
			_jellyfishPosition.Y += speed;
		}

		if (Input.Keyboard.IsKeyDown(Keys.A) || Input.Keyboard.IsKeyDown(Keys.Left))
		{
			_jellyfishPosition.X -= speed;
		}

		if (Input.Keyboard.IsKeyDown(Keys.D) || Input.Keyboard.IsKeyDown(Keys.Right))
		{
			_jellyfishPosition.X += speed;
		}
	}

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

		// prepare for rendering
		SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
		_slime.Draw(SpriteBatch, Vector2.One);
		_jellyfish.Draw(SpriteBatch, _jellyfishPosition);
		// need to end it when you're done
		SpriteBatch.End();

        base.Draw(gameTime);
    }
}
