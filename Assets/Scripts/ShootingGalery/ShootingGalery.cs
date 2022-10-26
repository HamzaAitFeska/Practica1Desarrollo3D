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
    public int ScoreObjective;
    public GameObject Congratulations;
    public GameObject YouLose;
    public GameObject TryAgain;
    //public Animation animation1;
    //public Animation animation2;
    public bool RoundHasStarted;
    public static ShootingGalery instance;
    public float time;
    public TMP_Text timetext;
    bool playerIsInTrigger;
    bool HasApperead;
    bool HasLeave;
    public TMP_Text textScore;

    void Start()
    {
        instance = this;
        HasApperead = false;
        HasLeave = false;
        playerIsInTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsInTrigger)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                StartGalery.SetActive(false);
                ScoreANDTime.SetActive(true);
                //animation1.Play();
                //animation2.Play();
                RoundHasStarted = true;
                HasApperead = true;
                time = 45;
                YouLose.SetActive(false);
                TryAgain.SetActive(false);
                HasLeave = false;
                FPSPlayerController.instance.m_TotalPoints = 0;
            }
        }
        if (RoundHasStarted)
        {
            time -= 2f * Time.deltaTime;
        }

        time = (float)System.Math.Round(time, 2);
        timetext.text = time.ToString();

        if(time <= 0)
        {
            time = 0;
            RoundHasStarted = false;
            //animation1.Stop();
            //animation2.Stop();

        }

        if(!RoundHasStarted && FPSPlayerController.instance.m_TotalPoints >= ScoreObjective && !HasLeave)
        {
            YouWon.SetActive(true);
            Congratulations.SetActive(true);
        }

        if (time <= 0 && FPSPlayerController.instance.m_TotalPoints < ScoreObjective)
        {
            YouLose.SetActive(true);
            TryAgain.SetActive(true);
        }

        if (!RoundHasStarted && FPSPlayerController.instance.m_TotalPoints >= ScoreObjective && HasLeave)
        {
            YouWon.SetActive(false);
            Congratulations.SetActive(false);
            ScoreANDTime.SetActive(false);
        }

        if(FPSPlayerController.instance.m_TotalPoints >= ScoreObjective)
        {
            time = 0;
        }

        textScore.text = FPSPlayerController.instance.m_TotalPoints.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartGalery.SetActive(true);
            playerIsInTrigger = true;
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
            playerIsInTrigger = false;
            StartGalery.SetActive(false);
        }
    }






}
