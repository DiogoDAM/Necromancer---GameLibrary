using System;
using System.Collections;
using System.Collections.Generic;

public class ObjectManager<T> : IEnumerable, IEnumerable<T>, ICollection<T> where T : IDisposable
{
	public int MaxSize { get; set; }

	private List<T> _objects;
	private Queue<T> _toAdd;
	private Queue<(T, bool)> _toRemove;

	public int Count { get; private set; }

    public bool IsReadOnly => false;

    public T this[int index] { 
		get { if(index < 0 || index >= Count) throw new IndexOutOfRangeException(); return _objects[index];} 
		set { if(index < 0 || index >= Count) throw new IndexOutOfRangeException(); _objects[index] = value;}
	}

    public ObjectManager()
	{
		_objects = new();
		_toAdd = new();
		_toRemove = new();

		MaxSize = 64;
	}

	public void Add(T item)
	{
		if(Count == MaxSize) return;

		_toAdd.Enqueue(item);
		Count++;
	}

	public bool Remove(T item)
	{
		if(!_objects.Contains(item)) return false;

		_toRemove.Enqueue((item, false));
		Count--;
		return true;
	}

	public bool Free(T item)
	{
		if(!_objects.Contains(item)) return false;

		_toRemove.Enqueue((item, true));
		Count--;
		return true;
	}

	public bool Contains(T item)
	{
		return _objects.Contains(item);
	}

	public T Find(Predicate<T> predicate)
	{
		return _objects.Find(predicate);
	}

	public List<T> FindAll(Predicate<T> predicate)
	{
		return _objects.FindAll(predicate);
	}

    public void CopyTo(T[] array, int arrayIndex)
    {
		for(int i=arrayIndex; i<array.Length; i++)
		{
			if(Count == MaxSize) break;

			_toAdd.Enqueue(array[i]);
			Count++;
		}
    }

	public void Clear()
	{
		_objects.Clear();
	}

	public void ProcessAddAndRemove()
	{
		for(int i=0; i<_toAdd.Count; i++)
		{
			var item = _toAdd.Dequeue();
			_objects.Add(item);
		}

		for(int i=0; i<_toRemove.Count; i++)
		{
			var pair = _toRemove.Dequeue();
			T item = pair.Item1;
			bool disposable = pair.Item2;

			_objects.Remove(item);

			if(disposable) item.Dispose();
		}
	}

    public IEnumerator GetEnumerator()
    {
		return _objects.GetEnumerator(); 
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
		return _objects.GetEnumerator(); 
    }

}
