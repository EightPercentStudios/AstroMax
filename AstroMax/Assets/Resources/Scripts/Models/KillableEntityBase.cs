using UnityEngine;
using System.Collections;

public abstract class KillableEntityBase : EntityBase
{
	// Internal Properties
	protected int hitPoints;
	protected readonly int maxHitPoints;
	protected bool alive;

	public KillableEntityBase(Vector2 position, int maxHitPoints) : base(position)
	{
		this.position = position;
		this.maxHitPoints = maxHitPoints;
		this.hitPoints = maxHitPoints;
		this.alive = true;
	}

	public int TakeDamage(int damagePoints)
	{
		this.hitPoints -= damagePoints;
		if (this.hitPoints <= 0)
			this.alive = false;

		return this.hitPoints;
	}

	public void RestoreHitPoints(int hitPoints)
	{
		this.hitPoints = Mathf.Clamp(this.hitPoints + hitPoints, 0, this.maxHitPoints);
	}

	public int GetMaxHitPoints()
	{
		return this.maxHitPoints;
	}

	public int GetRemainingHitPoints()
	{
		return this.hitPoints;
	}

	public bool IsAlive()
	{
		return this.alive;
	}

}
