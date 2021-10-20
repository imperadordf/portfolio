using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class TottemSabotadorInteractive : PuzzleInteractiveSalaEscura
{
    public Action<ItemPuzzle> actionInteractive;

     private void Start() {
        Initialize();
    }

    public override void Initialize(ItemPuzzle item = null)
    {
        base.Initialize(item);
    }

    public override void CallBackAction()
    {
        ItemPuzzle itemObject = new ItemPuzzle();
        animator.SetTrigger("Ativar");
        itemObject.sabotagem = _sabotagem;
        materialIcone.SetColor("_EmissionColor",colorPadrao * -1);
        actionInteractive(itemObject);

        salaControleManager.SituationSabotagem(false);
    }

    public void StartCooldown(){
         StartCoroutine(_sabotagem.StartCooldown(()=>materialIcone.SetColor("_EmissionColor",colorPadrao * 1)));
    }
    
    public  override void InteractionEnter(PointerEventData data)
    {
        if(_sabotagem.sabotagemOn)
        {
            base.InteractionEnter(data);
        }
    }

    public void TraderTask(Sabotagem sabotagem)
    {
        materialIcone.EnableKeyword("_EMISSION");

       materialIcone.SetTexture("_BaseMap",sabotagem.imagemDaSabotagem);
        materialIcone.SetTexture("_EmissionMap",sabotagem.imagemDaSabotagem);

        actionInteractive = sabotagem.sabotagem.ActivePuzzle;
        _sabotagem=sabotagem;

        if(_sabotagem.sabotagemOn)
        materialIcone.SetColor("_EmissionColor",colorPadrao * 1);
        else
         materialIcone.SetColor("_EmissionColor",colorPadrao * -1);

    }

    private void OnEnable() {
        materialIcone.EnableKeyword("_EMISSION");

        colorPadrao = materialIcone.GetColor("_EmissionColor");
    }

    private void OnDisable() {

        materialIcone.SetColor("_EmissionColor",colorPadrao * 1);
    }

    [SerializeField] private Material materialIcone;

    [SerializeField] private Animator animator;

    [SerializeField] private SalaControleManager salaControleManager;

    Sabotagem _sabotagem;
    Color colorPadrao;
}


