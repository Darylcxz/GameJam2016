using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour {
	public static ObjectPooler current;
	public GameObject _Ghosts;

	public int pooledAmount = 5;
	public bool canGrow;

	public List<GameObject> ghostPool;

	void Awake()
	{
		current = this;
	}

	void Start()
	{
		ghostPool = new List<GameObject>();
		for (int i = 0; i < pooledAmount; i++)
		{
			GameObject ghostClone = (GameObject)Instantiate(_Ghosts);
			ghostClone.SetActive(false);
			ghostPool.Add(ghostClone);
		}
	}

	public GameObject SpawnGhost()
	{
		for (int i = 0; i < ghostPool.Count; i++)
		{
			if (!ghostPool[i].activeInHierarchy)
			{
				return ghostPool[i];
			}
		}
		if (canGrow)
		{
			GameObject ghostClone = (GameObject)Instantiate(_Ghosts);
			ghostPool.Add(ghostClone);
			return ghostClone;
		}
		return null;
	}
}
