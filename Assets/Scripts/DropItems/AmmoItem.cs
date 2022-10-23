using UnityEngine;

public class AmmoItem : MonoBehaviour
{
    private readonly int Ammodextra = 100;
    public Animation m_Animation;
    public AnimationClip m_AmmoItemIddleClip;
    void Start()
    {
        SetAmmoItemIddleAnimation();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (PlayerAmmo.instance.currentmagSize < 145)
        {
            if ((PlayerAmmo.instance.currentmagSize += Ammodextra) > 145)
            {
                PlayerAmmo.instance.currentmagSize = 145;
            }
            else
            {
                PlayerAmmo.instance.currentmagSize += Ammodextra;
            }
            //PlayerAmmo.instance.currentmagSize += Ammodextra;
            //Destroy(collision.gameObject);
            AudioController.instance.PlayOneShot(AudioController.instance.itemAmmo);
            Destroy(gameObject);
        }
    }
    void SetAmmoItemIddleAnimation()
    {
        m_Animation.CrossFadeQueued(m_AmmoItemIddleClip.name);
    }
}
