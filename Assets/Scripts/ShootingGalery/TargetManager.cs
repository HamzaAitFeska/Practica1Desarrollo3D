using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public GameObject target1, target2, target3, target4, target5, target6, target7, target8, target9, target10;
    public bool targetInProgress;
    int targetId;

    public static TargetManager instance;
    void Start()
    {
        targetInProgress = false;
        targetId = 0;
        instance = this;
    }

    void Update()
    {
        if (ShootingGalery.instance.RoundHasStarted && !targetInProgress && targetId < 11)
        {
            SelectNextTarget();
        }
        
    }
    void ResetTarget()
    {
        FPSPlayerController.instance.m_TargetHit = false;
        targetInProgress = true;
    }
    
    void SelectNextTarget()
    {
        FPSPlayerController.instance.m_TargetHit = false;
        targetInProgress = true;
        if (targetId >= 10)
        {
            targetId = 1;
        }
        else targetId++;
        switch (targetId)
        {
            case 1:
                target1.SetActive(true);
                break;
            case 2:
                target2.SetActive(true);
                break;
            case 3:
                target3.SetActive(true);
                break;
            case 4:
                target4.SetActive(true);
                break;
            case 5:
                target5.SetActive(true);
                break;
            case 6:
                target6.SetActive(true);
                break;
            case 7:
                target7.SetActive(true);
                break;
            case 8:
                target8.SetActive(true);
                break;
            case 9:
                target9.SetActive(true);
                break;
            case 10:
                target10.SetActive(true);
                break;
        }
    }
    public void PlayerHasLeftTheArea()
    {
        targetId = 0;
        targetInProgress = false;

        target1.SetActive(false);
        target2.SetActive(false);
        target3.SetActive(false);
        target4.SetActive(false);
        target5.SetActive(false);
        target6.SetActive(false);
        target7.SetActive(false);
        target8.SetActive(false);
        target9.SetActive(false);
        target10.SetActive(false);
    }
}
