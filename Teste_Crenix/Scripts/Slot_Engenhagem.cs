using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot_Engenhagem : MonoBehaviour,IDropHandler
{
    
    public RectTransform myRectTransform;
    [SerializeField]
    EnumEngenhagem Slot;
    public bool isVoid=true;
   
    public void OnDrop(PointerEventData eventData)
    {
        if (isVoid)
        {
            eventData.pointerDrag.GetComponent<Drag_Engenhagem>().OnDrop(this, Slot);
        }
       
    }

   
}
