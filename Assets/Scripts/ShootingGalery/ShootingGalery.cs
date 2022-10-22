using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShootingGalery : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject StartGalery;
    public GameObject ScoreANDTime;
    public GameObject YouWon;
    public GameObject Congratulations;
    public GameObject YouLose;
    public GameObject TryAgain;
    public Animation animation1;
    public Animation animation2;
    public bool StartAnims;
    public static ShootingGalery instance;
    public float time;
    public TMP_Text timetext;
    bool HasApperead;
    bool HasLeave;
    
    void Start()
    {
        instance = this;
        HasApperead = false;
        HasLeave = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            StartGalery.SetActive(false);
            ScoreANDTime.SetActive(true);
            animation1.Play();
            animation2.Play();
            StartAnims = true;
            HasApperead = true;
            time = 45;
            YouLose.SetActive(false);
            TryAgain.SetActive(false);
            HasLeave = false;
            FPSPlayerController.instance.m_TotalPoints = 0;
        }
        if (StartAnims)
        {
            time -= 2f * Time.deltaTime;
        }

        time = (float)System.Math.Round(time, 2);
        timetext.text = time.ToString();

        if(time <= 0)
        {
            time = 0;
            StartAnims = false;
            animation1.Stop();
            animation2.Stop();

        }

        if(!StartAnims && FPSPlayerController.instance.m_TotalPoints >= 500 && !HasLeave)
        {
            YouWon.SetActive(true);
            Congratulations.SetActive(true);
        }

        if (time <= 0 && FPSPlayerController.instance.m_TotalPoints < 500)
        {
            YouLose.SetActive(true);
            TryAgain.SetActive(true);
        }

        if (!StartAnims && FPSPlayerController.instance.m_TotalPoints >= 500 && HasLeave)
        {
            YouWon.SetActive(false);
            Congratulations.SetActive(false);
            ScoreANDTime.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartGalery.SetActive(true);
            
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && HasApperead)
        {
            StartGalery.SetActive(false);


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HasLeave = true;

        }
    }






}
