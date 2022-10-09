using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LifeItem : MonoBehaviour
{
    // Start is called before the first frame update
    private readonly int Lifeextra = 50;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerLife.instance.currentLife += Lifeextra;
        //Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
