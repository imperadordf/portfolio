using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    PhotonView pv;

    public TabuleiroController tabuleiroController;

    public TabuleiroType tabuleiroType;
    
    private void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    public void Initialize()
    {
        if (pv.IsMine)
            pv.RPC("CreateTabuleiro", RpcTarget.AllBufferedViaServer, tabuleiroType);

        Debug.Log("FOI3");
    }

    [PunRPC]
    private void CreateTabuleiro(TabuleiroType type)
    {
        tabuleiroController.Initialize(type);
    }

}
