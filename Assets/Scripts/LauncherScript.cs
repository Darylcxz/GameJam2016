using UnityEngine;
using System.Collections;

public class LauncherScript : MonoBehaviour {
    [SerializeField] GameObject jumpvfx;
    [SerializeField]
    float launchforce;
    void Start()
    {
        jumpvfx.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D player = other.gameObject.GetComponent<Rigidbody2D>();
            jumpvfx.SetActive(true);
            player.AddForce(Vector2.up * launchforce, ForceMode2D.Impulse);
            Invoke("SetFalse", 1.0f);
        }
    }

    void SetFalse()
    {
        jumpvfx.SetActive(false);
    }
}
