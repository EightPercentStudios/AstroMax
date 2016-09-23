using UnityEngine;
using System.Collections;

public class EnemyBase : KillableEntityBase
{
	private GameObject graphicsObject;
	private float moveSpeed = 5f;

	public EnemyBase(Vector2 position, int maxHitPoints, GameObject gameObject) : base(position, maxHitPoints)
	{
		this.graphicsObject = gameObject;
		this.graphicsObject.transform.position = this.position;
	}

	public override void Update(float deltaTime, IWorld world)
	{
		this.position -= new Vector2(0, moveSpeed * deltaTime);
		this.graphicsObject.transform.position = this.position;

		if (world.IsPointOutOfBounds(this.position)) {
			this.TakeDamage(maxHitPoints + 1);
		}
	}

	public override int TakeDamage(int damagePoints)
	{
		this.graphicsObject.GetComponentInChildren<SpriteRenderer>().color = Color.gray * (this.hitPoints / (float)this.maxHitPoints);
		return base.TakeDamage(damagePoints);
	}

	public override void Destroy()
	{
		this.graphicsObject.SetActive(false);
	}
}
