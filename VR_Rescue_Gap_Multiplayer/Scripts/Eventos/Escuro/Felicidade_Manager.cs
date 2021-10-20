using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Felicidade_Manager : PuzzleFather
{
    public bool Finish;

    private void Start() {
        ActivePuzzle(null);
    }
    public override void ActivePuzzle(ItemPuzzle itemPuzzle)
    {
        base.ActivePuzzle(itemPuzzle);
    }

    public override void FinishPuzzle()
    {
        base.FinishPuzzle();
        ItemManager.instancie.RemoveItemHand();
        animatorCofre.SetTrigger("OpenCofre");
        GameManager.instancie.TerminouTask();
    }

    public bool TerminouTask()
    {   contPuzzle++;
        return contPuzzle>1;
    }

    public int contPuzzle=0;

    [SerializeField] private Animator animatorCofre;
}
