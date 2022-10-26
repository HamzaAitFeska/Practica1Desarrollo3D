using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    bool m_TargetIsUp;
    public bool m_TargetIsMobile;
    public Animation m_Animation;
    public AnimationClip m_TargetDownClip;
    public AnimationClip m_TargetUpClip;
    public AnimationClip m_TargetMovingClip;
    public static TargetController instance;
    public GameObject targetToHide;

    void Start()
    {
        m_TargetIsUp = false;
        instance = this;
    }
    void Update()
    {
        if (!m_TargetIsUp && ShootingGalery.instance.RoundHasStarted)
        {
            ActivateThisTarget();
        }
        else if (FPSPlayerController.instance.m_TargetHit)
        {
            SetTargetDownAnimation();
            TargetHasBeenHit();
        }
    }
    void TargetHasBeenHit()
    {
        FPSPlayerController.instance.m_TargetHit = false;
        HitColliderTarget.instance.GivePoints();
        m_TargetIsUp = false;
        TargetManager.instance.targetInProgress = false;
        m_Animation.Stop();
        targetToHide.SetActive(false);
    }
    void ActivateThisTarget()
    {
        if (m_TargetIsMobile)
        {
            SetTargetMovingAnimation();
        }
        else
        {
            SetTargetUpAnimation();
        }
        
        m_TargetIsUp = true;
    }
    
    void SetTargetUpAnimation()
    {
        m_Animation.CrossFadeQueued(m_TargetUpClip.name);
    }

    void SetTargetMovingAnimation()
    {
        m_Animation.CrossFadeQueued(m_TargetMovingClip.name);
    }
    void SetTargetDownAnimation()
    {
        m_Animation.CrossFadeQueued(m_TargetDownClip.name);
    }
}
