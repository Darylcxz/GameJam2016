using UnityEngine;
using System.Collections;

public class ItemsManager : MonoBehaviour {
    private int ingredientlevel = 0;
    private int ingredientindex = 0;
    private int itemnumber;

    [SerializeField] GameObject[] items;
    [SerializeField]
    AudioSource maincam;
    [SerializeField]
    AudioClip wrongtitem;

    void Start()
    {

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
                    Debug.Log(itemnumber + " " + ingredientindex);
                    ingredientindex++;
                    other.SendMessage("Collect");
                }

                else if(itemnumber == ingredientindex && ingredientindex == ingredientlevel)
                {
                    Debug.Log(ingredientlevel);
                    ingredientlevel++;
                    ingredientindex = 0;
                    other.SendMessage("Collect");
                }

                else if(itemnumber != ingredientindex)
                {
                    Debug.Log("Wrong item " + itemnumber + " this should be " + ingredientindex);
                    maincam.PlayOneShot(wrongtitem);
                }
            }
        }
    }

    void SpawnNewItems()
    {
        
    }
}
