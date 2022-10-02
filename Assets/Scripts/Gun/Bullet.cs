using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private float life = 3;

    void Awake()
    {
        Destroy(gameObject, life);
    }

    // Update is called once per frame
   /* private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }*/
}
