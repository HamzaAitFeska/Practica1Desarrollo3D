using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAmmo : MonoBehaviour
{
    // Start is called before the first frame update
    private int maxAmmo = 100;
    private int currentAmmo;
    public TMP_Text textAmmo;
    public static PlayerAmmo instance;
    void Start()
    {
        currentAmmo = maxAmmo;
        textAmmo.text = currentAmmo.ToString();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        textAmmo.text = currentAmmo.ToString();
        if (Input.GetKeyDown(KeyCode.L))
        {

            ResetAmmo();
        }
    }

    public void LoseAmmo()
    {
        currentAmmo--;
    }
    private void ResetAmmo()
    {
        currentAmmo = maxAmmo;
    }

    void SetReloadAnimation()
    {

    }
}
