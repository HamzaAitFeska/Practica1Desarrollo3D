using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TragetDownUp : MonoBehaviour
{
    // Start is called before the first frame update
    public bool m_IsUp;
    public Animation m_Animation;
    public AnimationClip m_TargetHitClip;
    public AnimationClip m_TargetUpClip;
    public static TragetDownUp instance;
    
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_IsUp && ShootingGalery.instance.StartAnims)
        {
            StartCoroutine(GoingUp());
        }

        if (m_IsUp && ShootingGalery.instance.StartAnims)
        {
            StartCoroutine(GoingDown());
        }

        
    }

    private IEnumerator GoingUp()
    {
        yield return new WaitForSeconds(2f);
        SetTargetUpAnimation();
        m_IsUp = true;
    }

    private IEnumerator GoingDown()
    {
        yield return new WaitForSeconds(2f);
        SetTargetHitAnimation();
        m_IsUp = false;
    }

    void SetTargetUpAnimation()
    {
        m_Animation.CrossFade(m_TargetUpClip.name, 20f);
        m_Animation.CrossFadeQueued(m_TargetHitClip.name, 20f);
    }

    void SetTargetHitAnimation()
    {
        m_Animation.CrossFade(m_TargetHitClip.name, 20f);
        m_Animation.CrossFadeQueued(m_TargetUpClip.name, 20f);
    }

    
}
