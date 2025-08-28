using Microsoft.Xna.Framework;

using System;
using System.Collections;
using System.Collections.Generic;

namespace Necromancer;

public sealed class ColliderList : Collider, IEnumerable, IEnumerable<Collider>
{
    public override int Width { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public override int Height { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override Vector2 Center => throw new NotImplementedException();

	public List<Collider> Colliders;

	public Collider this[int index]
	{
		get { if(index < 0 && index >= Colliders.Count) throw new IndexOutOfRangeException(); return Colliders[index]; }
		set { if(index < 0 && index >= Colliders.Count) throw new IndexOutOfRangeException(); Colliders[index] = value; }
	}

	public ColliderList() : base()
	{
		Colliders = new();
	}

	public void Add(Collider col) => Colliders.Add(col);
	public bool Remove(Collider col) => Colliders.Remove(col);
	public bool Contains(Collider col) => Colliders.Contains(col);
	public T GetType<T>() where T : Collider => (T)Colliders.Find(col => col is T);

    public override bool Collide(BoxCollider box)
    {
		foreach(Collider col in Colliders)
		{
			if(col.Collide(box)) return true;
		}

		return false;
    }

    public override bool Collide(CircleCollider circle)
    {
		foreach(Collider col in Colliders)
		{
			if(col.Collide(circle)) return true;
		}

		return false;
    }

    public override bool Collide(ColliderList list)
    {
		return CollisionHelper.Collide(Colliders, list);
    }

    public override bool Collide(Vector2 vec)
    {
		foreach(Collider col in Colliders)
		{
			if(col.Collide(vec)) return true;
		}

		return false;
    }

    public IEnumerator<Collider> GetEnumerator()
    {
		return Colliders.GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override IPrototype DeepClone()
    {
		ColliderList clone = (ColliderList)MemberwiseClone();

		foreach(Collider col in Colliders)
		{
			clone.Add(col);
		}

		return clone;
    }

	protected override void Dispose(bool disposable)
	{
		if(disposable)
		{
			if(!Disposed)
			{

				foreach(Collider col in Colliders) col.Dispose();
				Colliders.Clear();

				Disposed = true;
			}
		}
	}
}
