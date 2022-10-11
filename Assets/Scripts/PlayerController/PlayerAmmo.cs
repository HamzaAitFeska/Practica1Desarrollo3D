using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerAmmo : MonoBehaviour
{
    // Start is called before the first frame update
    private int maxAmmo = 30;
    private int maxMagSize = 100;
    [NonSerialized]public int currentmagSize;
    [NonSerialized]public int currentAmmo;
    public TMP_Text textAmmo;
    public TMP_Text textmagSize;
    public static PlayerAmmo instance;
    void Start()
    {
        currentAmmo = maxAmmo;
        currentmagSize = maxMagSize;
        textAmmo.text = currentAmmo.ToString();
        textmagSize.text = currentmagSize.ToString();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        textAmmo.text = currentAmmo.ToString();
        textmagSize.text = currentmagSize.ToString();
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetAmmo();
            SetReloadAnimation();
        }

    }

    public void LoseAmmo()
    {
        currentAmmo--;
    }
    private void ResetAmmo()
    {
        float diferenciaAmmo;
        diferenciaAmmo = maxAmmo - currentAmmo;
        currentAmmo += (int)diferenciaAmmo;
        currentmagSize -= (int)diferenciaAmmo;
    }

    void SetReloadAnimation()
    {
        FPSPlayerController.instance.m_Animation.CrossFade(FPSPlayerController.instance.m_ReloadClip.name,0.1f);
        FPSPlayerController.instance.m_Animation.CrossFadeQueued(FPSPlayerController.instance.m_IdleClip.name, 0.1f);
    }
}
