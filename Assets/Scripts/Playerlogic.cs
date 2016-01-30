using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Playerlogic : MonoBehaviour {

	[SerializeField]float maxHealth = 100f;
	[SerializeField]float currentHealth;
	[SerializeField]SpriteRenderer healthGlow;
	float _alpha;

    Rigidbody2D rb;

    [SerializeField]Transform groundtag;
    [SerializeField] float movespeed;
    [SerializeField] float jumpforce;
    float alpha;
    public LayerMask playermask;
	public LayerMask platformMask;
    private bool onGround;
    bool flickrcheck = false;
    [SerializeField]ParticleSystem dust;
    [SerializeField]GameObject jumpdust;
    SpriteRenderer sp;

	RaycastHit2D hit;

	Vector3 playerScale;
	private Animator _playerAnim;
    [SerializeField]
    AudioSource maincam;
    [SerializeField] AudioClip[] soundeffects;
	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
//		col = GetComponent<Collider2D>();
		_playerAnim = GetComponent<Animator>();
		playerScale = transform.localScale;
        sp = gameObject.GetComponent<SpriteRenderer>();
		healthGlow = healthGlow.GetComponent<SpriteRenderer>();
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        onGround = Physics2D.Linecast(transform.position, groundtag.position, playermask);
		_playerAnim.SetBool("bGrounded", onGround);
		
		_alpha = currentHealth / maxHealth;
		healthGlow.color = new Color(healthGlow.color.r, healthGlow.color.g, healthGlow.color.b, _alpha);
		currentHealth += Time.deltaTime/2;
		if (currentHealth > maxHealth)
		{
			currentHealth = maxHealth;
		}
	
		Move(Input.GetAxisRaw("Horizontal"));
		if (onGround)
		{
			if (Input.GetAxisRaw("Vertical") < 0)
			{
				_playerAnim.SetBool("bDuck", true);
				if (Input.GetKeyDown(KeyCode.Space))
				{
					hit = (Physics2D.Raycast(groundtag.position, Vector2.down, 0.5f,platformMask));
					if (hit.collider.CompareTag("Platform"))
					{
						StartCoroutine("DownJump",hit.collider.gameObject);
						rb.velocity += Vector2.down * jumpforce;
					}
					
				}
			}
			if (Input.GetKeyDown(KeyCode.Space) && Input.GetAxisRaw("Vertical") > -1)
			{
				GameObject jumpcloud = Instantiate(jumpdust, transform.position + Vector3.down * 1.8f, jumpdust.transform.rotation) as GameObject;
                maincam.PlayOneShot(soundeffects[2]);
				rb.velocity += Vector2.up * jumpforce;
			}

		}
		if (Input.GetAxisRaw("Vertical")>-1)
		{
			_playerAnim.SetBool("bDuck", false);
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
	public void TakeDamage(float dmg)
	{
        maincam.PlayOneShot(soundeffects[0]);
		Flicker();
		currentHealth -= dmg;
	}
    public void Playdust()
    {
        if(onGround)
        {
            dust.Play();
            maincam.PlayOneShot(soundeffects[1]);
        }
            
    }
    public void Flicker()
    {
        StartCoroutine("Flickering");
    }
    IEnumerator Flickering()
    {
        for (int i = 0; i < 6; i++)
        {
            if(!flickrcheck)
            {
                sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 0.1f);
                flickrcheck = true;
            }

            else if(flickrcheck)
            {
                sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1.0f);
                flickrcheck = false;
            }
            yield return new WaitForSeconds(0.08f);
        }
    }
	IEnumerator DownJump(GameObject col)
	{
		Collider2D[] temp = col.GetComponents<Collider2D>();
		foreach (Collider2D _col in temp)
		{
			_col.enabled = false;
		}
		yield return new WaitForSeconds(1f);
		_playerAnim.SetBool("bDuck", false);
		foreach (Collider2D _col in temp)
		{
			_col.enabled = true;
		}
	}
}
