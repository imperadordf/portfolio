using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : PuzzleFather
{
     public bool isActive;
   private void Start() { 
       if(isActive){
            ActivePuzzle(null);
       }   
       else{
         foreach (var item in puzzlesActive)
         { 
           item.DisableInteractive();
         }
       }   

     isActive=true;
   }

    public override void ActivePuzzle(ItemPuzzle itemPuzzle)
    {
        ItemPuzzle itemObject = new ItemPuzzle(){
             //puzzleFather = this,
        };
        
        foreach (var item in puzzlesActive)
       { 
           item.Initialize(itemObject);
       }
    }

    public bool IsFinish()
    {
        contSolution++;
        return contSolution>2;

    }

    public override void FinishPuzzle()
    {
        GameManager.instancie.TerminouTask();
        animatorCofre.SetTrigger("OpenCofre");
    }


    private int contSolution=1;
    [SerializeField] Animator animatorCofre;
}
