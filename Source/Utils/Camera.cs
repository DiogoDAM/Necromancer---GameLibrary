using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Necromancer;

public class Camera
{
	public Transform Transform { get; private set; }
	public Viewport Viewport;

	public Rectangle BoundingRectangle => new Rectangle((int)Transform.Position.X, (int)Transform.Position.Y, Viewport.Width, Viewport.Height);

	public Camera(int width, int height)
	{
		Transform = new();
		Viewport = new();
		Viewport.Width = width;
		Viewport.Height = height;
	}

	public Camera(Transform parent, int width, int height)
	{
		Transform = new();
		Transform.Parent = parent;
		Viewport = new();
		Viewport.Width = width;
		Viewport.Height = height;
	}

	public Matrix GetMatrix() => Transform.CreateGlobalMatrix();

	public void LookAt(Vector2 Position)
	{
		Transform.Position = Position;
	}

	public void Move(Vector2 moveValue)
	{
		Transform.Position += moveValue;
	}

	public void ZoomIn(Vector2 zoom)
	{
		Transform.Scale += zoom;
	}

	public void ZoomOut(Vector2 zoom)
	{
		Transform.Scale -= zoom;
	}

	public void Rotate(float rotation)
	{
		Transform.Rotation += rotation;
	}
}
