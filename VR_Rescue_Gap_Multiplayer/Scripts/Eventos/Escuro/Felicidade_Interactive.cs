using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Felicidade_Interactive : PuzzleInteractive
{
    public override void Initialize(ItemPuzzle item = null)
    {
        base.Initialize(item);
    }

    public override void InteractionEnter(PointerEventData data)
    {   
        if(ItemManager.instancie.ItemNecessary(enumItemNecessary))
        base.InteractionEnter(data);
    }

    public override void CallBackAction()
    {
        animator.SetTrigger("OpenJanela");
        if(!felicidade_Manager.TerminouTask())
        StartCoroutine(FinishAnimator());
        else{
            felicidade_Manager.FinishPuzzle();
        }
        this.gameObject.layer=0;
    } 

     IEnumerator FinishAnimator()
    {
        ItemManager.instancie.itensSlot.currentItemObject.SetActive(false);
        yield return new WaitForSeconds(clip_Animation.length);
        ItemManager.instancie.itensSlot.currentItemObject.SetActive(true);
    }


    [SerializeField] private EnumItens enumItemNecessary;

    [SerializeField] private Animator animator;

    [SerializeField] private Felicidade_Manager felicidade_Manager;

    [SerializeField] private AnimationClip clip_Animation;
}
