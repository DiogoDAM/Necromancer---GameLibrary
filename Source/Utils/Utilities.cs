using System;
using Microsoft.Xna.Framework;

namespace Necromancer 
{
	public static class Utilities
	{
		public static Random Random = new Random();

		//Cartesian methods world and map

		public static Vector2 MapToWorld(int mapx, int mapy, int tileWidth, int tileHeight)
		{
			return new Vector2(mapx * tileWidth, mapy * tileHeight);
		}

		public static Vector2 WorldToMap(Vector2 worldPos, int tileWidth, int tileHeight)
		{
			return new Vector2((float)Math.Floor(worldPos.X / tileWidth), (float)Math.Floor(worldPos.Y / tileHeight));
		}

		//Isometric methods world and map 

		public static Vector2 MapToIsometricWorld(int mapx, int mapy, int tileWidth, int tileHeight)
		{
			return new Vector2((mapx - mapy) * (tileWidth/2), (mapx + mapy) * (tileHeight/2));
		}

		public static Vector2 MapToIsometricWorld(Point map, int tileWidth, int tileHeight)
		{
			return new Vector2((map.X - map.Y) * (tileWidth/2)-16, (map.X + map.Y) * (tileHeight/2)-20);
		}

		public static Vector2 WorldToIsometricMap(Vector2 worldPos, int tileWidth, int tileHeight)
		{
			float mapX = (worldPos.X / (tileWidth/2) + worldPos.Y / (tileHeight/2) ) / 2;
			float mapY = (worldPos.Y / (tileHeight/2) - worldPos.X / (tileWidth/2) ) / 2;

			return new Vector2((float)Math.Floor(mapX + .5f), (float)Math.Floor(mapY + .5f));
		}

		public static float RandomFloat(float min, float max)
		{
			return (float)(Random.NextDouble() * (max - min) + min);
		}
	}
}
