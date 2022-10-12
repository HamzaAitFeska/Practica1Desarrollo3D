using UnityEngine;

public class ShieldItem : MonoBehaviour
{
    private readonly int Shieldextra = 50;
    public GameObject Player;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerShield.instance.currentShield < 50)
        {
            //gameObject.GetComponent<Collider>().enabled = false;
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
