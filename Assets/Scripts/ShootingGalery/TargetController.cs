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
        if (FPSPlayerController.instance.m_TargetHit)
        {
            TargetHasBeenHit();
        }
    }
    void TargetHasBeenHit()
    {
        HitColliderTarget.instance.Hit();
        m_TargetIsUp = false;
        HitColliderTarget.instance.HideTarget();
        TargetManager.instance.targetInProgress = false;
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
        //m_Animation.CrossFade(m_TargetUpClip.name);
        m_Animation.CrossFadeQueued(m_TargetUpClip.name);
    }

    void SetTargetMovingAnimation()
    {
        m_Animation.CrossFadeQueued(m_TargetMovingClip.name);
    }
}
