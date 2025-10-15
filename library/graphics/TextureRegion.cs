using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameLibrary.Graphics;

public class TextureRegion
{
	public Texture2D Texture { get; set; }

	public Rectangle SourceRectangle { get; set; }

	public int Width => SourceRectangle.Width;
	public int Height => SourceRectangle.Height;

	public TextureRegion() 
	{
	}
	public TextureRegion( Texture2D texture, int x, int y, int width, int height)
	{
		Texture = texture;
		SourceRectangle = new Rectangle(x,y,width,height);
	}

	public void Draw(SpriteBatch spritebatch, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
	{
		Draw(spritebatch, position, color, rotation, origin, new Vector2(scale, scale), effects, layerDepth);
	}
	public void Draw(SpriteBatch spritebatch, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
	{
		spritebatch.Draw(Texture, position, SourceRectangle, color, rotation, origin, scale, effects, layerDepth);
	}
}
