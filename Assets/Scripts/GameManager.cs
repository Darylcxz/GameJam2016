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

	// Use this for initialization
	void Start () {
	//	whiteImg = whiteImg.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.L))
		{
			UtilityScript.instance.FadeInFadeOut(_img);
		}
	
	}
	void NextLevel()
	{
 
	}
}
