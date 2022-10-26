using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StartLevel2 : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartLevelTwo();
        }
    }
    void StartLevelTwo()
    {
        GameController.GetGameController().NextLevel();
        gameObject.SetActive(false);
    }
}
