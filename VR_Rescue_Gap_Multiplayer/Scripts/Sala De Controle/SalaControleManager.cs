using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalaControleManager : MonoBehaviour
{

    public int contagemCamera;
    // Start is called before the first frame update
    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        foreach (var task in taskSabotagem)
        {
            task.SituatationSabotagem(true);
        }
        NextMap();
    }

    public void NextMap()
    {
        if (taskSabotagem[contagemCamera].sabotagems.Length > 0)
            tottem.TraderTask(taskSabotagem[contagemCamera].sabotagems[0]);
    }

    public void SituationSabotagem(bool active)
    {
        foreach (var task in taskSabotagem)
        {
            task.SituatationSabotagem(active);
        }

        if(active){
            tottem.StartCooldown();
        }
    }

    [Header("Tottem")]
    [SerializeField] private TottemSabotadorInteractive tottem;

    [Header("Sabotagem")]
    [SerializeField] TaskSabotagem[] taskSabotagem;
}

[System.Serializable]
public class TaskSabotagem
{
    public string nameSala;

    [Header("Sabotagem")]
    public Sabotagem[] sabotagems;

    public void SituatationSabotagem(bool sabotagemOn)
    {
        foreach (var sabotagem in sabotagems)
        {
            sabotagem.sabotagemOn=sabotagemOn;
        }
    }

}

[System.Serializable]
public class Sabotagem
{
    public string nameSabotagem;
    public PuzzleFather sabotagem;
    public Texture imagemDaSabotagem;

    public ItemObject itemSolution;

    public bool sabotagemOn = true;

    public IEnumerator StartCooldown(Action callback)
    {
        sabotagemOn = false;
        yield return new WaitForSeconds(60);
        sabotagemOn = true;
        callback();
        if(itemSolution)
        ItemManager.instancie.SpawnItemSabotagem(itemSolution);
    }

    public string ReturnInformationSabotagem()
    {
        return "A Sabotagem foi Ativada " + nameSabotagem + ", resolva para voltar os puzzle";
    }
}


