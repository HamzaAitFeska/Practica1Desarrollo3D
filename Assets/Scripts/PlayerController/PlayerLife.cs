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
    public bool m_IsCreated;
    void Start()
    {
        instance = this;
        currentLife = maxLife;
        healthamount.text = currentLife.ToString();
        m_IsCreated = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(damagePlayer))
        {
           DamagePlayer();
        }

        if(currentLife <= 0)
        {
            currentLife = 0;
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
        //Destroy(gameObject, 1f);
        yield return new WaitForSeconds(1f);
        if (!m_IsCreated)
        {
           Instantiate(Player, Checkpoint, Quaternion.identity);
            m_IsCreated = true;
        }
        Destroy(gameObject, 1f);

    }
}
