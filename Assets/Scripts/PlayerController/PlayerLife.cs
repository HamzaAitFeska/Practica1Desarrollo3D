using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerLife : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerLife instance;
    private readonly int maxLife = 100;
    [NonSerialized]public int currentLife;
    public TMP_Text healthamount;
    public KeyCode damagePlayer;
    void Start()
    {
        instance = this;
        currentLife = maxLife;
        healthamount.text = currentLife.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(damagePlayer))
        {
           DamagePlayer();
        }

        healthamount.text = currentLife.ToString();
    }

    private void DamagePlayer()
    {
        if(PlayerShield.instance.currentShield > 0)
        {
           currentLife = ((int)(currentLife - 2.5));
        }
        else
        {
            currentLife-=10;
        }
    }
}
