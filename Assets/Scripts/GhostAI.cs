using UnityEngine;
using System.Collections;

public class GhostAI : MonoBehaviour {


	[SerializeField]
	enum AILogic
	{
		PATROL,
		WAIT,
		MOVETOPLAYER,
		RUNFROMPLAYER
	};
	[SerializeField]AILogic States = AILogic.PATROL;

	
	Vector3 spawnPoint;

	
	Rigidbody2D _rb;  //rigidbody of boo
	Transform target; //player target
	float _t;
	bool fadeAway;

	Vector3 spriteScale;
	//Vector3 direction;//direction to face

	[SerializeField]SpriteRenderer booSprite;

	float spinAngleZ;

	[SerializeField]
	float rotateSpeed;
	[SerializeField]
	float translateSpeed;
	[SerializeField]
	float aggroRange = 10f;
	[SerializeField]float circleRadius = 20f;
	[SerializeField]Vector3 circleCenter;
	[SerializeField]Vector3 rectSize;
	Vector3 targetPos;
	float minDistance = 5f; //distance before it finds new pos;
	float distanceToPlayer;


	float gameTimer;
	float waitTime = 0.1f;

	// Use this for initialization
	void Start () {
		_rb = GetComponent<Rigidbody2D>();
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		booSprite = booSprite.GetComponent<SpriteRenderer>();
		spriteScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
		targetPos = circleCenter + (UtilityScript.OnUnitCircle(rectSize.x,rectSize.y) * circleRadius);
		SpriteReposition();
	
	}
	
	// Update is called once per frame
	void Update () {
		StateMachine();

		//Debug.Log(UtilityScript.OnUnitCircle(rectSize.x, rectSize.y));

		
	
	}
	void StateMachine()
	{
		distanceToPlayer = Vector3.Distance(transform.position, target.position);
//		Debug.Log(distanceToPlayer);
		switch (States)
		{
 			case AILogic.PATROL:
				AIMove(transform.position, targetPos, translateSpeed / 1.5f,true);
				SpriteReposition();
				break;
			case AILogic.WAIT:
				gameTimer += Time.deltaTime;
				if(gameTimer>waitTime)
				{
					gameTimer = 0f;
					States = AILogic.PATROL;
				}
				break;
			case AILogic.MOVETOPLAYER:
				AIMove(transform.position,target.position,translateSpeed);
				SpriteReposition(); //logic for repositioning the sprite
				//Let's the sprite blink back and forth
				booSprite.color = new Color(1, 0, 0, (Mathf.PingPong(Time.time / 2, 1 - 0)));
				break;
			case AILogic.RUNFROMPLAYER:
				
				AIMove(target.position, transform.position,translateSpeed*10);
				booSprite.color = new Color(1, 1, 1,(Mathf.Lerp(1, 0, _t / 1)));
				if (fadeAway)
				{
					_t += Time.deltaTime;
					if (_t / 1 > 1)
					{
						_t = 0;
						ResetAI();
						gameObject.SetActive(false);
						fadeAway = false;
					}
				}
				break;
		}
		if (distanceToPlayer < aggroRange && States!=AILogic.RUNFROMPLAYER)
		{
			States = AILogic.MOVETOPLAYER;
			//ResetAI();
		}
	}
	void AIMove(Vector3 A,Vector3 B,float _speed,bool isPatrol=false)
	{
		Vector3 direction = (B - A).normalized; //Gets the direction that the AI should be going to to chase player
		direction.z = 0;		//resets the position of the player's Z so he doesn't look far away

		//Vector3 vectorToTarget = targetTransform.position - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;		//gets angle of the direction you should face
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);			//makes a quaternion for that angle, using the correct axis
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotateSpeed); //rotate to face character
		transform.position = new Vector3(transform.position.x, transform.position.y, 0); // Set the Z to 0 
		transform.localPosition += transform.right * Time.deltaTime * _speed;		//moves the AI
		if(isPatrol)
		{
			float distance = Vector3.Distance(B,A);
			if (distance < minDistance)
			{
				FindNewTargetPosition();
				States = AILogic.WAIT;
			}
		}
	}
	void SpriteReposition()
	{
		if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 90)
		{
			transform.localScale = new Vector3(-spriteScale.x, spriteScale.y, transform.localScale.z);
		}
		if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 180)
		{
			transform.localScale = new Vector3(-spriteScale.x, -spriteScale.y, transform.localScale.z);
		}
		if (transform.eulerAngles.z > 180 && transform.eulerAngles.z < 270)
		{
			transform.localScale = new Vector3(-spriteScale.x, -spriteScale.y, transform.localScale.z);
		}
		if (transform.eulerAngles.z > 270 && transform.eulerAngles.z < 359)
		{
			transform.localScale = new Vector3(-spriteScale.x, spriteScale.y, transform.localScale.z);
		}
		
	}
	void OnTriggerEnter2D(Collider2D _col)
	{
		if(_col.gameObject.CompareTag("Player"))
		{
			//Send message to player when it hits the player
			States = AILogic.RUNFROMPLAYER;
			fadeAway = true;
		}
	}
	void ResetAI()
	{
		States = AILogic.PATROL;
		transform.position = spawnPoint;
		transform.rotation = Quaternion.identity;
		booSprite.color = new Color(1, 1, 1, 1);
		gameTimer = 0;
	}
	void FindNewTargetPosition()
	{
		targetPos = circleCenter + (UtilityScript.OnUnitCircle(rectSize.x,rectSize.y) * circleRadius);
	}
	void OnDrawGizmos()
	{
		Gizmos.color = Color.black;
	//	Gizmos.DrawWireSphere(circleCenter, circleRadius);
		float offsetX = rectSize.x / 2;
		float offsetY = rectSize.y / 2;
		Vector3 cubePos = new Vector3(offsetX, offsetY, 0);
		Gizmos.DrawWireCube(circleCenter+(cubePos*circleRadius), rectSize*circleRadius);
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(targetPos, new Vector3(1, 1, 1));
	}
	public void SetSpawn(Vector3 _spawnPoint)
	{
		Debug.Log(_spawnPoint);
		spawnPoint = _spawnPoint;
	}
}
