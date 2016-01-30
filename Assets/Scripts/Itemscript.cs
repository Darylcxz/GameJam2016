using UnityEngine;
using System.Collections;

public class Itemscript : MonoBehaviour {
    ParticleSystem pickupvfx;
    SpriteRenderer item;
    [SerializeField]
    AudioSource maincam;
    [SerializeField]
    AudioClip soundfx;
	// Use this for initialization
	void Start () {

        pickupvfx = gameObject.GetComponentInChildren<ParticleSystem>();
        item = gameObject.GetComponent<SpriteRenderer>();
	}


    void Collect()
    {
        pickupvfx.Play();
        maincam.PlayOneShot(soundfx);
        item.color = new Color(item.color.r, item.color.g, item.color.b, 0.0f);
        Invoke("Destroyme", 1.0f);
    }

    void Destroyme()
    {
        gameObject.SetActive(false);
    }
}
