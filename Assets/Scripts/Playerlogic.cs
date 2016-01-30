using UnityEngine;
using System.Collections;

public class Playerlogic : MonoBehaviour {
    Rigidbody2D rb;
    [SerializeField]Transform groundtag;
    [SerializeField] float movespeed;
    [SerializeField] float jumpforce;
    public LayerMask playermask;
    private bool onGround;

	Vector3 playerScale;
	private Animator _playerAnim;
	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
		_playerAnim = GetComponent<Animator>();
		playerScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        onGround = Physics2D.Linecast(transform.position, groundtag.position, playermask);
        Move(Input.GetAxisRaw("Horizontal"));
        if(onGround)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity += Vector2.up * jumpforce;
            }
        }
	}

    void Move(float h1)
    {
        Vector2 movedir = new Vector2(h1, 0);
        movedir *= movespeed;
        rb.velocity = new Vector2(movedir.x, rb.velocity.y);
	
		_playerAnim.SetFloat("f_velocity", rb.velocity.magnitude);
		if (h1 > 0)
		{
			transform.localScale = new Vector3(-playerScale.x, playerScale.y, playerScale.z);
		}
		if (h1 < 0)
		{
			transform.localScale = new Vector3(playerScale.x, playerScale.y, playerScale.z);
		}
    }
}
