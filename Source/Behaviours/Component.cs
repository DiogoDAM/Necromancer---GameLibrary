using System;

namespace Necromancer;

public abstract class Component : IDisposable
{
	public bool Disposed { get; protected set; }
	public bool IsActive { get; protected set; }
	public bool IsDrawable { get; protected set; }

	public Entity Entity;

	public Component(Entity e)
	{
	}

	public virtual void Start()
	{
		Active();
	}

	public virtual void PreUpdate(Time time)
	{
	}

	public virtual void Update(Time time)
	{
	}

	public virtual void AfterUpdate(Time time)
	{
	}

	public virtual void Draw()
	{
	}

	public virtual void Active()
	{
		IsActive = true;
		IsDrawable = true;
	}

	public virtual void Desactive()
	{
		IsActive = false;
		IsDrawable = false;
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
				Desactive();
				Disposed = true;
			}
		}
	}
}
