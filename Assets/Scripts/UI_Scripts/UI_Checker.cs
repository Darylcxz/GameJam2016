using UnityEngine;
using System.Collections;

public class UI_Checker : MonoBehaviour
{
    GameObject _uiM;
    
	// Use this for initialization
	void Start ()
    {
        _uiM = GameObject.Find("OverlayUI");
        _uiM.SendMessage("Setup");
    }
}
