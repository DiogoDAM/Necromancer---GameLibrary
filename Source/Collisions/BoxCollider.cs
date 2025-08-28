using Microsoft.Xna.Framework;

namespace Necromancer;

public sealed class BoxCollider : Collider
{
	public Rectangle Shape => new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

	private int _width;
    public override int Width { get => _width; set => _width = value; }

	private int _height;
    public override int Height { get => _height; set => _height = value; }

    public override Vector2 Center { get => new Vector2((int)(Position.X + Width), (int)(Position.Y + Height)); }

    public BoxCollider(int width, int height) : base()
	{
		Width = width;
		Height = height;
	}

    public override bool Collide(BoxCollider box)
    {
		return Shape.Intersects(box.Shape);
    }

    public override bool Collide(CircleCollider circle)
    {
		return CollisionHelper.CheckRectCircle(Shape, circle.Shape);
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
