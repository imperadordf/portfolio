using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instancie;
    public MovementManager moviment_Manager;
    public Transform player_transform;

    [Header("Contador de Tempo")]
    public int timeGameMax;

    public TextMeshPro[] textMeshProContador;

    public PlayerRef playerDono;


    public PlayerRef playerOne;

    public PlayerRef playerTwo;

    float time;

    private void Awake()
    {
        if (!instancie)
        {
            instancie = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Initialize()
    {
        moviment_Manager.Inicialize();
        time = timeGameMax;
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {

        for (int i = timeGameMax; i > 0; i--)
        {
            time--;
            TimeSpan t = TimeSpan.FromSeconds(time);
            string tempoTexto = string.Format("{0:D2}:{1:D2}",
                 t.Minutes,
                 t.Seconds
                );
            foreach (TextMeshPro contador in textMeshProContador)
            {
                contador.text = tempoTexto;
            }
            yield return new WaitForSecondsRealtime(1);
        }
        GameFinish(playerGanhou: false);
    }

    public void InteractiveObject(bool interative, Interaction method = null, float timeInteraction = 0)
    {
        if (interative)
        {
            interaction?.InteractionOn(timeInteraction, method);
        }
        else
        {
            interaction?.InteractionOff();
        }
    }

    public void InteractiveObjetoTwo(bool interative, Interaction method = null, float timeInteraction = 0)
    {
        if (interative)
        {
            interactionTwo?.InteractionOn(timeInteraction, method);
        }
        else
        {
            interactionTwo?.InteractionOff();
        }
    }

    public void GameFinish(bool playerGanhou)
    {
        playerRefs = FindObjectsOfType<PlayerRef>();
        foreach (PlayerRef player in playerRefs)
        {
            player.FadeInTela(2, 2, () =>
            {
                Vector3 position = new Vector3(positionFinalGame.position.x, 1.8f, positionFinalGame.position.z);
                player.transform.position = position;
                waitRoom.SetActive(true);
                blackRoom.SetActive(false);
                outText.SetActive(false);
                player.playerAvatar.SetActive(false);
            }
            );
        }


        string playerName;
        if (playerGanhou)
            playerName = ReturnNamePlayer(enumPlayer.Player_Casa);
        else
            playerName = ReturnNamePlayer(enumPlayer.Player_Sabotador);

        txtStatusPartida.text = "O jogador " + playerName + " Ganhou a partida";
    }

    private String ReturnNamePlayer(enumPlayer playerEnum)
    {
        foreach (PlayerRef player in playerRefs)
        {
            if (player.PlayerIm == playerEnum)
                return player.namePlayer;
        }
        return null;
    }

    public void TerminouTask()
    {
        contadorMedalhao++;
        for (int i = 0; i < contadorMedalhao; i++)
        {
            medalhaoObject[i].SetActive(true);
        }
        if (contadorMedalhao > 2)
            GameFinish(playerGanhou: true);
    }



    public void SetReference(PlayerRef playerOne)
    {
        interaction = playerOne.interactionObject;
        this.playerOne = playerOne;
    }
    public void SetReferenceTwo(PlayerRef playerTwo)
    {
        interactionTwo = playerTwo.interactionObject;
        this.playerTwo = playerTwo;
    }

    public void AtivouSabotagem(Sabotagem sabotagem)
    {
        foreach (PuzzleFather puzzle in puzzleFathers)
        {   
            puzzle.DesactivePuzzle();
        }
        StartCoroutine(playerOne.SituantioText(sabotagem.ReturnInformationSabotagem()));
    }

    public void SolucionouSabotagem()
    {
         foreach (PuzzleFather puzzle in puzzleFathers)
        {   
            puzzle.ActivePuzzleLayer();
        }

        salaControleManager.SituationSabotagem(true);
    }

    public InteractionObject GetInteraction() => interaction;
    public InteractionObject GetInteractionTwo() => interactionTwo;

    [SerializeField] private InteractionObject interaction;
    [SerializeField] private InteractionObject interactionTwo;

    [Header("Puzzle")]
    [SerializeField] PuzzleFather[] puzzleFathers;

    [SerializeField] private SalaControleManager salaControleManager;


    [Header("Medalhoes")]
    [SerializeField] private GameObject[] medalhaoObject;
    private int contadorMedalhao = 0;

    [Space]
    [Header("Finish Game")]
    [SerializeField] private Transform positionFinalGame;
    [SerializeField] private GameObject blackRoom;
    [SerializeField] private GameObject waitRoom;
    [SerializeField] private GameObject outText;

    [SerializeField] TextMeshPro txtStatusPartida;

    private PlayerRef[] playerRefs;
}
