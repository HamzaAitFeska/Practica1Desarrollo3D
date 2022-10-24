using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Key;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DoorLocked.instance.playerHasKey = true;
            Key.SetActive(false);
        }
    }
}
