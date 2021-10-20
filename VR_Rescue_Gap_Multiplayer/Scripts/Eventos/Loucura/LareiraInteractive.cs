using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LareiraInteractive : PuzzleInteractive
{
    public override void Initialize(ItemPuzzle item = null)
    {
        base.Initialize(item);
        animator = GetComponent<Animator>();
        fireObject.SetActive(true);
    }


    public override void CallBackAction()
    {
        if (ItemManager.instancie.ItemNecessary(enumItemNecessary))
            FinishPuzzle();
    }

    private void FinishPuzzle()
    {
        animator.SetTrigger("WaterOn");
        StartCoroutine(FinishAnimator());
        GameManager.instancie.SolucionouSabotagem();
        DisableInteractive();
    }

     public override void DisableInteractive()
    {      
        this.enabled = false;
        this.gameObject.layer = 0;
    }

    IEnumerator FinishAnimator()
    {  
        ItemManager.instancie.RemoveItemHand();
        print(clip_Balde.length);
        yield return new WaitForSeconds(clip_Balde.length-1.2f);
        fireObject.SetActive(false);
    }

    private void OnEnable() {
        DisableInteractive();
        fireObject.SetActive(false);
    }


    [SerializeField] private EnumItens enumItemNecessary;


    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip clip_Balde;

    [SerializeField] private GameObject fireObject;
}
