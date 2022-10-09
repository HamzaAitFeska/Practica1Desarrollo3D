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
        PlayerShield.instance.currentShield += Shieldextra;
        //Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
