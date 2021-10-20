using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LixeiraManager : PuzzleFather
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

    public void SolutionBoneco()
    {
        contSolution++;
        if(contSolution>3)   
        FinishPuzzle();  
    }

    public override void FinishPuzzle()
    {
         animatorCofre.SetTrigger("OpenCofre");
        GameManager.instancie.TerminouTask();
    }
     


    private int contSolution=1;
    [SerializeField] Animator animatorCofre;
    
}
