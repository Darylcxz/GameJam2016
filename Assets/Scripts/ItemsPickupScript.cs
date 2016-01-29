using UnityEngine;
using System.Collections;

public class ItemsPickupScript : MonoBehaviour {
    private int ingredientlevel = 1;
    private int[] ingredients;
    private int[] ingredientchecklist;



    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Ingredient"))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                
            }
        }
    }
}
