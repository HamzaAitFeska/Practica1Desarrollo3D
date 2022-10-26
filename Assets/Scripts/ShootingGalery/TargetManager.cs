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
            ResetTarget();
            ActivateTarget();
        }
        else if (targetId >= 10)
        {
            targetId = 0;
        }
        Debug.Log(targetId);
    }
    void ResetTarget()
    {
        FPSPlayerController.instance.m_TargetHit = false;
        targetInProgress = true;
        targetId++;
    }
    void ActivateTarget()
    {
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
}
