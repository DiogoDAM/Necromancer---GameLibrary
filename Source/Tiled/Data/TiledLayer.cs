namespace Necromancer;

public class TiledLayer
{
	public int Id { get; set; }
	public string Name { get; set; }
	public int Width { get; set; }
	public int Height { get; set; }
	public float Opacity { get; set; }
	public string Class { get; set; }
	public string Type { get; set; }
	public bool Visible { get; set; }
	public int[] Data { get; set; }
}
