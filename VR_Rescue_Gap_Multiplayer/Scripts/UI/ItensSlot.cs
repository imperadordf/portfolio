
using System.Collections.Generic;
using UnityEngine;

public class ItensSlot:MonoBehaviour
{
  public GameObject currentItemObject;
  public void SetItemObject(ItemObject item) 
  {
      RemoveItem();
     _itemCurrent = item;
     _itemCurrent.gameObject_Item.transform.position = new Vector3(0,0,0);
     _itemCurrent.Initialize();
    currentItemObject = Instantiate(_itemCurrent.gameObject_Item,SpawnItem);
  }

  public ItemObject GetItemObject()
  {
     return _itemCurrent;
  }

  public void RemoveItem(){
      if(GetItemObject()){
      Destroy(currentItemObject);

       _itemCurrent = null;
      }
     
  }

   [SerializeField] private ItemObject _itemCurrent;

   [SerializeField] private Transform SpawnItem;
   //[SerializeField] private ItensInteractive[] itensInteractives;

}
