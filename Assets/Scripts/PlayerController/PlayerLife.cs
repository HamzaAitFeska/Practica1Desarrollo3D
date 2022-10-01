using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerLife : MonoBehaviour
{
    // Start is called before the first frame update
    private int maxLife = 100;
    private int currentLife;
    public TMP_Text healthamount;
    public KeyCode damagePlayer;
    void Start()
    {
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
        currentLife--;
    }
}
