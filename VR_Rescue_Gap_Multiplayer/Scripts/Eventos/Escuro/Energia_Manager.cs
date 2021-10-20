using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energia_Manager : SabotagemManager
{

    public override void ActivePuzzle(ItemPuzzle itemPuzzle)
    {
        if(!puzzleInActive){
              
        ItemPuzzle itemObject = new ItemPuzzle()
        {
            puzzleFather = this,
        };

        foreach (var item in puzzlesActive)
        {
            item.Initialize(itemObject);
            energias.Add(item.GetComponent<Energia_Interactive>());
            if(_contAleatorio<4)
            {   
                 System.Random random = new System.Random();
                 int randomInt = random.Next(puzzlesActive.Count);
                 puzzlesActive[randomInt].CallBackAction();
                _contAleatorio++;
            }
        }
            puzzleInActive=true;
            GameManager.instancie.AtivouSabotagem(itemPuzzle.sabotagem);
        }
        
    }


    public override void FinishPuzzle()
    {

        foreach (Energia_Interactive energia in energias)
        {
            if (!energia.correctEnergia)
                return;
        }

       Invoke("AnimatorOn",1);
       GameManager.instancie.SolucionouSabotagem();

    }

    private void AnimatorOn()
    {
       
    }

    [SerializeField] Animator animatorCofre;

    int _contAleatorio;
    List<Energia_Interactive> energias = new List<Energia_Interactive>();
}
