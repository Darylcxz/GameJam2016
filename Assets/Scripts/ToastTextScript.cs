using UnityEngine;
using System.Collections;

public class ToastTextScript : MonoBehaviour {
    float Timertime = 5.0f;
    float alpha;
    CanvasGroup canvas;

    void Start()
    {
        canvas = gameObject.GetComponent<CanvasGroup>();
    }
	// Update is called once per frame
	void Update () {

        Timertime -= Time.deltaTime;
        canvas.alpha -= Time.deltaTime / 5;
        transform.Translate(Vector3.up * Time.deltaTime);
        if (Timertime < 0)
            Destroy(gameObject);
	
	}
}
