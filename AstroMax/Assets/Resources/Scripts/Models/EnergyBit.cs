using UnityEngine;
using System.Collections;

public class EnergyBit : KillableEntityBase, IGraphicObject
{
	private GameObject graphicsObject;
	private Vector2 velocity;
	private float fallSpeed = 5f;
	private const float friction = 0.85f;

	public EnergyBit(Vector2 position, Vector2 velocity) : base(position, 1) // 1 hit point
	{
		this.velocity = velocity;
	}

	public void SetGameObject(GameObject gameObject)
	{
		this.graphicsObject = gameObject;
		this.graphicsObject.transform.position = this.position;
	}

	public GameObject ReturnGameObject()
	{
		GameObject obj = this.graphicsObject;
		this.graphicsObject = null;
		return obj;
	}

	public void Update(float deltaTime, IWorld world, Vector2 playerPosition)
	{
		this.position += this.velocity * deltaTime;
		this.position.y -= deltaTime * fallSpeed;
		this.velocity *= friction;
		this.fallSpeed += 8f * deltaTime;

		if (this.graphicsObject)
			this.graphicsObject.transform.position = this.position;

		if (this.position.y < world.GetLowerBound().y)
			this.TakeDamage(this.maxHitPoints + 1);
	}
}
