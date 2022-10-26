using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinObject : MonoBehaviour
{
    public GameObject WinScreen;
    public GameObject UI;
    public GameObject OverlayBlood;
    private void Start()
    {
        WinScreen.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FPSPlayerController.instance.m_Shooting = true;
            AudioController.instance.PlayOneShot(AudioController.instance.KeyPickup);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            FPSPlayerController.instance.m_AngleLocked = true;
            FPSPlayerController.instance.m_characterController.enabled = false;
            WinScreen.SetActive(true);
            UI.SetActive(false);
            OverlayBlood.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

   public void Quit()
   {
        Application.Quit();
   }
        
}
