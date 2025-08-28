using Microsoft.Xna.Framework;

namespace Necromancer;

public sealed class CircleCollider : Collider
{
	public Circle Shape => new Circle((int)Position.X, (int)Position.Y, Radius);

	public int Radius;

	private int _width;
    public override int Width { get => _width; set => _width = value; }

	private int _height;
    public override int Height { get => _height; set => _height = value; }

    public override Vector2 Center { get => Shape.Center; }

	public CircleCollider(int radius) : base()
	{
		Radius = radius;
	}

    public override bool Collide(BoxCollider box)
    {
		return CollisionHelper.CheckRectCircle(box.Shape, Shape);
    }

    public override bool Collide(CircleCollider circle)
    {
		return Shape.Intersects(circle.Shape);
    }

    public override bool Collide(ColliderList list)
    {
		foreach(Collider col in list)
		{
			if(Collide(col)) return true;
		}

		return false;
    }

    public override bool Collide(Vector2 vec)
    {
		return Shape.Contains(vec);
    }
}
