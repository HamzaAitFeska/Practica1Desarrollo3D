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
            //PlayerLife.instance.CheckpointRotation = FPSPlayerController.instance.m_characterController.transform.rotation;
            PlayerLife.instance.CheckpointRotation = other.transform.rotation;
            AudioController.instance.PlayOneShot(AudioController.instance.uiWarning);
            Destroy(gameObject);
        }
    }
}
