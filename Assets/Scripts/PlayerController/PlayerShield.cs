using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerShield : MonoBehaviour
{
    public static PlayerShield instance;
    private readonly double maxShield = 50;
    [NonSerialized]public double currentShield;
    public TMP_Text Shieldamount;
    

    private void Start()
    {
        instance = this;
        currentShield = maxShield;
        Shieldamount.text = currentShield.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            DamageShield();
        }
        
        //percent = (currentShield / maxShield) * 100;
        Shieldamount.text = currentShield.ToString();
    }

    private void DamageShield()
    {
        currentShield -= 7.5;
    }
}
