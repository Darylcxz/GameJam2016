using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	[SerializeField]
	GameObject[] Levels;
	[SerializeField]
	int currentLevel;
	[SerializeField]
	Image _img;
	[SerializeField]
	Transform playerSpawn;
	GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void NextLevel()
	{
		UtilityScript.instance.FlashToFade(_img);
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
