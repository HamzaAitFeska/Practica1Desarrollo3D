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
    public Quaternion StartRotation;
    public GameObject Player;
    public bool m_IsCreated;
    public Slider sliderlifebar;
    [Header("GameOver")]
    public GameObject GameOver;
    public GameObject UI;
    public GameObject OverlayBlood;
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
        transform.rotation = StartRotation;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(damagePlayer))
        {
            //DamagePlayer();
            currentLife = 0;
        }

        if(currentLife <= 0)
        {
            currentLife = 0;
            GameOver.SetActive(true);
            FPSPlayerController.instance.m_AngleLocked = true;
            Overlay.color = new Color(Overlay.color.r, Overlay.color.g, Overlay.color.b, 0f);
            //FPSPlayerController.instance.m_AimLocked = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            FPSPlayerController.instance.m_characterController.enabled = false;
            UI.SetActive(false);
            OverlayBlood.SetActive(false);
            //Death();
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
            Overlay.color = new Color(255, 255, 255, 0.5f);

        }
        
    }

    public void Death()
    {
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        //Destroy(gameObject, 1f);
        yield return new WaitForSeconds(1f);
        
            transform.position = Checkpoint;
            transform.rotation = StartRotation;
            currentLife = maxLife;
            PlayerShield.instance.currentShield = PlayerShield.instance.maxShield;
            PlayerAmmo.instance.currentAmmo = PlayerAmmo.instance.maxAmmo;
            PlayerAmmo.instance.currentmagSize = PlayerAmmo.instance.maxMagSize;
            FPSPlayerController.instance.m_characterController.enabled = true;
            FPSPlayerController.instance.m_AngleLocked = false;
            UI.SetActive(true);
            GameOver.SetActive(false);
            OverlayBlood.SetActive(true);
            //Instantiate(Player, Checkpoint, Quaternion.identity);
            m_IsCreated = true;
        

        //Destroy(gameObject, 1f);

    }
}
