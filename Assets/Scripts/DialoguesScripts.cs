using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Xml;
using System.IO;

public class DialoguesScripts : MonoBehaviour {
    [SerializeField]private TextAsset dialoguetext;
    [SerializeField]private Text diatext;
    private XmlNode showtext;
    private bool textcomplete;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator Printletters(string sentence)
    {
        string str = "";
        for (int i = 0; i < sentence.Length; i++)
        {
            str += sentence[i];
            if (i == sentence.Length - 1)
            {
                print("truuuuuueeeee");
                textcomplete = true;
            }

            if (textcomplete == true)
            {
                str = sentence;
                i = sentence.Length;
            }
            diatext.text = str;
            yield return new WaitForSeconds(0.04f);
        }
    }
}
