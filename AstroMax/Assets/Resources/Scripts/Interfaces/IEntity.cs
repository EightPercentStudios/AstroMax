using UnityEngine;
using System.Collections;

public interface IEntity
{
	void Update(float deltaTime, IWorld world);
	void Destroy();

	Vector2 GetPosition();
}
