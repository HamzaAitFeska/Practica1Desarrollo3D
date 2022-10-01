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
    void Start()
    {
        currentAmmo = maxAmmo;
        textAmmo.text = currentAmmo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoseAmmo()
    {
        currentAmmo--;
    }
    private void ResetAmmo()
    {
        currentAmmo = maxAmmo;
    }
}
