using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace Necromancer;

public class TiledObject
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Type { get; set; }
	public float X { get; set; }
	public float Y { get; set; }
	public float Width { get; set; }
	public float Height { get; set; }
	public Dictionary<string, string> Properties { get; set; }

	public TiledObject()
	{
		Properties = new Dictionary<string, string>();
	}

	public Rectangle GetBounds()
	{
		return new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
	}
}
