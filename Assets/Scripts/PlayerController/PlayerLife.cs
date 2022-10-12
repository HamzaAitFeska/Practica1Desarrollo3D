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
    public Vector3 Checkpoint;
    public GameObject Player;
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

        if(currentLife <= 0)
        {
            Death();
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

    private void Death()
    {
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        Destroy(gameObject, 1f);
        yield return new WaitForSeconds(1f);
        Instantiate(Player, Checkpoint, Quaternion.identity);

    }
}
