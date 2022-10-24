using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LifeItem : MonoBehaviour
{
    // Start is called before the first frame update
    private readonly int Lifeextra = 100;
    public Animation m_Animation;
    public AnimationClip m_HealthItemIddleClip;
    void Start()
    {
        SetHealthItemIddleAnimation();
    }
    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerLife.instance.currentLife < 100)
            {
                PlayerLife.instance.currentLife += Lifeextra;
                if ((PlayerLife.instance.currentLife += Lifeextra) >= 100)
                {
                    PlayerLife.instance.currentLife = 100;
                }
                else
                {
                    PlayerLife.instance.currentLife += Lifeextra;
                }
                AudioController.instance.PlayOneShot(AudioController.instance.itemHealth);
                PlayerLife.instance.Overlay.color = new Color(0, 255, 0, 0.85f);
                PlayerLife.instance.duration = 0;
                Destroy(gameObject);
            }
        }
         
    }
    void SetHealthItemIddleAnimation()
    {
        m_Animation.CrossFadeQueued(m_HealthItemIddleClip.name);
    }
}
