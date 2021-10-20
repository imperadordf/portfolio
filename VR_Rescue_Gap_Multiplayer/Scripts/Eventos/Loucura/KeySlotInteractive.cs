using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeySlotInteractive : PuzzleInteractive
{

    public override void Initialize(ItemPuzzle item = null)
    {
        base.Initialize(item);
        _audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        _audioSource.clip = audioGrito;
        _audioSource.Play();
    }


    public override void CallBackAction()
    {
        if (ItemManager.instancie.ItemNecessary(enumItemNecessary))
            FinishPuzzle();
    }

    private void FinishPuzzle()
    {
        animator.SetTrigger("KeyOn");
        StartCoroutine(FinishAnimator());
        DisableInteractive();
    }

     public override void DisableInteractive()
    {      
        this.enabled = false;
        this.gameObject.layer = 0;
    }

    IEnumerator FinishAnimator()
    {
        bool finish = !keyManager.IsFinish();
        if (finish)
            ItemManager.instancie.itensSlot.currentItemObject.SetActive(false);
        else
            ItemManager.instancie.RemoveItemHand();
        yield return new WaitForSeconds(clip_Key.length);
        if (!finish){
            keyManager.FinishPuzzle();
        }else
        ItemManager.instancie.itensSlot.currentItemObject.SetActive(true);
        _audioSource.Stop();
    }


    [SerializeField] private EnumItens enumItemNecessary;
    [SerializeField] private KeyManager keyManager;

    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip clip_Key;

    [SerializeField] private AudioClip audioGrito;

    private AudioSource _audioSource;
}
