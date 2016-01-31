using UnityEngine;
using System.Collections;

public class Itemscript : MonoBehaviour {
    ParticleSystem pickupvfx;
    SpriteRenderer item;
    [SerializeField]
    AudioSource maincam;
    [SerializeField]
    AudioClip soundfx;
	Collider2D col;
	// Use this for initialization
	void Start () {

        pickupvfx = gameObject.GetComponentInChildren<ParticleSystem>();
        item = gameObject.GetComponent<SpriteRenderer>();
	}


    public void Collect()
    {
        pickupvfx.Play();
        maincam.PlayOneShot(soundfx);
        item.color = new Color(item.color.r, item.color.g, item.color.b, 0.0f);
        col = gameObject.GetComponent<Collider2D>();
        col.enabled = false;
        Invoke("Destroyme", 1.0f);
    }

    void Destroyme()
    {
        gameObject.SetActive(false);
    }
}
