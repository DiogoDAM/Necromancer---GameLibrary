using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Necromancer;

public class SpriteText
{
	public Transform Transform;

	public Vector2 Origin = Vector2.Zero;
	public Color Color = Color.White;
	public float LayerDepth = 0f;
	public SpriteEffects Flip = SpriteEffects.None;

	public string Text;
	public SpriteFont Font;

	public Vector2 TextSize => Font.MeasureString(Text);

	public SpriteText()
	{
		Transform = new();
	}

	public SpriteText(SpriteFont spriteFont, string text)
	{
		Transform = new();
		Font = spriteFont;
		Text = text;
	}

	public SpriteText(SpriteFont font, string text, Transform parent)
	{
		Transform = new();
		Transform.Parent = parent;
		Font = font;
		Text = text;
	}

	public void CenterOrigin()
	{
		Origin.X = TextSize.X * .5f;
		Origin.Y = TextSize.Y * .5f;
	}

	public void Draw()
	{
		NecroGame.SpriteBatch.DrawString(Font, Text, Transform.GlobalPosition, Color, Transform.GlobalRotation, Origin, Transform.GlobalScale, Flip, LayerDepth);
	}

}
