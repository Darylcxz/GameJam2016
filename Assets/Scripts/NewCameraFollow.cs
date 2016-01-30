using UnityEngine;
using System.Collections;

public class NewCameraFollow : MonoBehaviour 
{
    GameObject target;

    float min = -10;
    float max = 10;

	// Use this for initialization
	void Start () {

        target = GameObject.Find("Player");
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        float moveX = target.transform.position.x - transform.position.x;
        float moveY = target.transform.position.y - transform.position.y;

        Vector3 currLocation = new Vector3(transform.position.x + moveX / 6, transform.position.y + moveY / 6, 0);
        transform.position = new Vector3(Mathf.Clamp(currLocation.x, min, max), Mathf.Clamp(currLocation.y, min, max), 0);
        //currLocation
	
	}
}
