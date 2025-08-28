using System;
using System.IO;
using System.Xml;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Necromancer;

public static class TiledLoader
{
	public static string DefaultDirToTextures = "Textures";

	public static TiledMap LoadMap(string path, ContentManager content)
	{
		TiledMap map = new();

		XmlDocument xmlDoc = new();
		xmlDoc.Load(Path.Combine(content.RootDirectory, path));

		XmlNode mapNode = xmlDoc.SelectSingleNode("map");
		map.Width = int.Parse(mapNode.Attributes["width"].Value);
		map.Height = int.Parse(mapNode.Attributes["height"].Value);
		map.TileWidth = int.Parse(mapNode.Attributes["tilewidth"].Value);
		map.TileHeight = int.Parse(mapNode.Attributes["tileheight"].Value);

		//Get orientation and set the renderer
		map.Orientation = mapNode.Attributes["orientation"].Value;
		switch(map.Orientation)
		{
			case "orthogonal": map.Renderer = new TiledOrthogonalRenderer(); break;
			default: throw new Exception($"TiledMap Orientation not supported: {map.Orientation}");
		}

		XmlNodeList tilesetsNode = mapNode.SelectNodes("tileset");
		foreach(XmlNode tileset in tilesetsNode)
		{
			LoadTileset(tileset, map, content);
		}

		XmlNodeList layersNode = mapNode.SelectNodes("layer");
		foreach(XmlNode layer in layersNode)
		{
			map.Layers.Add(LoadLayer(layer));
		}

		return map;
	}

	private static void LoadTileset(XmlNode tilesetNode, TiledMap map, ContentManager content)
	{
		TiledTileset tileset = new();

		tileset.FirstGid = int.Parse(tilesetNode.Attributes["firstgid"].Value);
		tileset.Source = tilesetNode.Attributes["source"].Value;

		//To load Texture
		tileset.Source = tileset.Source.Substring(0, tileset.Source.Length-4);
		tileset.Atlas = new(content.Load<Texture2D>(Path.Combine(DefaultDirToTextures, tileset.Source)), map.TileWidth, map.TileHeight);

		map.Tilesets.Add(tileset);
	}

	private static TiledLayer LoadLayer(XmlNode layerNode)
	{
		TiledLayer layer = new();

		layer.Width = int.Parse(layerNode.Attributes["width"].Value);
		layer.Height = int.Parse(layerNode.Attributes["height"].Value);
		layer.Name = layerNode.Attributes["name"].Value;
		// layer.Type = layerNode.Attributes["type"].Value;
		layer.Visible = (layerNode.Attributes["visible"] != null) ? bool.Parse(layerNode.Attributes["visible"].Value) : true;
		layer.Opacity = (layerNode.Attributes["opacity"] != null) ? float.Parse(layerNode.Attributes["opacity"].Value) : 1.0f;
		layer.Class = (layerNode.Attributes["class"] != null) ? layerNode.Attributes["class"].Value : "None";
		layer.Id = int.Parse(layerNode.Attributes["id"].Value);

		// if(layer.Type == "tilelayer")
		// {
			XmlNode dataNode = layerNode.SelectSingleNode("data");
			layer.Data = ParseCSV(dataNode, layer.Width * layer.Height);
		// }

		return layer;
	}

	private static int[] ParseCSV(XmlNode dataNode, int dataLength)
	{
		string valuesText = dataNode.InnerText;
		string[] values = valuesText.Split(new[] {",", "\r", "\n"}, System.StringSplitOptions.RemoveEmptyEntries);
		int[] data = new int[dataLength];

		for(int i=0; i<dataLength; i++)
		{
			data[i] = int.Parse(values[i].Trim());
		}

		return data;
	}
}
