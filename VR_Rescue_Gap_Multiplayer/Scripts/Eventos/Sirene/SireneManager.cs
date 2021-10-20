using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SireneManager : SabotagemManager
{

  
    public override void ActivePuzzle(ItemPuzzle itemPuzzle)
    {
          if(!puzzleInActive)
        {
            base.ActivePuzzle(itemPuzzle);
            sprinklerGameObject.SetActive(true);
        }     
    }

    public bool SireneFinish()
    {
        contador++;
        if(contador>=2){
            FinishPuzzle();
            return false;
        }
        return true;
    }

    public override void FinishPuzzle()
    {
       sprinklerGameObject.SetActive(false);
       ItemManager.instancie.RemoveItemHand();
        GameManager.instancie.SolucionouSabotagem();

    }

    private void OnEnable() {
        sprinklerGameObject.SetActive(false);
    }

    [SerializeField] GameObject sprinklerGameObject;

    private int contador=0;   

}
