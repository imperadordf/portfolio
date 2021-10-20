using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class TottemInteractive : PuzzleInteractive
{
    [SerializeField] PhotonView MyphotonView;
    public Action actionMetodo;
    public override void Initialize(ItemPuzzle item = null)
    {
        base.Initialize(item);
    }

    public void ActionAdd(Action actionMetodo)
    {
        this.actionMetodo=actionMetodo;
    }

    public override void InteractionEnter(PointerEventData data)
    {   
         if(GameManager.instancie.playerDono.PlayerIm==enumPlayer.Player_Casa && !playerCasa){
             GameManager.instancie.InteractiveObject(true, CallBackAction, tempo_Interactive);
             playerEnum=enumPlayer.Player_Casa;
        }else if (GameManager.instancie.playerDono.PlayerIm==enumPlayer.Player_Sabotador && !playerRoom){
            GameManager.instancie.InteractiveObjetoTwo(true, CallBackAction, tempo_Interactive);
            playerEnum=enumPlayer.Player_Sabotador;
        }
    }

     [PunRPC]
    public override void ActionOn()
    { 
        animator.SetTrigger("Ativar");
        actionMetodo();
        //OnEvent(null);
    }

    public override void CallBackAction()
    {
        if(playerEnum==enumPlayer.Player_Casa)
          DisableInteractive();
        else{
          DisableInteractive();
        }
        MyphotonView.RPC("ActionOn",RpcTarget.AllBuffered);
    }

    public override void DisableInteractive()
    {
        this.gameObject.layer=0;
    }

    public override void InteractionBack(PointerEventData data)
    {
       if(GameManager.instancie.playerDono.PlayerIm==enumPlayer.Player_Casa)
        GameManager.instancie.InteractiveObject(false);
        else{
         GameManager.instancie.InteractiveObjetoTwo(false);
        }
    }

    bool playerCasa, playerRoom;

    enumPlayer playerEnum;

    [SerializeField] Animator animator;

}
