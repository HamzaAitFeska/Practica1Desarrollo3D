using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class PlayerShield : MonoBehaviour
{
    public static PlayerShield instance;
    public readonly double maxShield = 50;
    [NonSerialized]public double currentShield;
    public Slider sliderShield;
    

    private void Start()
    {
        instance = this;
        currentShield = maxShield;
        sliderShield.value = (float)(currentShield / maxShield);
    }

    private void Update()
    {
        if(currentShield <= 0)
        {
            currentShield = 0;
        }

        
        sliderShield.value = (float)(currentShield / maxShield);
        if(sliderShield.value <= 0)
        {
            sliderShield.fillRect.gameObject.SetActive(false);
        }

        if (sliderShield.value > 0)
        {
            sliderShield.fillRect.gameObject.SetActive(true);
        }

    }

    public void DamageShield()
    {
        currentShield -= 7.5;
    }
}
