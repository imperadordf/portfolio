using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;

public class RoomConnected : MonoBehaviour
{
    public static RoomConnected instancie;

    public RoomOptions roomOptions;

    public string nameSala;

    public bool iscreateRoom;

    private void Awake()
    {
        if (!instancie)
        {
            instancie=this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
