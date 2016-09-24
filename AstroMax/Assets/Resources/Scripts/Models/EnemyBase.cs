using UnityEngine;
using System.Collections;

public class EnemyBase : KillableEntityBase, IGraphicObject
{
	private GameObject graphicsObject;
	private float moveSpeed = 5f;

	public EnemyBase(Vector2 position, int maxHitPoints) : base(position, maxHitPoints)
	{
		
	}

	public void SetGameObject(GameObject gameObject)
	{
		this.graphicsObject = gameObject;
		this.graphicsObject.transform.position = this.position;
		this.graphicsObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
	}

	public GameObject ReturnGameObject()
	{
		GameObject obj = this.graphicsObject;
		this.graphicsObject = null;
		return obj;
	}

	public override void Update(float deltaTime, IWorld world)
	{
		this.position -= new Vector2(0, moveSpeed * deltaTime);

		if (this.graphicsObject)
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
		//this.graphicsObject.SetActive(false);
	}
}
