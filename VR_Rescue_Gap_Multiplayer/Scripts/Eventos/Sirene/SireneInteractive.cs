using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SireneInteractive : PuzzleInteractive
{

    public override void Initialize(ItemPuzzle item = null)
    {
        base.Initialize(item);
    }

    public override void InteractionEnter(PointerEventData data)
    {   
        if(ItemManager.instancie.ItemNecessary(EnumItens.Alavanca))
        base.InteractionEnter(data);
    }
 
    public override void CallBackAction()
    {
       animator.Play("Alavanca");
       if(sireneManager.SireneFinish())
       StartCoroutine(FinishAnimator());
       DisableInteractive();
    }

    IEnumerator FinishAnimator()
    {
        ItemManager.instancie.itensSlot.currentItemObject.SetActive(false);
        yield return new WaitForSeconds(clip_Sirene.length);
        ItemManager.instancie.itensSlot.currentItemObject.SetActive(true);
        waterSirene.SetActive(false);
    }

    public override void DisableInteractive()
    {
        gameObject.layer=0;
        base.DisableInteractive();
    }

    [Header("Logica e Animator")]
    [SerializeField] private SireneManager sireneManager;
    [SerializeField] GameObject waterSirene;
    
    [Space]
    [SerializeField] Animator animator;
    [SerializeField] AnimationClip clip_Sirene;
}
