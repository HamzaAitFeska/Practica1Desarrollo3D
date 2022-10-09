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

    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerAmmo.instance.currentmagSize += Ammodextra;
        //Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
