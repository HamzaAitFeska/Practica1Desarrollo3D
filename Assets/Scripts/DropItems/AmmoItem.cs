using UnityEngine;

public class AmmoItem : MonoBehaviour
{
    private readonly int Ammodextra = 50;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerAmmo.instance.currentAmmo < 30)
        {
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(PlayerAmmo.instance.currentmagSize < 100)
        {
            if ((PlayerAmmo.instance.currentmagSize += Ammodextra) > 100)
            {
                PlayerAmmo.instance.currentmagSize = 100;
            }
            else
            {
                PlayerAmmo.instance.currentmagSize += Ammodextra;
            }
            //PlayerAmmo.instance.currentmagSize += Ammodextra;
            //Destroy(collision.gameObject);
            Destroy(gameObject);
        }
       
    }
}
