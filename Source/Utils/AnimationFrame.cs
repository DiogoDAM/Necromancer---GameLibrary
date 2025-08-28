using Microsoft.Xna.Framework;

namespace Necromancer;

public struct AnimationFrame
{
	public Rectangle Bounds;
	public float Duration;

	public AnimationFrame(Rectangle bounds, float duration)
	{
		Bounds = bounds;
		Duration = duration;
	}
}
