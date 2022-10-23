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
            AudioController.instance.PlayOneShot(AudioController.instance.doorLockedOpening);
        }
    }
    void SetDoorOpeningAnimation()
    {
        m_Animation.CrossFadeQueued(m_DoorOpeningClip.name);
    }

}
