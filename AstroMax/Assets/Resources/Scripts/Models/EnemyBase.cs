using UnityEngine;
using System.Collections;

public class EnemyBase : KillableEntityBase
{
	private GameObject graphicsObject;
	private float moveSpeed = 7.5f;

	public EnemyBase(Vector2 position, int maxHitPoints, GameObject gameObject) : base(position, maxHitPoints)
	{
		this.graphicsObject = gameObject;
	}

	public override void Update(float deltaTime, IWorld world)
	{
		this.position -= new Vector2(0, -moveSpeed);
		this.graphicsObject.transform.position = this.position;

		if (world.IsPointOutOfBounds(this.position))
			this.TakeDamage(maxHitPoints + 1);
	}

	public override void Destroy()
	{
		this.graphicsObject.SetActive(false);
	}
}
