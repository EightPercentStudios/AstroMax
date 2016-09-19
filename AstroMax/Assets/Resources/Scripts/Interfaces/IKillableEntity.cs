using UnityEngine;
using System.Collections;

public interface IKillableEntity : IEntity
{
	int TakeDamage(int damagePoints);
	int GetRemainingHitPoints();
	int GetMaxHitPoints();
	void RestoreHitPoints(int healingPoints);
	bool IsAlive();
}
