using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	[SerializeField]
	GameObject[] Levels;
	public int currentLevel;
	[SerializeField]
	Image _img;
	[SerializeField]
	Transform playerSpawn;
	GameObject player;

	[SerializeField]
	GameObject explosionBOOM;
	[SerializeField]
	SpawnManager _spawnManager;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		_spawnManager = _spawnManager.GetComponent<SpawnManager>();
	}
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.M))
		{
			StartCoroutine("SwitchLevel");
		}
	}

	public void PlayExplosion()
	{
		_spawnManager.stop = true;
		UtilityScript.instance.CameraShake(10f, 2f);
		foreach (GameObject ghost in ObjectPooler.current.ghostPool)
		{
			ghost.GetComponent<GhostAI>().DespawnAll();
		}
		GameObject explosionClone = (GameObject)Instantiate(explosionBOOM, player.transform.position, Quaternion.identity);
		player.GetComponent<Playerlogic>().freeze = true;
		Invoke("NextLevel", 10f);
	}
	void NextLevel()
	{
		player.GetComponent<Playerlogic>().freeze = false;
		_spawnManager.stop = false;
		UtilityScript.instance.FlashToFade(_img);
		player.GetComponent<Playerlogic>().FullHealth();
		StartCoroutine("SwitchLevel");
		
	}
	IEnumerator SwitchLevel()
	{
		Levels[currentLevel].SetActive(false);
		currentLevel++;
		player.transform.position = playerSpawn.position;
		yield return new WaitForSeconds(0.1f);
		Levels[currentLevel].SetActive(true);
		yield return null;
	}
}
