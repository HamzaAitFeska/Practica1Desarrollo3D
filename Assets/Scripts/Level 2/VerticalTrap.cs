using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalTrap : MonoBehaviour
{
    public GameObject m_VerticalTrap;
    public Animation m_Animation;
    public AnimationClip m_TrapMovingClip;

    void Start()
    {
        SetTrapMovingAnimation();
    }

    void SetTrapMovingAnimation()
    {
        m_Animation.CrossFadeQueued(m_TrapMovingClip.name);
    }
}
