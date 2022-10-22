using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameoverr;
    void Start()
    {
        gameoverr.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOverr()
    {
        SceneManager.LoadScene(0);
    }
}
