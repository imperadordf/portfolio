using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonecosManager : PuzzleFather
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

     isActive=false;
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

     
     public Material ReturnMaterial(enumStateEstatua state){

         switch (state)
         {
             case enumStateEstatua.Feliz:
             return materialFeliz;
             case enumStateEstatua.Normal:
             return materialMedio;
             case enumStateEstatua.Triste:
             return materialTriste;
             default:
             return null;
             ;
         }
     }

    [SerializeField] Animator animatorCofre;

    [Header("Material da Estatua")]
    [SerializeField] private Material materialTriste,materialFeliz,materialMedio;
    private int contSolution=1;
    
}

public enum enumStateEstatua{
    Feliz,
    Normal,
    Triste,
}
