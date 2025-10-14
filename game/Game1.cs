using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

namespace mgTest;

public class Game1 : Core
{
	private Sprite _slime;
	private AnimatedSprite _jellyfish;
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

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

		// prepare for rendering
		SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
		_slime.Draw(SpriteBatch, Vector2.One);
		_jellyfish.Draw(SpriteBatch, new Vector2((_slime.Width +10), 0));
		// need to end it when you're done
		SpriteBatch.End();

        base.Draw(gameTime);
    }
}
