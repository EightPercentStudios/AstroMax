using UnityEngine;
using System.Collections;

public class BulletBase : KillableEntityBase, IGraphicObject
{
	private float moveSpeed;
	private GameObject graphicsObject;

	public BulletBase(Vector2 position, float speed) : base(position, 1)
	{
		this.moveSpeed = speed;
	}

	public void SetGameObject(GameObject gameObject)
	{
		this.graphicsObject = gameObject;
	}

	public GameObject ReturnGameObject()
	{
		GameObject obj = this.graphicsObject;
		this.graphicsObject = null;
		return obj;
	}

	public override void Update(float deltaTime, IWorld world)
	{
		this.position += new Vector2(0, moveSpeed * deltaTime);

		if (this.graphicsObject)
			this.graphicsObject.transform.position = this.position;

		// Check if dead...
		if (world.IsPointOutOfBounds(this.position))
			this.TakeDamage(this.maxHitPoints + 1);
	}

	public override void Destroy()
	{
		//graphicsObject.SetActive(false);
	}
}
