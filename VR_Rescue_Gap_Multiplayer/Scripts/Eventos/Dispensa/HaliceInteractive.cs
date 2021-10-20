using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HaliceInteractive : PuzzleInteractive
{
   
    public override void Initialize(ItemPuzzle item = null)
    {
        _isActive=true;
        animatorHalice.SetBool("HeliceForte",true);
        ventoForte.SetActive(true);
        ventoFraco.SetActive(false);
        foreach (var movement in playerMovement)
        {
            movement.gameObject.layer=0;
        }
        base.Initialize();
    }

    public override void InteractionEnter(PointerEventData data)
    {
        base.InteractionEnter(data);
    }

    public override void CallBackAction()
    {
        animatorHalice.SetBool("HeliceForte",false);
        animatorCaixa.Play("Caixa_de_energia");
        DisableInteractive();
        GameManager.instancie.SolucionouSabotagem();
    }

    public override void DisableInteractive()
    {
        ventoForte.SetActive(false);
        ventoFraco.SetActive(true);
        base.DisableInteractive();
         _isActive=false;
         this.gameObject.layer=0;
        foreach (var movement in playerMovement)
        {
            movement.gameObject.layer=8;
        }
    }

    public override void InteractionBack(PointerEventData data)
    {
        base.InteractionBack(data);
    }

    [SerializeField] Animator animatorHalice;

   [SerializeField] bool _isActive;

   [SerializeField]Animator animatorCaixa;

   [SerializeField] GameObject ventoFraco,ventoForte;

   [SerializeField] PlayerMovement[] playerMovement;

}
