using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Photon.Pun;

public class ItemManager:MonoBehaviour{
    
    public static ItemManager instancie;
    
    public  ItensSlot itensSlot;
    [SerializeField] PhotonView photonViewmy;

    public List<ItemObject> listObjectItens=new List<ItemObject>();
     private void Awake() {
        if(!instancie)
        {
            instancie=this;
        }
        else{
            Destroy(this);
        }
     }

     public void Initialize() {        
        // photonViewmy.RPC("SpawnItensRandom",RpcTarget.AllBuffered);
        SpawnItensRandom();
     }

    [PunRPC]
     private void SpawnItensRandom()
     {
         System.Random randomNext = new System.Random();
         
         for (int i = 0; i < getItems.Count; i++)
        {
            
            getItems[i].Initialize(new ItemPuzzle()
            {
                 itens = listObjectItens[i]
            });

        }
    }

    private int NumeroAleatorio()
    {
        int value =UnityEngine.Random.Range(0, getItems.Count);
        while(NumeroAleatorioJaFoi())
        {
           value = UnityEngine.Random.Range(0, getItems.Count);
        }

        bool NumeroAleatorioJaFoi()
        {
            if(numerosAleatorios.Count>0)
            { 
                foreach (var numero in numerosAleatorios)
               {
                if(numero == value)
                {
                    return true;
                }
               }
            }

            return false;
        }
        numerosAleatorios.Add(value);
        return value;
    }

    public void AddItem(ItemObject item)
    {
        itensObject.Add(item);        
    }

    public void TradeItem(GetItem getItem)
    {
        int numberRandom  = NumeroAleatorio();
        GetItem getItem1 =  getItems[numberRandom];
        ItemObject itemTroca = getItem.itemPuzzle.itens;        
        getItem.DestroyItemAndAddItem(getItem1.itemPuzzle.itens);
        getItem1.DestroyItemAndAddItem(itemTroca);
        print("FoiAquiFUNCIONOU");
    }

    public void RemoveItem(ItemObject item) => itensObject.Remove(item);

    public void ItemInHand(ItemObject item)
    {
        //AddItem(item);
        _iteminHand=item;
        itensSlot.SetItemObject(item);
    }

    public bool ItemNecessary(EnumItens enumItens)
    {
        if(_iteminHand?.itemName==enumItens){
            return true;
        }
        

        foreach (var _ in itensObject.Where(item => item.itemName == enumItens).Select(item => new { }))
        {
            return true;
        }

        return false;
    }

    public void RemoveItemHand(){
      _iteminHand=null;
      itensSlot.RemoveItem();
    } 

    public void SpawnItemSabotagem(ItemObject itemSpawn)
    {
        foreach (var getItem in getItems)
        {
           if(getItem.itemObjectCurrent==null)
           {
             getItem.Initialize(new ItemPuzzle()
            {
                 itens = itemSpawn
            });
           }
           
        }
    }


    List<ItemObject> itensObject=new List<ItemObject>();
     
    [Space]

    [SerializeField] private List<GetItem> getItems= new List<GetItem>();

    int cont_Win;

    ItemObject _iteminHand; 

    private List<int> numerosAleatorios = new List<int>();
    

}
