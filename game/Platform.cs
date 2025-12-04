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

    public Color Color;

    public Platform(Vector2 position, int width, int height) // Platform constructor with position and size (default gray color)
    {
        WorldPosition = position;
        ScreenPosition = Vector2.Zero;
        CollisionBox = new Rectangle((int)position.X, (int)position.Y, width, height);
        Color = Color.Gray;
        Sprite = null;
    }

    public Platform(Vector2 position, int width, int height, Color color) // Platform constructor with position, size, and color
    {
        WorldPosition = position;
        ScreenPosition = Vector2.Zero;
        CollisionBox = new Rectangle((int)position.X, (int)position.Y, width, height);
        Color = color;
        Sprite = null;
    }

    public void Update(GameTime gameTime) // Update collision box position to match world position 
    {
        CollisionBox.X = (int)WorldPosition.X;
        CollisionBox.Y = (int)WorldPosition.Y;
    }

    public void CalculateScreenPosition(Vector2 cameraPosition) // Calculate screen position from world position and camera position

    {
        ScreenPosition = WorldPosition - cameraPosition;
    }


    public Vector2 GetCenter() // find center of collision box
    {
        return new Vector2(CollisionBox.Center.X, CollisionBox.Center.Y);
    }
}