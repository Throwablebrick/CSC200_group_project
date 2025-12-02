using System;
using Microsoft.Xna.Framework;

namespace MonoGameLibrary;

public readonly struct Circle : IEquatable<Circle>
{
	//the x coord for the center
	public readonly int X;
	//the y coord for the center
	public readonly int Y;
	//the radius in pixels
	public readonly int Radius;

	//get the location of the center
	public readonly Point Location => new Point(X,Y);
	//circle with 0 for all fields
	public static Circle Empty => s_empty;
	//check if it's empty
	public readonly bool IsEmpty => X == 0 && Y == 0 && Radius == 0;

	//get the highest y
	public readonly int Top => Y - Radius;
	//get lowest y
	public readonly int Bottom => Y + Radius;
	//get leftmost x
	public readonly int Left => X - Radius;
	//get rightmost x
	public readonly int Right => X + Radius;

	//circle at a position and Radius
	public Circle(int x, int y, int radius)
	{
		X = x;
		Y = y;
		Radius = radius;
	}
	//circle at a point and radius
	public Circle(Point p, int radius)
	{
		X = p.X;
		Y = p.Y;
		Radius = radius;
	}
	//does it intersect yes or no
	public bool Intersects(Circle other)
	{
		int radiiSquared = (this.Radius + other.Radius) * (this.Radius + other.Radius);
		float distanceSquared = Vector2.DistanceSquared(this.Location.ToVector2(), other.Location.ToVector2());
		return distanceSquared < radiiSquared;
	}
}
