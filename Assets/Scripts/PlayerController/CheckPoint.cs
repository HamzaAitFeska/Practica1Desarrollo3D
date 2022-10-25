using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerLife.instance.CheckpointPosition = transform.position;
            //PlayerLife.instance.CheckpointRotation = Quaternion.Euler(0f, other.GetComponent, 0f);
            AudioController.instance.PlayOneShot(AudioController.instance.uiWarning);
            Destroy(gameObject);
        }
    }
}
