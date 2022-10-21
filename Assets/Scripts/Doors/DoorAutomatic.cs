using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAutomatic : MonoBehaviour
{
    public GameObject m_AutomaticDoor;
    public Animation m_Animation;
    public AnimationClip m_DoorOpeningClip;
    public AnimationClip m_DoorClosingClip;
    bool doorIsOpen = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !doorIsOpen)
        {
            SetDoorOpeningAnimation();
            doorIsOpen = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player") && doorIsOpen)
        {
            SetDoorClosingAnimation();
            doorIsOpen = false;
        }
    }
    void SetDoorOpeningAnimation()
    {
        m_Animation.CrossFadeQueued(m_DoorOpeningClip.name);
        
    }
    void SetDoorClosingAnimation()
    {
        m_Animation.CrossFadeQueued(m_DoorClosingClip.name);
       
    }
}
