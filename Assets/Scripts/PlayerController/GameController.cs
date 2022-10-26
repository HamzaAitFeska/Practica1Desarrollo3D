using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    static GameController m_GameController = null;
    float m_PlayerLife = 1.0f;
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

    }

    public static GameController GetGameController()
    {
        if(m_GameController == null)
        {
            m_GameController = new GameObject("gamecontroller").AddComponent<GameController>();
            //GameControllerData gameControllerData = Resources.Load(GameControllerData("GameControllerDtat"));
        }
        return m_GameController;
    }

    public static void DestroySingleton()
    {
        if (m_GameController != null)
        {
            GameObject.Destroy(m_GameController.gameObject);
        }
        m_GameController = null;
    }

    public void SetPlayerLife(float PlayerLife)
    {
        m_PlayerLife = PlayerLife;
    }

    public float GetPlayerLife()
    {
        return m_PlayerLife;
    }
    public void NextLevel()
    {
        LevelLoder.instance.LoadNextlevel(0);
    }
}
