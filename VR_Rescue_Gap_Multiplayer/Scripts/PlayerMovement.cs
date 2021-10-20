using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private bool isWalk = true;
    public bool IsWalk1 { 
    get => isWalk; 
    set {
        if(value)
        isWalk = value;
        if(boxCollider)
        boxCollider.enabled=isWalk;
    }
    }

    public bool doorBoll; 

    public int myIndex;
    public void Inicialize(MovementManager manager,int myIndex)
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        #region Action para entradas
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => { InteractionMove((PointerEventData)data); });
        trigger.triggers.Add(entry);

        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerExit;
        entry2.callback.AddListener((data) => { InteractionBack((PointerEventData)data); });
        trigger.triggers.Add(entry2);
        #endregion
        IsWalk1 = true;
        Ref_manager = manager;
        this.myIndex = myIndex;

        if(!targetTransformPosition){
              targetTransformPosition=this.transform;
        }else{
            if(transform.childCount>0)
           _playerMovementChild = transform?.GetChild(0)?.GetComponent<PlayerMovement>();
           if(!_playerMovementChild)
           _playerMovementChild = targetTransformPosition.GetComponent<PlayerMovement>();
           _playerMovementChild.myIndex=myIndex;
           boxCollider=GetComponent<BoxCollider>();
        }
    }

    public void InteractionMove(PointerEventData data)
    {
        if(IsWalk1)
        {
            GameManager.instancie.InteractiveObject(true, Movement, time);
        }     
    }

    public void InteractionBack(PointerEventData data)
    {
        GameManager.instancie.InteractiveObject(false);
    }

    public void Movement()
    {
        float y = GameManager.instancie.player_transform.position.y;
        Vector3 position = targetTransformPosition.position;
        GameManager.instancie.player_transform.position = new Vector3(position.x, y, position.z);
        IsWalk1 = false;
        Ref_manager.AttCurrentWalk(myIndex);

        if(_playerMovementChild)
        {
           _playerMovementChild.IsWalk1=false;
           boxCollider.enabled=false;         
        }      
    }


    [SerializeField]private float time=1;
    [SerializeField] private float offSet_Y;

    [Header("Target Position, caso ele seja nulo, ele vai pega a posição do Script")]
    [SerializeField] private Transform targetTransformPosition;
    private MovementManager Ref_manager;

    private PlayerMovement _playerMovementChild;

    private BoxCollider boxCollider;

   
}
