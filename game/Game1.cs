using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;

namespace mgTest;

public class Game1 : Core
{
	private Texture2D _slime;
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
		_slime = Content.Load<Texture2D>("images/Slime");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

		// prepare for rendering
		SpriteBatch.Begin();
		// draw the slime
		SpriteBatch.Draw(_slime, Vector2.Zero, Color.White);
		// need to end it when you're done
		SpriteBatch.End();

        base.Draw(gameTime);
    }
}
