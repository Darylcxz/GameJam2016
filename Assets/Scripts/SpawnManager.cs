using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {


	[SerializeField]Transform[] SpawnPoints;
	[SerializeField]
	float spawnTimeLimit = 20f;
	float gameTime;

	public bool stop;
	// Use this for initialization
	void Start () {
		SpawnPoints = GetComponentsInChildren<Transform>();
		stop = true;
	}
	
	void Update () {
		gameTime += Time.deltaTime;
		if (gameTime > spawnTimeLimit && !stop)
		{
			int _index = Random.Range(1, SpawnPoints.Length);
			GameObject ghostClone = ObjectPooler.current.SpawnGhost();
			if (System.Object.ReferenceEquals(ghostClone, null))
			{
				return;
			}
			ghostClone.transform.position = SpawnPoints[_index].position;
			ghostClone.SetActive(true);
			ghostClone.GetComponent<GhostAI>().SendMessage("SetSpawn", SpawnPoints[_index].position);
			gameTime = 0f;
		}
	}
}
