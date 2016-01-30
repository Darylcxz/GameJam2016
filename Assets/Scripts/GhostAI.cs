using UnityEngine;
using System.Collections;

public class GhostAI : MonoBehaviour {

	Rigidbody2D _rb;  //rigidbody of boo
	Transform target; //player target
	Vector3 direction;//direction to face

	SpriteRenderer booSprite;

	[SerializeField]
	float speed = 5f;
	[SerializeField]
	float steerForce;
	[SerializeField]
	float rotateSpeed;
	[SerializeField]
	float translateSpeed;
	// Use this for initialization
	void Start () {
		_rb = GetComponent<Rigidbody2D>();
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		booSprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		direction = (target.position - transform.position).normalized;
		direction.z = 0;
		
		//Vector3 vectorToTarget = targetTransform.position - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime *rotateSpeed);
		transform.position = new Vector3(transform.position.x, transform.position.y, 0);
		transform.position += transform.right * Time.deltaTime * translateSpeed;

		booSprite.color = new Color(1,1,1,(Mathf.PingPong(Time.time / 2, 1 - 0)));
		
		

		if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 360)
		{
			transform.localScale = new Vector3(-1, 1, 1);
		}
		else
		{
			transform.localScale = new Vector3(1, 1, 1);
		}

		
	
	}
}
