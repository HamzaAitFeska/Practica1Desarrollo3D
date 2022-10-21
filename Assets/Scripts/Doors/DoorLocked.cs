using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLocked : MonoBehaviour
{
    public GameObject m_LockedDoor;
    public Animation m_Animation;
    public AnimationClip m_DoorOpeningClip;
    public AnimationClip m_DoorClosingClip;
    public bool playerHasKey = false;
    bool doorOpened = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && playerHasKey && !doorOpened)
        {
            SetDoorOpeningAnimation();
            doorOpened = true; ;
        }
    }
    void SetDoorOpeningAnimation()
    {
        m_Animation.CrossFadeQueued(m_DoorOpeningClip.name);
    }

    void SetDoorClosingAnimation()
    {
        m_Animation.CrossFade(m_DoorClosingClip.name,1f);
        m_Animation.CrossFadeQueued(m_DoorOpeningClip.name, 1f);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && doorOpened)
        {
            SetDoorClosingAnimation();
        }
    }

}
