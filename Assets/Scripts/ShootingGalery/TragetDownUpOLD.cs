using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TragetDownUp : MonoBehaviour
{
    // Start is called before the first frame update
    public bool m_TargetIsUp;
    public Animation m_Animation;
    public AnimationClip m_TargetDownClip;
    public AnimationClip m_TargetUpClip;
    public static TragetDownUp instance;
    
    void Start()
    {
        m_TargetIsUp = false;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_TargetIsUp && ShootingGalery.instance.RoundHasStarted)
        {
            StartCoroutine(GoingUp());
        }

        if (m_TargetIsUp && ShootingGalery.instance.RoundHasStarted)
        {
            StartCoroutine(GoingDown());
        }

        
    }

    private IEnumerator GoingUp()
    {
        yield return new WaitForSeconds(2f);
        SetTargetUpAnimation();
        m_TargetIsUp = true;
    }

    private IEnumerator GoingDown()
    {
        yield return new WaitForSeconds(2f);
        SetTargetDownAnimation();
        m_TargetIsUp = false;
    }

    void SetTargetUpAnimation()
    {
        m_Animation.CrossFade(m_TargetUpClip.name);
        m_Animation.CrossFadeQueued(m_TargetDownClip.name);
    }

    void SetTargetDownAnimation()
    {
        m_Animation.CrossFade(m_TargetDownClip.name, 20f);
        m_Animation.CrossFadeQueued(m_TargetUpClip.name, 20f);
    }

    
}
