using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Key;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioController.instance.PlayOneShot(AudioController.instance.KeyPickup);
            DoorLocked.instance.playerHasKey = true;
            Key.SetActive(false);
        }
    }
}
