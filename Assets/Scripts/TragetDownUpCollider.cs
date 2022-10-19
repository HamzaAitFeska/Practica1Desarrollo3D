using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TragetDownUpCollider : MonoBehaviour
{
    public bool IsColliding;
    public static TragetDownUpCollider instance;
    
    private void Start()
    {
        instance = this;
    }
    private void Update()
    {
        Debug.Log(IsColliding);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DecalCollider"))
        {
            IsColliding = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("DecalCollider"))
        {
            IsColliding = true;
        }
    }
}
