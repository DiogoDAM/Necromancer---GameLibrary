using System;

using Microsoft.Xna.Framework;

namespace Necromancer;

public abstract class Scene : IDisposable
{
	public bool IsDisposed { get; protected set; }
	public bool IsActive { get; protected set; }
	public bool IsDrawable { get; protected set; }

	public Color BackgroundColor { get; set; } = Color.CornflowerBlue;

	public Scene() { }

	public abstract void Load();
	public abstract void Unload();

	public abstract void Start();
	public abstract void Active();
	public abstract void Desactive();

	public abstract void BeforeUpdate(Time time);
	public abstract void Update(Time time);
	public abstract void AfterUpdate(Time time);

	public abstract void Draw();


	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected abstract void Dispose(bool disposable);
}
