using UnityEngine;

public class CreditsRoles : MonoBehaviour
{
    Animator _anim;
	// Use this for initialization
	void Start ()
    {
        _anim = GetComponent<Animator>();
	}

    public void Appear()
    {
        _anim.SetBool("Appear", true);
    }

    public void Disappear()
    {
        _anim.SetBool("Appear", false);
    }
}
