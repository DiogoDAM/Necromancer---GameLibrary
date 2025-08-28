namespace Necromancer;

public interface ITiledRenderer
{
	public void DrawMap(TiledMap map);

	public void DrawLayer(TiledMap map, TiledLayer layer);
}
