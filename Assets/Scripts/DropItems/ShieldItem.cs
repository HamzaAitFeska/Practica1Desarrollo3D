using UnityEngine;

public class ShieldItem : MonoBehaviour
{
    private readonly int Shieldextra = 50;
    public GameObject Player;
    public Animation m_Animation;
    public AnimationClip m_ShieldItemIddleClip;
    void Start()
    {
        SetShieldItemIddleAnimation();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerShield.instance.currentShield < 50)
            {
                gameObject.GetComponent<Collider>().enabled = false;
                if ((PlayerShield.instance.currentShield += Shieldextra) > 50)
                {
                    PlayerShield.instance.currentShield = 50;
                }
                else
                {
                    PlayerShield.instance.currentShield += Shieldextra;
                }
                AudioController.instance.PlayOneShot(AudioController.instance.itemShield);
                Destroy(gameObject);
            }
        }
        
    }
    void SetShieldItemIddleAnimation()
    {
        m_Animation.CrossFadeQueued(m_ShieldItemIddleClip.name);
    }
}
