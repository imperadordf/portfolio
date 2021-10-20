using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.XR;

public class RoomConected : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public const string NAMEROOM = "SALA123";

    public List<Player> listPlayer = new List<Player>();
    private void Awake()
    {
        if (RoomConnected.instancie.iscreateRoom)
        {
            PhotonNetwork.CreateRoom(RoomConnected.instancie.nameSala, RoomConnected.instancie.roomOptions, TypedLobby.Default);
        }
        else
        {
            PhotonNetwork.JoinRoom(RoomConnected.instancie.nameSala);
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {   
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuAnimado");
        base.OnCreateRoomFailed(returnCode, message);
    }

    public override void OnJoinedRoom()
    {

        Debug.Log(PhotonNetwork.CountOfPlayersInRooms + "Numero de Pessoa");

        Vector3 position = new Vector3(spawn_Sala.position.x, 1.8f, spawn_Sala.position.z);
        playerOne = PhotonNetwork.Instantiate("Player", position, Quaternion.identity);

        GerencerPlayer();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuAnimado");
        base.OnJoinRoomFailed(returnCode, message);
    }

    private static void GerencerPlayer()
    {

        if (PhotonNetwork.CountOfPlayersInRooms >= 0)
            WaitForRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        listPlayer.Add(newPlayer);
        StartCoroutine(GameManager.instancie.playerOne.SituantioText(newPlayer.NickName + " Conectou a partida"));
        print("oi");
        base.OnPlayerEnteredRoom(newPlayer);
        WaitForRoom();
    }


    public static void WaitForRoom()
    {
        var players = FindObjectsOfType<PlayerRef>();
        string playerOnestring = PhotonNetwork.PlayerList?[0].NickName;
        print(players.Length);
        print(PhotonNetwork.PlayerList[0].NickName);
        foreach (PlayerRef player in players)
        {
            Debug.Log(player.MyphotonView.Controller.NickName);
            if (player.MyphotonView.Controller.NickName == playerOnestring)
            {
                GameManager.instancie.SetReference(player);
                player.PlayerIm = enumPlayer.Player_Casa;
            }
            else
            {
                GameManager.instancie.SetReferenceTwo(player);
                player.PlayerIm = enumPlayer.Player_Sabotador;
            }
        }

    }

    private void PlayerReady()
    {
        if (PhotonNetwork.CountOfPlayersInRooms >= 0)
        {
            if (_contPlay <= 1)
            {
                _contPlay++;
                string playerPronto = "Jogadores Pronto: " + _contPlay + "/2";
                textMeshPlay.text = playerPronto;
            }
            else
            {
                return;
            }

            if (_contPlay == 2)
                StartCoroutine(StartGameInSeconds());
        }
    }

    IEnumerator StartGameInSeconds()
    {
        for (int i = 3; i >= 0; i--)
        {
            string playerPronto = "Jogo começa em: " + i;
            txtEspereJogadores.text = playerPronto;
            yield return new WaitForSeconds(1);
        }
        StartGame();
        saladeControle.gameObject.SetActive(true);
        waitRoom.SetActive(false);
    }

    private static void StartGame()
    {
        var players = FindObjectsOfType<PlayerRef>();
        string playerOnestring = PhotonNetwork.PlayerList[0].NickName;
        print(players.Length);
        print(PhotonNetwork.PlayerList[0].UserId);
        foreach (PlayerRef player in players)
        {
            player.StartGame();
            print(player);
            if (player.namePlayer == playerOnestring)
            {
                GameManager.instancie.player_transform = player.transform;
                GameManager.instancie.SetReference(player);
                GameManager.instancie.Initialize();
                ItemManager.instancie.itensSlot = player.itens;
                player.PlayerIm = enumPlayer.Player_Casa;
            }
            else
            {
                GameManager.instancie.SetReferenceTwo(player);
                player.PlayerIm = enumPlayer.Player_Sabotador;
            }
        }

        ItemManager.instancie.Initialize();
    }

    private void Start()
    {
        tottemInteractive.actionMetodo = PlayerReady;
        textMeshPlay.gameObject.SetActive(true);
        tottemInteractive.Initialize();
        //if(MyphotonView.Controller.IsMasterClient)
        //MyphotonView.RPC("WaitForRoom",RpcTarget.AllBufferedViaServer);
    }

    private void Update()
    {

    }


    [SerializeField] private Transform spawn_Sala;
    [SerializeField] private Transform spawn_Casa;

    [SerializeField] private Transform playerTrasform;

    [Space]
    [SerializeField] private TextMeshPro txtEspereJogadores;

    [SerializeField] private TextMeshPro textMeshPlay;

    [SerializeField] TottemInteractive tottemInteractive;

    [SerializeField] GameObject waitRoom;

    [SerializeField] GameObject saladeControle;

    int _contPlay;

    private GameObject playerOne;

    [SerializeField] private PhotonView MyphotonView;

}
