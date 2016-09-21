using UnityEngine;
using System.Collections;

public abstract class EntityBase : IEntity
{
	// Internal Properties
	protected Vector2 position;

	public EntityBase()
	{
		this.position = Vector2.zero;
	}

	public EntityBase(Vector2 position)
	{
		this.position = position;
	}

	public Vector2 GetPosition()
	{
		return this.position;
	}

	public virtual void Update(float deltaTime)
	{

	}
}
