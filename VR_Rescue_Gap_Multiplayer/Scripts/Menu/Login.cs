
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Photon.Pun;
using System;
using UnityEngine.XR;

public class Login : MonoBehaviourPunCallbacks
{
    [SerializeField] MenuGerencer menuGerencer;
    private void Awake()
    {
        FeedBackOn(false);
    }
    private void Start() => Initialize();

    public void Initialize()
    {
        bt_LoginEntrar.onClick.AddListener(LoginGame);
        bt_loginMenu.onClick.AddListener(() =>
        {
            if(!PhotonNetwork.IsConnected)
            FeedBackOn(true);
            else
            menuGerencer.TraderMenu(EnumMenuState.SalaEspera);
        });
        FeedBackOn(false);

        Screen.orientation = ScreenOrientation.AutoRotation;
        XRSettings.enabled = false;
    }

    private void LoginGame()
    {

        if (input_Name.text != "")
        {
            bt_LoginEntrar.enabled = false;
            StatusFeedback("Conectando...");
            PhotonNetwork.NickName = input_Name.text;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
            StatusFeedback("Nome não pode ser Vazio");


    }

    public override void OnConnectedToMaster()
    {
        StatusFeedback("Conectou");
        base.OnConnectedToMaster();
        Invoke("NextMenu", 1);
    }

    public override void OnCustomAuthenticationFailed(string debugMessage)
    {
        base.OnCustomAuthenticationFailed(debugMessage);
    }

    private void NextMenu() => FeedBackOn(false, () =>
    {
        menuGerencer.TraderMenu(EnumMenuState.SalaEspera);
        StatusFeedback("Entrar");
        bt_LoginEntrar.enabled = true;
        PhotonNetwork.JoinLobby();
        //SceneManager.LoadScene("VR_Demo");
    });

    private void StatusFeedback(string Text_Feedback = "")
    {
        txt_Carregando.text = Text_Feedback;
        // FeedBackOn(true);
    }

    public void FeedBackOn(bool OnFeedback, Action Callback = null)
    {
        Transform statusTransform = statusObject.transform;
        if (OnFeedback)
            statusTransform.DOScale(new Vector3(1, 1, 1), 0.3f)
                    .OnPlay(() => statusObject.gameObject.SetActive(true));
        else
            statusTransform.DOScale(new Vector3(0, 0, 0), 0.3f)
                           .OnComplete(() => { statusObject.gameObject.SetActive(false); Callback(); });
    }


    [Header("Action Menu")]

    [SerializeField] Button bt_loginMenu;
    [SerializeField] Button bt_LoginEntrar;
    [SerializeField] TMP_InputField input_Name;

    [Header("FeedBack")]
    [SerializeField] GameObject statusObject;
    [SerializeField] TextMeshProUGUI txt_Carregando;
}
