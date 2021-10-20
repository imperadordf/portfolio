using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TelaInteractive : PuzzleInteractiveSalaEscura
{
    public bool isContagemRight;
    private void Start()
    {
        Initialize();
    }
    public override void Initialize(ItemPuzzle item = null)
    {
        base.Initialize(item);
    }

    public override void CallBackAction()
    {
        TraderCamera();
        //salaControleManager.
    }

    private void TraderCamera()
    {   
        StartCoroutine(TraderCameraFeedBack());
        _contCameraAtual = salaControleManager.contagemCamera;

        cameras[_contCameraAtual].gameObject.SetActive(false);  

        if (isContagemRight)
        {
            _contCameraAtual++;
            if (_contCameraAtual >= cameras.Length)
                _contCameraAtual = 0;
        }
        else
        {
            _contCameraAtual--;
            if (_contCameraAtual < 0)
                _contCameraAtual = cameras.Length - 1;
        }


        salaControleManager.contagemCamera = _contCameraAtual;
        imagemTela.texture = textures[_contCameraAtual];
        cameras[_contCameraAtual].gameObject.SetActive(true);
        salaControleManager.NextMap();

    }

    IEnumerator TraderCameraFeedBack()
    {
        feedBackObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        feedBackObject.SetActive(false);
    }

    [SerializeField] private SalaControleManager salaControleManager;
    [Header("Cameras")]
    [SerializeField] Camera[] cameras;
    [SerializeField] Texture[] textures;

    [SerializeField] RawImage imagemTela;

    [SerializeField] GameObject feedBackObject;

    int _contCameraAtual;
}
