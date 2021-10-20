using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleFather : MonoBehaviour
{
    public List<PuzzleInteractive> puzzlesActive = new List<PuzzleInteractive>();

    [Space]
    public ItemObject itemResolvedSabotagem;

    public bool puzzleInActive;

    public virtual void ActivePuzzle(ItemPuzzle itemPuzzle =null)
    {
        foreach(PuzzleInteractive puzzles in puzzlesActive)
        {
            puzzles.Initialize(_itens);
        }
    }

    public virtual void DesactivePuzzle()
    {
         foreach(PuzzleInteractive puzzles in puzzlesActive)
        {
            puzzles.gameObject.layer=0;
        }
    }

    public virtual void ActivePuzzleLayer(){
        
         foreach(PuzzleInteractive puzzles in puzzlesActive)
        {
            puzzles.gameObject.layer=8;
        }
    }

    public virtual void FinishPuzzle()
    {
        DesactivePuzzle();
    }

    public virtual void ReturnSabotagem(){
        
    }
    

    private ItemPuzzle _itens = new ItemPuzzle();
    
}
