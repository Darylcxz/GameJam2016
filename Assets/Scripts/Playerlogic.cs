using UnityEngine;
using System.Collections;

public class Playerlogic : MonoBehaviour {
    Rigidbody2D rb;
    [SerializeField] float movespeed;
	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Move(Input.GetAxisRaw("Horizontal"));
	}

    void Move(float h1)
    {
        Vector2 movedir = new Vector2(h1, 0);
        rb.velocity = movedir * movespeed;
    }
}
