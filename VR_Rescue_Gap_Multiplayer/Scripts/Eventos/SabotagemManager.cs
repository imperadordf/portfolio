using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SabotagemManager : PuzzleFather
{
    public override void ActivePuzzle(ItemPuzzle itemPuzzle)
    {
        if(!puzzleInActive)
        {
            base.ActivePuzzle();
            puzzleInActive=true;
            GameManager.instancie.AtivouSabotagem(itemPuzzle.sabotagem);
        }
    }

    public override void FinishPuzzle()
    {
        base.FinishPuzzle();
        GameManager.instancie.SolucionouSabotagem();
    }

    public override void DesactivePuzzle()
    {
        base.DesactivePuzzle();
    }
}
