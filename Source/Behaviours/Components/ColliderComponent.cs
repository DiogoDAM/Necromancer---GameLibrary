using Microsoft.Xna.Framework;

namespace Necromancer;

public sealed class ColliderComponent : Component 
{
	public Collider Collider;

	public ColliderComponent(Entity e) : base(e)
	{
	}

	public void SetCollider(Collider col) => Collider = col;

	public override void Update(Time time)
	{
		Collider.Position = Entity.Transform.Position;
	}
}
