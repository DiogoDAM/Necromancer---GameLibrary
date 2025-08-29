using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Necromancer;

public sealed class TileMap
{
	public TiledMap Map;
	public ITiledRenderer Renderer;

	public Rectangle DrawBounds;
	public Matrix Matrix;

	public TileMap()
	{
		DrawBounds = new Rectangle(0,0, NecroGame.WindowWidth, NecroGame.WindowHeight);
		Matrix = Matrix.CreateTranslation(0,0,0);
	}

	public void LoadMap(string filePath, ContentManager content)
	{
		Map = TiledLoader.LoadMap(filePath, content);

		switch(Map.Orientation)
		{
			case "orthogonal": Renderer = new TiledOrthogonalRenderer(); break;
			default: throw new Exception($"TiledMap Orientation not supported: {Map.Orientation}");
		}
	}

	public void Update(Matrix transformMatrix, Rectangle drawBounds)
	{
		Matrix = transformMatrix;
		DrawBounds = drawBounds;
	}

	public void Update(Matrix transformMatrix, Camera camera)
	{
		Matrix = transformMatrix;
		DrawBounds = camera.BoundingRectangle;
	}

	public void Draw()
	{
		Renderer.DrawMap(Map, Matrix, DrawBounds);
	}
}
