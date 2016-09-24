using UnityEngine;
using System.Collections;

public interface IGraphicObject
{
	void SetGameObject(GameObject gameObject);
	GameObject ReturnGameObject();
}
