using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemsManager : MonoBehaviour {
    private int ingredientlevel = 0;
    private int ingredientindex = 0;
    private int itemnumber;

    [SerializeField] GameObject[] items;
    [SerializeField]
    AudioSource maincam;
    [SerializeField]
    AudioClip wrongtitem;
	[SerializeField]
	GameManager _gm;
    public Image[] images;


    void Start()
    {
		_gm = _gm.GetComponent<GameManager>();
        foreach (Image pic in images)
        {
            pic.enabled = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Ingredient"))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                int.TryParse(other.gameObject.name, out itemnumber);
                if(itemnumber == ingredientindex && ingredientindex != ingredientlevel)
                {
                    images[ingredientindex].enabled = true;
                    ingredientindex++;
                    Debug.Log(ingredientindex);
                    Debug.Log("level = " + ingredientlevel);
                    Itemscript thisitem = other.GetComponent<Itemscript>();
                    thisitem.Collect();
                    
                }

                else if(itemnumber == ingredientindex && ingredientindex == ingredientlevel)
                {
                    images[ingredientindex].enabled = true;
                    Debug.Log(ingredientlevel);
					_gm.PlayExplosion();
                    ingredientlevel++;
                    Itemscript thisitem = other.GetComponent<Itemscript>();
                    thisitem.Collect();
                    ingredientindex = 0;
                    Invoke("DestroyImages", 12.0f);
                }
                else if(itemnumber != ingredientindex)
                {
                    Debug.Log(ingredientindex);
                    Debug.Log("Wrong item " + itemnumber + " this should be " + ingredientindex);
                    maincam.PlayOneShot(wrongtitem);
                }
            }
        }
    }

    void DestroyImages()
    {
        foreach (Image pic in images)
        {
            pic.enabled = false;
        }
    }
    
}
