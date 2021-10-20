using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;


[RequireComponent(typeof(EventTrigger))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(PhotonView))]
public class PuzzleInteractiveSalaEscura : MonoBehaviourPun
{
    private const string LayerName = "Interactive2";
    public PhotonView photon;
    public ItemPuzzle itemPuzzle;

    [Header("O Tempo para interagir com o Objeto")]
    public float tempo_Interactive = 2;

    //Inicio Colocando os Metodos que vão ser chamados
    public virtual void Initialize(ItemPuzzle item = null)
    {
        itemPuzzle = item;
        SetActionButton();
        this.gameObject.layer = LayerMask.NameToLayer(LayerName);
        photon = PhotonView.Get(this);
    }

    private void SetActionButton()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();

        #region Action para entradas
        //Colocando Metodo Quando Entra 
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => { InteractionEnter((PointerEventData)data); });
        trigger.triggers.Add(entry);

        //Colocando Metodo quando Sai
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerExit;
        entry2.callback.AddListener((data) => { InteractionBack((PointerEventData)data); });
        trigger.triggers.Add(entry2);
        #endregion
    }

    //Metodo chamado quando entra na collisão
    public virtual void InteractionEnter(PointerEventData data)
    {
        print("oi");
        //Mando o Metodo que eu vou executar depois de um tempo
        photon.RPC("ActionOn",RpcTarget.AllBuffered);
    }

    [PunRPC]
    public virtual void ActionOn()
    {
        GameManager.instancie.InteractiveObjetoTwo(true, CallBackAction, tempo_Interactive);
    }

    ////Metodo que eu vou executar Depois de um tempo
    [PunRPC]
    public virtual void CallBackAction()
    {
        
    }

    //Metodo que eu chamo ao Sair
    public virtual void InteractionBack(PointerEventData data)
    {
        photon.RPC("ActionOff",RpcTarget.AllBuffered);
    }

    [PunRPC]
    public virtual void ActionOff()
    {
        GameManager.instancie.InteractiveObjetoTwo(false);
    }

    public virtual void DisableInteractive()
    {

    }


}
