using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerLife instance;
    private readonly int maxLife = 100;
    [NonSerialized]public float currentLife;
    
    public KeyCode damagePlayer;
    public Vector3 Checkpoint;
    public GameObject Player;
    public bool m_IsCreated;
    public Slider sliderlifebar;
    [Header("DamageOverlay")]
    public Image Overlay;
    public float duration;
    public float FadeSpeed;

    private float durationTimer;
    void Start()
    {
        instance = this;
        currentLife = maxLife;
        sliderlifebar.value = currentLife / maxLife;
        m_IsCreated = false;
        Overlay.color = new Color(Overlay.color.r, Overlay.color.g, Overlay.color.b, 0f);
    }

    // Update is called once per frame
    void Update()
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

        sliderlifebar.value = currentLife / maxLife;
        if (sliderlifebar.value <= 0)
        {
            sliderlifebar.fillRect.gameObject.SetActive(false);
        }

        if (sliderlifebar.value > 0)
        {
            sliderlifebar.fillRect.gameObject.SetActive(true);
        }

        if(Overlay.color.a > 0)
        {

            if (currentLife < 40)
                return;
            durationTimer += Time.deltaTime;
            if(durationTimer > duration)
            {
                float tempAlpha = Overlay.color.a;
                tempAlpha -= Time.deltaTime * FadeSpeed;
                Overlay.color = new Color(Overlay.color.r, Overlay.color.g, Overlay.color.b, tempAlpha);
            }
        }
    }

    public void DamagePlayer()
    {
        if(PlayerShield.instance.currentShield > 0)
        {
           currentLife -= 0.25f;
           PlayerShield.instance.DamageShield();
        }
        
        if(PlayerShield.instance.currentShield <= 0)
        {
            currentLife--;
        }

        durationTimer = 0;
        if(currentLife < 40)
        {
            Overlay.color = new Color(Overlay.color.r, Overlay.color.g, Overlay.color.b, 1f);
        }
        else
        {
            Overlay.color = new Color(Overlay.color.r, Overlay.color.g, Overlay.color.b, 0.5f);

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
