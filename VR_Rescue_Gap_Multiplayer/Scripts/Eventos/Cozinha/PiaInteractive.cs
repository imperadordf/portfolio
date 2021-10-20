using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PiaInteractive : PuzzleInteractive
{
   
    public override void Initialize(ItemPuzzle item = null)
    {
        base.Initialize(item);
        particleWater.SetActive(true);
        playerMovement.gameObject.layer=0;
    }

    public override void InteractionEnter(PointerEventData data)
    {
        base.InteractionEnter(data);
    } 

    public override void CallBackAction()
    {
      if(ItemManager.instancie.ItemNecessary(EnumItens.ChaveDeBoca))
      { 
        particleWater.SetActive(false);
        DisableInteractive();
        ItemManager.instancie.RemoveItemHand();
        GameManager.instancie.SolucionouSabotagem();
      } 
    }

    public override void DisableInteractive()
    {
        base.DisableInteractive();
        gameObject.layer=0;
        playerMovement.gameObject.layer=8;
    }

    public override void InteractionBack(PointerEventData data)
    {
        base.InteractionBack(data);
    }

    [SerializeField] GameObject particleWater;

    [SerializeField] PlayerMovement playerMovement;

    private void OnEnable() {
        particleWater.SetActive(false);
    }
}
