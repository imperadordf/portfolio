using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class RoomListInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtNameSala;

    public void SetInfo(RoomInfo _roomInfo)
    {
        txtNameSala.text = _roomInfo.Name;
    }

    public void OnClick()
    {
        RoomConnected.instancie.nameSala = txtNameSala.text;
        RoomConnected.instancie.iscreateRoom=false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("VR_Demo");
    }
}
