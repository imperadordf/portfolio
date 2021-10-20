using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LixeiraInteractive : PuzzleInteractive
{
    //Inicia o Puzzle, deixa o Base.Initialize e adicione as novas funções do Codigo
    public override void Initialize(ItemPuzzle item=null)
    {
       base.Initialize(item);
    }

    //Metodo que chama ao Entrar
    public override void InteractionEnter(PointerEventData data)
    {
        //Deixa isso aqui, ele vai chamar o metodo Callback, depois de um tempo 
        
        if(!_interactive)
        return;
        base.InteractionEnter(data);
    }

    //Metodo que chama depois de um Tempo
    public override void CallBackAction()
    {  
        if(ItemManager.instancie.ItemNecessary(enumItemNecessary))
        FinishPuzzle();
      
    }

    private void FinishPuzzle()
    {
        DisableInteractive();
        ItemManager.instancie.RemoveItemHand();
    }

    public override void DisableInteractive()
    {      
        this.enabled = false;
        this.gameObject.layer = 0;
    }

    // Metodo que vc chama ao sair da Intection
    public override void InteractionBack(PointerEventData data)
    {
        base.InteractionBack(data);
    }
    
    [SerializeField] private EnumItens enumItemNecessary;

    [SerializeField]
    bool _interactive=true;
}
