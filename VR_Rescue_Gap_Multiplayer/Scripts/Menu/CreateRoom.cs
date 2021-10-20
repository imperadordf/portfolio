using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

public class CreateRoom : MonoBehaviourPunCallbacks
{
    void Start()
    {
        buttonCreateRoom.onClick.RemoveAllListeners();

        buttonCreateRoom.onClick.AddListener(() =>
        {
            OpenMenuOptions(panelObject, panelBuscarPartida);
        });

        bt_BuscaRapida.onClick.AddListener(() =>
        {
            OpenMenuOptions(panelBuscarPartida, panelObject);
        });

        btCreateRoom.onClick.AddListener(Create);
    }


    public void Create()
    {
        _nameSala = inputNameRoom.text;

        if (_nameSala != "")
        {
            RoomOptions room = new RoomOptions();
            room.MaxPlayers = 2;
            room.IsVisible = true;
            room.IsOpen = true;
            RoomConnected.instancie.roomOptions = room;
            RoomConnected.instancie.nameSala = _nameSala;
            RoomConnected.instancie.iscreateRoom = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene("VR_Demo");
        }

    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("Sucesso");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print(PhotonNetwork.CurrentRoom.Name);
        print(PhotonNetwork.CurrentRoom.Players);
    }

    public void OpenMenuOptions(Transform openTransform, Transform quitTransform)
    {
        VerifiqueObjectOpen(quitTransform, () =>
        {
            openTransform.gameObject.SetActive(true);
            openTransform.DOScale(new Vector3(1, 1, 1), 0.5f);
        }
        );
    }

    public void VerifiqueObjectOpen(Transform objecttransform, Action callback)
    {
        if (objecttransform.gameObject.activeSelf)
        {
            objecttransform.transform.DOScale(new Vector3(0, 0, 0), 0.5f).OnComplete(() => { objecttransform.gameObject.SetActive(false); callback(); });
        }
        else
        {
            callback();
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for (int i = 0; i < fatherSala.childCount; i++)
        {
            Destroy(fatherSala.GetChild(i).gameObject);
        }

        foreach (var room in roomList)
        {
            Instantiate(prefabSala, fatherSala).GetComponent<RoomListInfo>().SetInfo(room);
        }
    }


    [Header("Entrada do Panel")]
    [SerializeField] private Button buttonCreateRoom;
    [SerializeField] private Button bt_BuscaRapida;
    [SerializeField] private Transform panelObject;

    [SerializeField] private Transform panelBuscarPartida;

    [Header("Criar Sala")]
    [SerializeField] private TMP_InputField inputNameRoom;
    [SerializeField] private Button btCreateRoom;

    [Header("Buscar Sala")]
    [SerializeField] private GameObject prefabSala;
    [SerializeField] private Transform fatherSala;
    string _nameSala;
}
