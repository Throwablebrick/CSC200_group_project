using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Graphics;

namespace mgTest;

public class Platform
{
    public Vector2 WorldPosition;
    public Vector2 ScreenPosition;

    public Rectangle CollisionBox;

    public Sprite Sprite;
	private string sprite_file_path;
	private string sprite_name;

    public Color Color;
	public float scale;

    public Platform(Vector2 position, string path, string name) // Platform constructor with position and size (default white color)
    {
        WorldPosition = position;
        ScreenPosition = Vector2.Zero;
        //CollisionBox = new Rectangle((int)position.X, (int)position.Y, width, height);
        //Color = Color.White;
		sprite_file_path = path;
		sprite_name = name;
		scale = 4.0f;
    }

    public Platform(Vector2 position, string path, string name, float bigness)
    {
        WorldPosition = position;
        ScreenPosition = Vector2.Zero;
        //CollisionBox = new Rectangle((int)position.X, (int)position.Y, width, height);
        //Color = White;
		sprite_file_path = path;
		sprite_name = name;
		scale = bigness;
    }
/*
    public Platform(Vector2 position, string path, string name, float bigness, Color color) // Platform constructor with position, size, and color
    {
        WorldPosition = position;
        ScreenPosition = Vector2.Zero;
        //CollisionBox = new Rectangle((int)position.X, (int)position.Y, width, height);
        Color = color;
		sprite_file_path = path;
		sprite_name = name;
		scale = bigness;
    }
*/
	public void LoadContent(ContentManager content)
	{
		TextureAtlas atlas = TextureAtlas.FromFile(content, sprite_file_path);
		Sprite = atlas.CreateSprite(sprite_name);
		Sprite.Scale = new Vector2(scale, scale);


		CollisionBox = new Rectangle((int)WorldPosition.X, (int)WorldPosition.Y, (int)Sprite.Width, (int)Sprite.Height);
	}

    public void Update() // Update collision box position to match world position 
    {
        CollisionBox.X = (int)WorldPosition.X;
        CollisionBox.Y = (int)WorldPosition.Y;
    }

    public void CalculateScreenPosition(Vector2 cameraPosition) // Calculate screen position from world position and camera position

    {
        ScreenPosition = WorldPosition - cameraPosition;
    }

	public void Draw(SpriteBatch spriteBatch)
	{
		Sprite.Draw(spriteBatch, ScreenPosition);
	}

    public Vector2 GetCenter() // find center of collision box
    {
        return new Vector2(CollisionBox.Center.X, CollisionBox.Center.Y);
    }
}
