using UnityEngine;

public class ShieldItem : MonoBehaviour
{
    private readonly int Shieldextra = 50;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (PlayerShield.instance.currentShield < 50)
        {
            if ((PlayerShield.instance.currentShield += Shieldextra) > 50)
            {
                PlayerShield.instance.currentShield = 50;
            }
            else
            {
                PlayerShield.instance.currentShield += Shieldextra;
            }

            Destroy(gameObject);
        }
       
    }
}
