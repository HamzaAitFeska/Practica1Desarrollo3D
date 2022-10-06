using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    private void Start()
    {
        GameController.DestroySingleton();
    }
    public void OnstartClicked()
    {
        SceneManager.LoadScene(1);
    }
}
