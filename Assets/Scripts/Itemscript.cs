using UnityEngine;
using System.Collections;

public class Itemscript : MonoBehaviour {
    ParticleSystem pickupvfx;
    SpriteRenderer item;
	// Use this for initialization
	void Start () {

        pickupvfx = gameObject.GetComponentInChildren<ParticleSystem>();
        item = gameObject.GetComponent<SpriteRenderer>();
	}


    void Collect()
    {
        pickupvfx.Play();
        item.color = new Color(item.color.r, item.color.g, item.color.b, 0.0f);
        Invoke("Destroyme", 1.0f);
    }

    void Destroyme()
    {
        gameObject.SetActive(false);
    }
}
