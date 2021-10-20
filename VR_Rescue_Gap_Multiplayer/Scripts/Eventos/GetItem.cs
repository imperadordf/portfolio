using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GetItem : PuzzleInteractive
{
    [Header("Item que não pode Spawnar aqui")]
    public EnumItens enumItensIndisponivel = EnumItens.None;

    public ItemObject itemObjectCurrent;

    public override void Initialize(ItemPuzzle item)
    {
        base.Initialize(item);
        itemObjectCurrent = itemPuzzle.itens;
        this.gameObject.layer = 8;
        if (itemObjectCurrent.itemName == enumItensIndisponivel)
        {
            ItemManager.instancie.TradeItem(this);
            itemObjectCurrent = itemPuzzle.itens;
        }
        else
        {
            InstantiateItem();
        }

    }

    private void InstantiateItem()
    {
        StartCoroutine(ResetItem());
        itemObjectCurrent.ReturnItem();
        _gameObjectItem = Instantiate(itemPuzzle.itens.gameObject_Item, transform);
        _gameObjectItem.transform.localPosition = itemPuzzle.itens.offSet;
    }

    IEnumerator ResetItem()
    {
        this.enabled = false;
        yield return new WaitForSeconds(1);
        this.enabled = true;
    }

    //Metodo que chama ao Entrar
    public override void InteractionEnter(PointerEventData data)
    {
        //Deixa isso aqui, ele vai chamar o metodo Callback, depois de um tempo 
        base.InteractionEnter(data);
    }

    //Metodo que chama depois de um Tempo
    public override void CallBackAction()
    {
        if (ItemManager.instancie.itensSlot.GetItemObject())
        {
            DestroyItemAndAddItem(ItemManager.instancie.itensSlot.GetItemObject());
             ItemManager.instancie.ItemInHand(itemObjectCurrent);
        }
        else
        {   
            ItemManager.instancie.ItemInHand(itemObjectCurrent);
            DisableInteractive();
        }
        itemObjectCurrent = itemPuzzle.itens;
    }

    public void DestroyItemAndAddItem(ItemObject itemObject)
    {
        Destroy(_gameObjectItem, 0);
        itemPuzzle.itens = itemObject;
        InstantiateItem();
    }

    public override void DisableInteractive()
    {
        Destroy(_gameObjectItem, 0);
        itemObjectCurrent=null;
        this.gameObject.layer = 0;
        base.DisableInteractive();
    }

    // Metodo que vc chama ao sair da Intection
    public override void InteractionBack(PointerEventData data)
    {
        base.InteractionBack(data);
    }

    private void OnDestroy()
    {
        GameManager.instancie.InteractiveObject(false);
    }

    GameObject _gameObjectItem;

    Vector3 offSet;

}
