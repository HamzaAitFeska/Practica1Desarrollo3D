using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StartLevel2 : MonoBehaviour
{
    GameController gameController = GameController.GetGameController();
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartLevelTwo();
        }
    }
    void StartLevelTwo()
    {
        gameController.NextLevel();
        gameObject.SetActive(false);
    }
}
