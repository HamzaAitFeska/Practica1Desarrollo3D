using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    private void Start()
    {
        GameController.DestroySingleton();
        Cursor.lockState = CursorLockMode.None;
        AudioController.instance.Play(AudioController.instance.TopGmusic);
    }
    public void OnstartClicked()
    {
        LevelLoder.instance.LoadNextlevel(1);
    }
}
