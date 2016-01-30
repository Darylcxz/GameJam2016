using UnityEngine;
using System.Collections;

public class DestroyParticlefx : MonoBehaviour {


    void OnEnable()
    {
        Invoke("Destroyitself", 1.0f);
    }

    void Destroyitself()
    {
        Destroy(gameObject);
    }
}
