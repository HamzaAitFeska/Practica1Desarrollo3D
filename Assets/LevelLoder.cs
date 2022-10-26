using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoder : MonoBehaviour
{
    // Start is called before the first frame update
    public static LevelLoder instance;
    public Animator transition;
    void Start()
    {
       instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextlevel(int indexlevel)
    {
        StartCoroutine(Loadlevel(indexlevel));
    }

    

    public IEnumerator Loadlevel(int indexlevel)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(indexlevel);
    }
}
