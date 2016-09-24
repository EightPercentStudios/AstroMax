using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : IObjectPool
{
	// Internal Properties
	private GameObject parentObject;
	private GameObject prefab;
	private List<GameObject> pool;

	public ObjectPool(string name, GameObject prefab, int initialSize)
	{
		// Create GameObject as parent folder
		this.parentObject = new GameObject(name);

		this.prefab = prefab;
		this.pool = new List<GameObject>();
		for (int i = 0; i < initialSize; i++)
		{
			AllocateObjectInstance();
		}
	}

	protected virtual void AllocateObjectInstance()
	{
		GameObject instance = GameObject.Instantiate(this.prefab) as GameObject;
		instance.transform.SetParent(this.parentObject.transform);
		instance.SetActive(false);
		this.pool.Add(instance);
	}

	public GameObject GetObjectInstance()
	{
		if (this.pool.Count == 0)
			AllocateObjectInstance();

		GameObject instance = this.pool[0];
		this.pool.RemoveAt(0);

		instance.SetActive(true);
		return instance;
	}

	public void ReturnObjectInstance(GameObject objectInstance)
	{
		objectInstance.SetActive(false);
		this.pool.Add(objectInstance);
	}
}
