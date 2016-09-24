using UnityEngine;
using System.Collections;

public interface IObjectPool
{
	GameObject GetObjectInstance();
	void ReturnObjectInstance(GameObject objectInstance);
}
