using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour, ISpawner
{
	public const string ResourcePrefabPath = "Prefabs/";

	public GameObject SpawnPrefab(string prefabName)
	{
		return (GameObject)Instantiate(Resources.Load(ResourcePrefabPath + prefabName));
	}

	public GameObject SpawnPrefab(GameObject prefabObject)
	{
		return (GameObject)Instantiate(prefabObject);
	}
}
