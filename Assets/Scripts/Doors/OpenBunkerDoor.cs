using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBunkerDoor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject AutomaticDoor;
    public Animation m_Animation;
    public AnimationClip m_DoorOpeningClip;
    bool doorIsOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && FPSPlayerController.instance.m_TotalPoints >= ShootingGalery.instance.ScoreObjective && !doorIsOpen)
        {
            SetDoorOpeningAnimation();
            doorIsOpen = true;
            AudioController.instance.PlayOneShot(AudioController.instance.doorLockedOpening);
        }
    }

    void SetDoorOpeningAnimation()
    {
        m_Animation.CrossFadeQueued(m_DoorOpeningClip.name);

    }
}
