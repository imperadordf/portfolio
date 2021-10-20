using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using System;

public class MenuGerencer : MonoBehaviour
{

    public void LoadingScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("VR_Demo",UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
    private void Start()
    {
        TraderMenu(EnumMenuState.Inicial);
        AddEventInAnimations();
    }

    private void AddEventInAnimations()
    {
        foreach (var menu in menus)
        {
            menu.animationClasse.SetTimeEvent(() =>
            {
                menu.menuTransform.DOLocalMove(new Vector3(0, 0, 0), 1, false);
            }, 0f);
            menu.DesativeAllFilhos();
        }
    }

    public void TraderMenu(EnumMenuState state)
    {
        if (_menuAtual != null)
        {
            _menuAtual.DesativeAllFilhos();

            _menuAtual.menuTransform.DOLocalMove(caminhoFim.localPosition, 1, false).OnStart(() =>
            {
                SetEventCameraEvent(state);
                animatorCamera.SetTrigger(AnimatorStringState(state));
            });
            print("OI");
        }
        else
        {
            EnterState(state);
        }

    }

    private void SetEventCameraEvent(EnumMenuState state)
    {
        foreach (var menu in from menu in menus
                             where menu.menuName == state
                             select menu)
        {
            cameraEvent.SetEvent(menu.animationClasse.ReturnEvent());
            _menuAtual = menu;
            break;
        }
    }

    private string AnimatorStringState(EnumMenuState state)
    {
        switch (state)
        {
            case EnumMenuState.Inicial:
                return "Jogar";
            case EnumMenuState.SalaEspera:
                return "SalaEspera";
            default:
                return null;
        }
    }

    public void Teste()
    {
        TraderMenu(EnumMenuState.Inicial);
    }

    private void EnterState(EnumMenuState state)
    {
        foreach (var menu in from menu in menus
                             where menu.menuName == state
                             select menu)
        {
            menu.menuTransform.DOLocalMove(new Vector3(0, 0, 0), 2, false);
            _menuAtual = menu;
            break;
        }
    }

    [Header("Animation and Animator")]
    [SerializeField] Animator animatorCamera;
    [SerializeField] CameraEvent cameraEvent;

    [Space]
    [Header("Menu e Gerenciar")]
    [SerializeField] GameObject menuGameobject;

    [SerializeField] Menu[] menus;

    [Header("Fim")]
    [SerializeField] private Transform caminhoFim;

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    Menu _menuAtual;
}
public enum EnumMenuState
{
    Inicial,
    SalaEspera,
    Opcoes

}

[System.Serializable]
public class Menu
{
    public string nome;
    public EnumMenuState menuName;
    public Transform menuTransform;

    public AnimationClasse animationClasse;

    public GameObject[] gameObjectFilhos;

    public void DesativeAllFilhos()
    {
        foreach (var item in gameObjectFilhos)
        {
            item.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 1).OnComplete(() => item.SetActive(false));
        }
    }
}

[System.Serializable]
public class AnimationClasse
{
    public AnimationClip animationClip;

    EventAnimation EventAnimation;

    public void SetTimeEvent(EventAnimation eventAction, float timeEvent)
    {
        EventAnimation = eventAction;

        AnimationEvent animationEvent = new AnimationEvent();
        animationEvent.time = timeEvent;
        animationEvent.functionName = ("OnEvent");

        animationClip.AddEvent(animationEvent);
        //
    }

    public EventAnimation ReturnEvent() => (EventAnimation);

}

public delegate void EventAnimation();


