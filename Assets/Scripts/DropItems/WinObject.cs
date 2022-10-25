using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioController.instance.PlayOneShot(AudioController.instance.KeyPickup);
            //PLAYER WIN SCREEN
            Destroy(gameObject);
        }
    }
}
