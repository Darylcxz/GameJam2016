using UnityEngine;
using System.Collections;

public class DestroyParticlefx : MonoBehaviour {


    void OnEnable()
    {
        Invoke("Destroyitself", 0.5f);
    }

    void Destroyitself()
    {
        Destroy(gameObject);
    }
}
