using UnityEngine;
using System.Collections;

public class ItemsManager : MonoBehaviour {
    private int ingredientlevel = 0;
    private int ingredientindex;
    private int itemnumber;

    [SerializeField] GameObject[] items;

    void Start()
    {
        ingredientindex = 0;
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
                    Debug.Log("Correct item " + itemnumber);
                    ingredientindex++;
                    other.SendMessage("Collect");
                }

                else if(itemnumber == ingredientindex && ingredientindex == ingredientlevel)
                {
                    Debug.Log("Light Shines Brighter");
                    ingredientlevel++;
                    ingredientindex = 0;
                    other.SendMessage("Collect");
                }

                else
                {
                    Debug.Log("Wrong item " + itemnumber + " this should be " + ingredientindex);
                }
            }
        }
    }

    void SpawnNewItems()
    {
        
    }
}
