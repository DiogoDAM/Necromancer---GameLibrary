using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Necromancer;

public class TextureAtlas
{
	private Dictionary<object, Rectangle> _regions;

	public Texture2D Texture { get; set; }
	public Rectangle SourceRectangle { get { return Texture.Bounds; } }

	public Rectangle this[object index] 
	{
		get 
		{
			if(!_regions.ContainsKey(index)) throw new KeyNotFoundException("index doesn't exist  in TextureAtlas");

			return _regions[index];
		}
	}

	public TextureAtlas(Texture2D texture)
	{
		Texture = texture;
		_regions = new();
	}

	public void AddRegion(object name, int x, int y, int w, int h)
	{
		_regions.Add(name, new Rectangle(x, y, w, h));
	}

	public TextureAtlas(Texture2D texture, int tileWidth, int tileHeight)
	{
		_regions = new();
		Texture = texture;

		int cols = texture.Width / tileWidth;
		int rows = texture.Height / tileHeight;

		for(int row=0; row<rows; row++)
		{
			for(int col=0; col<cols; col++)
			{
				int index = col + row * cols;
				_regions.Add(index, new Rectangle(col * tileWidth, row * tileHeight, tileWidth, tileHeight));
			}
		}
	}

	public Rectangle GetRegion(string name)
	{
		if(!_regions.ContainsKey(name)) throw new KeyNotFoundException();

		return _regions[name];
	}

	public Sprite CreateSprite(string name)
	{
		if(!_regions.ContainsKey(name)) throw new KeyNotFoundException();

		return new Sprite(Texture, _regions[name]);
	}

	public AnimatedSprite CreateAnimatedSprite(string name, int sourcewidth, int sourceheight, bool isLoop=false, float defaultFramesDuration=0.1f)
	{
		if(!_regions.ContainsKey(name)) throw new KeyNotFoundException();

		return new AnimatedSprite(
				Texture,
				_regions[name],
				_regions[name].X,
				_regions[name].Y,
				sourcewidth,
				sourceheight,
				_regions[name].Width,
				_regions[name].Height,
				isLoop,
				defaultFramesDuration);
	}

	public AnimatedSprite CreateAnimatedSprite(string name, int sourcewidth, int sourceheight, bool isLoop=false, params float[] framesDuration)
	{
		if(!_regions.ContainsKey(name)) throw new KeyNotFoundException();

		return new AnimatedSprite(
				Texture,
				_regions[name],
				_regions[name].X,
				_regions[name].Y,
				sourcewidth,
				sourceheight,
				_regions[name].Width,
				_regions[name].Height,
				isLoop,
				framesDuration);
	}

	public static TextureAtlas CreateFromFile(ContentManager content, string filename)
	{
		TextureAtlas atlas;

		string filepath = Path.Combine(content.RootDirectory, filename);

		XDocument doc = XDocument.Load(filepath);
		XElement root = doc.Root;

		string texturePath = root.Element("Texture").Value;
		atlas = new(content.Load<Texture2D>(texturePath));

		var regions = root.Element("Regions").Elements("Region");

		if(regions != null)
		{
			foreach(var region in regions)
			{
				atlas.AddRegion(
						region.Attribute("name").Value,
						int.Parse(region.Attribute("x")?.Value ?? "0"),
						int.Parse(region.Attribute("y")? .Value ?? "0"),
						int.Parse(region.Attribute("width")?.Value ?? "0"),
						int.Parse(region.Attribute("height")?.Value ?? "0")
						);
			}
		}

		return atlas;
	}
}
