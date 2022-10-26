﻿using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    private void Start()
    {
        GameController.DestroySingleton();
        Cursor.lockState = CursorLockMode.None;
    }
    public void OnstartClicked()
    {
        LevelLoder.instance.LoadNextlevel(1);
    }
}
