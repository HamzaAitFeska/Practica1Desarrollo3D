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
        if (PlayerLife.instance.currentLife < 100)
        {
            if ((PlayerLife.instance.currentLife += Lifeextra) > 100)
            {
                PlayerLife.instance.currentLife = 100;
            }
            else
            {
                PlayerLife.instance.currentLife += Lifeextra;
            }
            
            Destroy(gameObject);
        }
       
    }
}
