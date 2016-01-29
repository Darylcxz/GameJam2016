using UnityEngine;
using System.Collections;

public class Playerlogic : MonoBehaviour {
    Rigidbody2D rb;
    [SerializeField]Transform groundtag;
    [SerializeField] float movespeed;
    [SerializeField] float jumpforce;
    public LayerMask playermask;
    private bool onGround;
	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
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
    }
}
