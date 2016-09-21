using UnityEngine;
using System.Collections;

public interface ISpawner
{
	GameObject SpawnPrefab(string prefabName);
	GameObject SpawnPrefab(GameObject prefabObject);
}
