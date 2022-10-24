using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoder : MonoBehaviour
{
    // Start is called before the first frame update
    public Animation m_animation;
    public AnimationClip start;
    public AnimationClip end;
    bool StartFinished;
    public static LevelLoder instance; 
    void Start()
    {
        StartFinished = false;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextlevel()
    {
        StartCoroutine(Loadlevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public IEnumerator StartAnim()
    {
        m_animation.Play(start.name);
        yield return new WaitForSeconds(start.length);
        StartFinished = true;
        
    }

    public IEnumerator Loadlevel(int indexlevel)
    {
        m_animation.Play(end.name);
        yield return new WaitForSeconds(end.length);
        SceneManager.LoadScene(indexlevel);
    }
}
