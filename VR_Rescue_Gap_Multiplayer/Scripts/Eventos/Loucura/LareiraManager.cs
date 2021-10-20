using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LareiraManager : SabotagemManager
{
     public bool isActive;

    public override void ActivePuzzle(ItemPuzzle itemPuzzle)
    {
      if(!puzzleInActive){
           ItemPuzzle itemObject = new ItemPuzzle(){
             //puzzleFather = this,
        };
        
        foreach (var item in puzzlesActive)
       { 
           item.Initialize(itemObject);
       }
        puzzleInActive=true;
      }
    }

   
    public override void FinishPuzzle()
    {
        GameManager.instancie.SolucionouSabotagem();
    }
     


}
