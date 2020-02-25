using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DataUpdate : MonoBehaviour {

    public Text Deaths;
    public Text tTime;
    public Text ToDeaths;
    public Text ToTime;
    public Text ToSkips;
    public Text ToScore;

    public GameObject Skip;
    public GameObject SkipFinal;
    public int SkipAmount;
    public int SkipTime;

	// Use this for initialization
	void Start ()
    {
        //GameData.TotalDeaths = 0;
        //GameData.TotalTime = 220;
        //GameData.Skips = 0;

        float FinalDeaths = 0;
        if(GameData.TotalDeaths >= 315)
        {
            FinalDeaths = 0;
        }
        else
        {
            FinalDeaths = (105 - Mathf.Floor(GameData.TotalDeaths / 3));
        }

        float FinalTime = 0;
        if (GameData.TotalTime < 1800)
        {
            FinalTime = Mathf.Floor(800 - (GameData.TotalTime / 4));
        }
        else
        {
            FinalTime = Mathf.Floor(2117.376f * Mathf.Exp(-.001f * GameData.TotalTime));
        } 

        float FinalSkips = (5 - GameData.Skips) * 150;

        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            ToDeaths.text = "Total Deaths: " + GameData.TotalDeaths + " = " + FinalDeaths + " / 105"; 
            ToTime.text = "Total Time: " + Mathf.Floor(GameData.TotalTime / 60) + ":" + Mathf.Floor(GameData.TotalTime - Mathf.Floor(GameData.TotalTime / 60) * 60) + " = " + FinalTime + " / 745";
            ToSkips.text = "Total Skips: " + GameData.Skips + " = " + FinalSkips + " / 750"; 
            ToScore.text = "Final Score: " + (FinalTime + FinalDeaths + FinalSkips) + " / 1600";
        }
        else
        {
            Skip.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (SceneManager.GetActiveScene().buildIndex == 6)
        {

        }
        else
        {
            Deaths.text = "Deaths: " + GameData.LevelDeaths;
            tTime.text = "Time: " + Mathf.Floor(GameData.LevelTime / 60) + ":" + Mathf.Floor(GameData.LevelTime - Mathf.Floor(GameData.LevelTime / 60) * 60);
            if (GameData.LevelDeaths >= SkipAmount && GameData.LevelTime >= SkipTime && Skip.activeInHierarchy == false)
            {
                Skip.SetActive(true);
            }
        }
    }

    void LoadScene()
    {
        Debug.Log("Button");
        GameData.Skips++;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameData.LevelDeaths = 0;
        GameData.LevelTime = 0;
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            Persist.Song = false;
            Destroy(GameObject.Find("Music"));
        }
    }

    public void SkipStart()
    {
        GetComponent<AudioSource>().Play();
        SkipFinal.SetActive(true);
    }

    public void SkipButton()
    {
        GetComponent<AudioSource>().Play();
        Invoke("LoadScene", .2f);
    }
}
