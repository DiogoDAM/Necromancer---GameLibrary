using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Necromancer;

public class TiledMap
{
	public int Width { get; set; }
	public int Height { get; set; }
	public int TileWidth { get; set; }
	public int TileHeight { get; set; }
	public string Orientation { get; set; }
	public List<TiledTileset> Tilesets { get; set; }
	public List<TiledLayer> Layers { get; set; }
	public List<TiledObjectGroup> ObjectGroups { get; set; }

	public TiledMap()
	{
		Tilesets = new List<TiledTileset>();
		Layers = new List<TiledLayer>();
		ObjectGroups = new List<TiledObjectGroup>();
	}
}
