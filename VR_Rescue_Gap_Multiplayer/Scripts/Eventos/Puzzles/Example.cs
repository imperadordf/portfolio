using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Example : PuzzleInteractive
{
     //Inicia o Puzzle, deixa o Base.Initialize e adicione as novas funções do Codigo
   public override void Initialize(ItemPuzzle item)
    {
        base.Initialize(item);
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
        base.CallBackAction();
    }

   // Metodo que vc chama ao sair da Intection
    public override void InteractionBack(PointerEventData data)
    {
        base.InteractionBack(data);
    }
}
