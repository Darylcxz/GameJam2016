using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UtilityScript : MonoBehaviour {

	public static UtilityScript instance;

	//[SerializeField] Image blackUI;
	[SerializeField] Image whiteUI;
	Image _img;
	bool bFade;
	float _alpha;

	Vector3 _originalPos;
	public Transform _cam;
	public static float camShake;

	// Use this for initialization
	void Start () 
	{
		instance = this;
		_originalPos = _cam.position;
	
	}
	public void FlashToFade(Image _image)
	{
		if (!bFade)
		{
			bFade = true;
			_img = _image;
			StartCoroutine("Flash");
		}
	}
	public void FadeInFadeOut(Image _image)
	{
		if (!bFade)
		{
			bFade = true;
			_img = _image;
			StartCoroutine("Fade");
		}
	}
	IEnumerator Flash()
	{
		_alpha = 1;
		yield return new WaitForSeconds(0.5f);
		for (float j = 1; j > 0; j -= Time.deltaTime)
		{
			_alpha = j;
			yield return null;
		}
		_alpha = 0;
		yield return new WaitForSeconds(2f);
		bFade = false;

	}
	IEnumerator Fade()
	{
		for (float i = 0; i < 1; i += Time.deltaTime * 3.5f)
		{
			_alpha = i;
			yield return null;
		}
		_alpha = 1;
		yield return new WaitForSeconds(2);
		for (float j = 1; j > 0; j -= Time.deltaTime)
		{
			_alpha = j;
			yield return null;
		}
		_alpha = 0;
		yield return new WaitForSeconds(1f);
		bFade = false;
			
	}
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.O))
		{
			_originalPos = _cam.position;
			camShake += 5f;

		}
		
		
		if (_img != null)
		{
			_img.color = new Color(_img.color.r, _img.color.g, _img.color.b, _alpha);
		}

		//_originalPos = _cam.position;

		// Camera Shake
		if (camShake > 0)
		{
			_cam.position = _originalPos + Random.insideUnitSphere * 2f;

			camShake -= Time.deltaTime;
		}
		else
		{
			camShake = 0f;
			_cam.position = _originalPos;
		}
		
	}
	public static Vector3 OnUnitCircle(float x,float y)
	{

		float newX = Random.Range(0, x);
		float newY = Random.Range(0, y);

		return new Vector3(newX, newY, 0);
	}

}
