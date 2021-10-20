using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.XR;
using TMPro;

public class PlayerRef : MonoBehaviourPun
{
   public PhotonView MyphotonView;

   public InteractionObject interactionObject;

   public ItensSlot itens;

   public Camera cameraObject;

   public string namePlayer;

   public MonoBehaviour [] scriptsMy;

   public AudioListener audioListener;

   public enumPlayer PlayerIm;

   public GameObject playerAvatar;

  private void Awake() {
     namePlayer = MyphotonView.Owner.NickName;

      if(!MyphotonView.IsMine){
         cameraObject.enabled=false;
         foreach(MonoBehaviour script in scriptsMy){
            script.enabled=false;
         }
         audioListener.enabled=false;
      }
      else{
         cameraObject.enabled=true;
         foreach(MonoBehaviour script in scriptsMy){
            script.enabled=true;
         }
          audioListener.enabled=true;
         StartCoroutine(StartVr());
      }
      
  }


    IEnumerator StartVr()
    {
        XRSettings.LoadDeviceByName("cardboard");

        yield return null;

        XRSettings.enabled = true;
    }
  
   private void Start() {

      namePlayer = MyphotonView.Owner.NickName;
      playerAvatar.SetActive(false);
     
         if(MyphotonView.IsMine){
            StartCoroutine(StartVr());
             GameManager.instancie.playerDono = this;
             playerAvatar.SetActive(false);
         }
         else
         {
          
         }
          RoomConected.WaitForRoom();
      

      StartGame();
   }

   public void StartGame(){
      
      FadeInTela(0,2);
      if(PlayerIm==enumPlayer.Player_Casa){
         playerAvatar.SetActive(true);
      }
      else{
         playerAvatar.SetActive(false);
      }

      if(photonView.IsMine){
         playerAvatar.SetActive(false);
      }
   }

   public void FadeInTela(float timeFadeIn, float timeFadeOut,Action callback=null)
   {
      
      imageFadeIn.gameObject.SetActive(true);
      //reticlePoint.SetActive(false);
      imageFadeIn.DOFade(1,timeFadeIn).OnComplete(()=>{if(callback!=null){
         callback();
      }});
      Invoke("FadeOut",timeFadeOut);
   }

   private void FadeOut(){
      imageFadeIn.DOFade(0,2).OnComplete(()=>{reticlePoint.SetActive(true); imageFadeIn.gameObject.SetActive(false);});
   }

   private void Update() {
     if(Input.GetKeyDown(KeyCode.L)){
        MyphotonView.RPC("MethodGanhar",RpcTarget.AllBuffered);
     }

   }

   [PunRPC]
   private void MethodGanhar(){
      GameManager.instancie.TerminouTask();
   }

   public IEnumerator SituantioText(string information)
   {
      txt_Situation.gameObject.SetActive(true);
      txt_Situation.DOFade(1,1);
      txt_Situation.text=information;
      yield return new WaitForSeconds(6);
      txt_Situation.DOFade(0,1);
   }



   [SerializeField] private Image imageFadeIn;
   [SerializeField] private GameObject reticlePoint;

   [SerializeField] private TextMeshProUGUI txt_Situation;

   //public void EntreiRoom()


}

public enum enumPlayer{
   Player_Casa,
   Player_Sabotador
}
