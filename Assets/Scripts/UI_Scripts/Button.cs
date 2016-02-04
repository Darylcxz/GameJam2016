using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _anim.SetBool("Hover", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _anim.SetBool("Hover", false);
    }
}
