using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject AutomaticDoor;
    public Animation m_Animation;
    public AnimationClip m_DoorOpeningClip;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && FPSPlayerController.instance.m_TotalPoints >= 500)
        {
            SetDoorOpeningAnimation();
        }
    }

    void SetDoorOpeningAnimation()
    {
        m_Animation.CrossFadeQueued(m_DoorOpeningClip.name);

    }
}
