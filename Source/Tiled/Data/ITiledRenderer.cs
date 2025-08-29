using Microsoft.Xna.Framework;

namespace Necromancer;

public interface ITiledRenderer
{
	public void DrawMap(TiledMap map, Matrix transformMatrix, Rectangle drawBounds);

	public void DrawLayer(TiledMap map, TiledLayer layer, Matrix transformMatrix, Rectangle drawBounds);
}
