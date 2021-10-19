using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ManagerSlot : MonoBehaviour
{
 
    public static ManagerSlot instancie;

    private void Awake()
    {
        if (instancie == null)
        {
            instancie = this;
        }
    }

    private void Start()
    {
        foreach (Slot_Engenhagem slots in slotList_UI)
        {
            slots.isVoid = false;
        }
    }

    public void Contador()
    {

        foreach (Drag_Engenhagem draglist in drag_Engenhagem)
        {
            if (draglist.State == EnumEngenhagem.Slot_Engenhagem || draglist.State == EnumEngenhagem.Slot_EngenhagemAntiHorario)
            {
                contador_Engenhagem++;
            }
            else
            {
                contador_Engenhagem = 0;
            }
        }

        if (contador_Engenhagem >= drag_Engenhagem.Count)
        {
            foreach (Drag_Engenhagem draglist in drag_Engenhagem)
            {
                draglist.StartRotate();
            }
            text_Nugget.text = "VOCE CONCLUIU A TASK";
        }
        else
        {
            foreach (Drag_Engenhagem draglist in drag_Engenhagem)
            {
                draglist.StopRotate();
            }
            text_Nugget.text = "ENCAIXE AS ENGRENAGENS EM QUALQUER ORDEM!";
        }
       
      
    } 
    
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    [SerializeField]
    List<Drag_Engenhagem> drag_Engenhagem = new List<Drag_Engenhagem>();

    [SerializeField]
    List<Slot_Engenhagem> slotList_UI = new List<Slot_Engenhagem>();

    [SerializeField]
    public TextMeshProUGUI text_Nugget;
   
   int contador_Engenhagem = 0;
    
}
