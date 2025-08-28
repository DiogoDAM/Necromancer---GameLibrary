using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace Necromancer;

public class Sprite : IDisposable
{
	public Transform Transform;
	public Rectangle Region;
	public Texture2D Texture;

	public Vector2 Origin = Vector2.Zero;
	public float LayerDepth = 0f;
	public SpriteEffects Flip = SpriteEffects.None;
	public Color Color = Color.White;

	public int Width { get { return Region.Width; }  }
	public int Height { get { return Region.Height; }  }

	public bool Disposed { get; protected set; }

	public Sprite() { }

	public Sprite(Texture2D texture, Rectangle region)
	{
		Region = region;
		Texture = texture;
		Transform = new();
		Transform.Scale = Vector2.One;
	}

	public Sprite(Texture2D texture, Rectangle region, Vector2 pos)
	{
		Region = region;
		Texture = texture;
		Transform = new(pos);
		Transform.Scale = Vector2.One;
	}

	public Sprite(Texture2D texture, Rectangle region, Transform parent)
	{
		Region = region;
		Texture = texture;
		Transform = new();
		Transform.Parent = parent;
		Transform.Scale = Vector2.One;
	}

	public void LookAt(Vector2 target)
	{
		Transform.LookAt(target);
	}

	public void CenterOrigin()
	{
		Origin = new Vector2(Width, Height) * .5f;
	}

	public void FlipH(bool flip) => Flip = (flip) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
	public void FlipV(bool flip) => Flip = (flip) ? SpriteEffects.FlipVertically : SpriteEffects.None;

	public virtual void Draw()
	{
		NecroGame.SpriteBatch.Draw(Texture, Transform.GlobalPosition, Region, Color, Transform.GlobalRotation, Origin, Transform.GlobalScale, Flip, LayerDepth);
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposable)
	{
		if(disposable)
		{
			if(!Disposed)
			{
				Transform = null;
				Disposed = true;
			}
		}
	}
}
