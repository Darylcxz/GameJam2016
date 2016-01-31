using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Itemscript : MonoBehaviour {
    ParticleSystem pickupvfx;
    SpriteRenderer item;
    [SerializeField]
    AudioSource maincam;
    [SerializeField]
    AudioClip soundfx;
	Collider2D col;
    [SerializeField] string uitext;
    [SerializeField] GameObject ToastText;
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
        TextAppears();
    }

    void Destroyme()
    {
        gameObject.SetActive(false);
    }

    void TextAppears()
    {
        GameObject UItext = Instantiate(ToastText, transform.position + Vector3.up * 2, ToastText.transform.rotation) as GameObject;
        Text texttoshow = UItext.GetComponentInChildren<Text>();
        texttoshow.text = uitext;
    }
}
