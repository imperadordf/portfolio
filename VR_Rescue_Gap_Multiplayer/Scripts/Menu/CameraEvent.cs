using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEvent : MonoBehaviour
{   
    EventAnimation eventAnimation;
    public void SetEvent(EventAnimation eventAnimation)
    {
        this.eventAnimation =  eventAnimation;
    }
    private void OnEvent()
    {   
        if(eventAnimation!=null)
        eventAnimation();
    }
}

