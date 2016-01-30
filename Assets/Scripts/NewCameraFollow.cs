using UnityEngine;
using System.Collections;

public class NewCameraFollow : MonoBehaviour {
    GameObject target;
	// Use this for initialization
	void Start () {

        target = GameObject.Find("Player");
	
	}
	
	// Update is called once per frame
	void Update () {

        float moveX = target.transform.position.x - transform.position.x;
        float moveY = target.transform.position.y - transform.position.y;

        Vector2 currLocation = new Vector2(transform.position.x + moveX / 6, transform.position.y + moveY / 6);
        transform.position = currLocation;
	
	}
}
