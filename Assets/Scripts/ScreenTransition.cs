using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenTransition : MonoBehaviour {
	//fade in black, fade out black
	[SerializeField]
	Image fadeUI;		//UI for fade

	float _t; //update time keeper
	bool bFade;
	float _alpha;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.F)&&!bFade)
		{
			StartCoroutine("Faded");
			bFade = true;
		}
		fadeUI.color = new Color(fadeUI.color.r, fadeUI.color.g, fadeUI.color.b, _alpha);
	}
	IEnumerator Faded()
	{
		for (float i = 0; i < 1; i+=Time.deltaTime*3.5f)
		{
			Debug.Log(i);
			_alpha = i;
			yield return null;
		}
		_alpha = 1;
		yield return new WaitForSeconds(2);
		for (float j = 1; j > 0; j-=Time.deltaTime)
		{
			Debug.Log(j);
			_alpha = j;
			yield return null;
		}
		_alpha = 0;
		bFade = false;
			
	}
}
