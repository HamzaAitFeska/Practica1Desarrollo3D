using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorElevator : MonoBehaviour
{
    public GameObject m_AutomaticDoor;
    public Animation m_Animation;
    public AnimationClip m_DoorOpeningClip;
    bool doorIsOpen = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !doorIsOpen)
        {
            SetDoorOpeningAnimation();
            doorIsOpen = true;
            AudioController.instance.PlayOneShot(AudioController.instance.doorElevatorOpening);
        }
    }
    void SetDoorOpeningAnimation()
    {
        m_Animation.CrossFadeQueued(m_DoorOpeningClip.name);
    }
}
