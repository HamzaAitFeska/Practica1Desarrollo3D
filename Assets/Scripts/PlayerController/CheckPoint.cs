using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerLife.instance.Checkpoint = transform.position;
            AudioController.instance.PlayOneShot(AudioController.instance.uiWarning);
            Destroy(gameObject);
        }
    }
}
