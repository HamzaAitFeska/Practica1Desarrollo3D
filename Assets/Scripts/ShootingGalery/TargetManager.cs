using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public GameObject target1, target2, target3, target4, target5;
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
        if (ShootingGalery.instance.RoundHasStarted && !targetInProgress)
        {
            ResetTarget();
            ActivateTarget();
        }
        Debug.Log(FPSPlayerController.instance.m_TargetHit);
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
                targetId = 0;
                targetInProgress = false;
                break;
        }
    }
}