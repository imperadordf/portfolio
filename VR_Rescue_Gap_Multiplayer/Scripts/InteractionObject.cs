using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.XR;
using Photon.Pun;


public class InteractionObject : MonoBehaviour
{
    public PhotonView myphotonView;
    public void InteractionOn(float timeInteraction, Interaction method)
    {
        Interation = method;
        onDrag = true;
        this.timeInteraction = timeInteraction;
    }

    public void InteractionOff()
    {
        Interation = null;
        onDrag = false;
        img_ball.fillAmount = contTime = 0;
    }

    private void Update()
    {
        if (onDrag)
        {
            contTime += Time.deltaTime;
            img_ball.fillAmount = contTime / timeInteraction;
            if (timeInteraction <= contTime)
            {
                myphotonView.RPC("ActionPhoton", RpcTarget.AllBuffered);
            }
        }
    }

    [PunRPC]
    public void ActionPhoton()
    {
        Debug.Log(myphotonView.Controller.NickName);
        Interation();
        InteractionOff();
    }


    [SerializeField] private Image img_ball;


    private Interaction Interation;
    private bool onDrag;
    private float timeInteraction;
    float contTime;
    bool methodOn;
}

public delegate void Interaction();
