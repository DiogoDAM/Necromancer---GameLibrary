namespace Necromancer;

public class TiledTileset
{
	public int FirstGid { get; set; }
	public string Source { get; set; }
	public int TileWidth { get; set; }
	public int TileHeight { get; set; }
	public int Columns { get; set; }
	public TextureAtlas Atlas { get; set; }
}
