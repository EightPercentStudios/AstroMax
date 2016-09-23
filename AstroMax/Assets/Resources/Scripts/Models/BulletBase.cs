using UnityEngine;
using System.Collections;

public class BulletBase : KillableEntityBase
{
	private float moveSpeed;
	private GameObject graphicsObject;

	public BulletBase(Vector2 position, float speed, GameObject gameObject) : base(position, 1)
	{
		this.moveSpeed = speed;
		this.graphicsObject = gameObject;
	}

	public override void Update(float deltaTime, IWorld world)
	{
		this.position += new Vector2(0, moveSpeed * deltaTime);
		this.graphicsObject.transform.position = this.position;

		// Check if dead...
		if (world.IsPointOutOfBounds(this.position))
			this.TakeDamage(this.maxHitPoints + 1);
	}

	public override void Destroy()
	{
		graphicsObject.SetActive(false);
	}
}
