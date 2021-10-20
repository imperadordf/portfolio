using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Photon.Pun;

public class Energia_Interactive : PuzzleInteractive
{
    [Range(1, 90)]
    public float anguloXCorrect;

    public bool correctEnergia=true;

    public override void Initialize(ItemPuzzle item = null)
    {
        base.Initialize(item);
        CallBackAction();

        if(anguloXCorrect>1){
            anguloIncorrect = 0;
        }
        else{
            anguloIncorrect = 90;
        }

    }
    [PunRPC]
    public override void CallBackAction()
    {
        transform.DOLocalRotate(MudarArgulhoVector(),2,RotateMode.Fast).OnComplete(()=>{
            VerificarEnergia();
             if(correctEnergia)
            itemPuzzle.puzzleFather.FinishPuzzle();
            });
    }

      public Vector3 MudarArgulhoVector()
    {
        Vector3 Vector3correct;

        VerificarEnergia();
        if (!correctEnergia)
            Vector3correct = new Vector3(anguloXCorrect, 0, 0);

        else
            Vector3correct = new Vector3(anguloIncorrect, 0, 0);

        return Vector3correct;
        
    }

    private void VerificarEnergia()
    {
        correctEnergia = transform.localEulerAngles.x == anguloXCorrect;
    }

    float anguloIncorrect;

}

