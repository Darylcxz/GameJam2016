using UnityEngine;
using System.Collections;

public class NewCameraFollow : MonoBehaviour 
{
    GameObject target;



	[SerializeField]float minY;
	[SerializeField]
	float maxY;

	[SerializeField]
	float minX;
	[SerializeField]
	float maxX;

	[SerializeField]
	GameManager _gm;
	// Use this for initialization
	void Start () {

		_gm = _gm.GetComponent<GameManager>();
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		target = GameObject.FindGameObjectWithTag("Player");
        float moveX = target.transform.position.x - transform.position.x;
        float moveY = target.transform.position.y - transform.position.y;

		switch (_gm.currentLevel)
		{
 			case 0:
				minX = -6.3f;
				maxX = 5.2f;
				minY = -0.12f;
				maxY = 3.4f;
				break;
			case 1:
				minX = -12.1f;
				maxX = 11.8f;
				minY = 0f;
				maxY = 13.5f;
				break;
			case 2:
				minX = -18.4f;
				maxX = 17.6f;
				minY = 0f;
				maxY = 20.1f;
				break;
			case 3:
				minX = -27.4f;
				maxX = 26.4f;
				minY = 0f;
				maxY = 30.5f;
				break;
			case 4:
				break;
			case 5:
				break;
			case 6:
				break;
			case 7:
				break;

		}


        Vector3 currLocation = new Vector3(transform.position.x + moveX / 6, transform.position.y + moveY / 6, 0);
        transform.position = new Vector3(Mathf.Clamp(currLocation.x, minX, maxX), Mathf.Clamp(currLocation.y, minY, maxY), 0);
        //currLocation
	
	}
}
