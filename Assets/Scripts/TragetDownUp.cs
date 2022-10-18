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

    void Start()
    {
        m_IsUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_IsUp)
        {
            StartCoroutine(GoingUp());
        }

        if (m_IsUp)
        {
            StartCoroutine(GoingDown());
        }
    }

    private IEnumerator GoingUp()
    {
        yield return new WaitForSeconds(8f);
        SetTargetUpAnimation();
        m_IsUp = true;
    }

    private IEnumerator GoingDown()
    {
        yield return new WaitForSeconds(8f);
        SetTargetHitAnimation();
        m_IsUp = false;
    }

    void SetTargetUpAnimation()
    {
        m_Animation.CrossFade(m_TargetUpClip.name, 2f);
        m_Animation.CrossFadeQueued(m_TargetHitClip.name, 2f);
    }

    void SetTargetHitAnimation()
    {
        m_Animation.CrossFade(m_TargetHitClip.name, 1f);
        m_Animation.CrossFadeQueued(m_TargetUpClip.name, 1f);
    }
}
