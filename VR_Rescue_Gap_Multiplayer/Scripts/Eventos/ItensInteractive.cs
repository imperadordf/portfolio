using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItensInteractive : PuzzleInteractive
{
    public override void Initialize(ItemPuzzle item)
    {
        base.Initialize(item);
        if(item.itens)
        Instantiate(item.itens.gameObject_Item,transform);
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
       // ItemManager.instancie.ReturnItem();
    }

   // Metodo que vc chama ao sair da Intection
    public override void InteractionBack(PointerEventData data)
    {
        base.InteractionBack(data);
    }
}
