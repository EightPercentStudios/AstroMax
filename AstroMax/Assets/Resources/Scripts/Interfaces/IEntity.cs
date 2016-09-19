using UnityEngine;
using System.Collections;

public interface IEntity
{
	void Update(float deltaTime);

	Vector2 GetPosition();
}
