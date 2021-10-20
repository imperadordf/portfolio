using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class StartGameInteractive : PuzzleInteractive
{
   public Action actionMetodo;
    public bool isPlayOne;
    public override void Initialize(ItemPuzzle item = null)
    {
        base.Initialize(item);
    }

    public void ActionAdd(Action actionMetodo)
    {
        this.actionMetodo=actionMetodo;
    }

    public override void InteractionEnter(PointerEventData data)
    {
        if(!isPlayOne)
        GameManager.instancie.InteractiveObjetoTwo(true,CallBackAction,tempo_Interactive);
        else
        GameManager.instancie.InteractiveObject(true,CallBackAction,tempo_Interactive);

    }

    public override void CallBackAction()
    {
        actionMetodo();
    }

    public override void DisableInteractive()
    {
        base.DisableInteractive();
    }

    public override void InteractionBack(PointerEventData data)
    {
         if(!isPlayOne)
        GameManager.instancie.InteractiveObjetoTwo(false);
        else
        GameManager.instancie.InteractiveObject(false);
    }
}
