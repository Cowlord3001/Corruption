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

	// Use this for initialization
	void Start ()
    {
        float FinalDeaths = (110 - GameData.TotalDeaths);
        float FinalTime = (221 - (GameData.TotalTime / 110));
        float FinalSkips = (5 - GameData.Skips);

        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            ToDeaths.text = "Total Deaths: " + GameData.TotalDeaths + " = " + (110 - GameData.TotalDeaths) + " / 110";
            ToTime.text = "Total Time: " + Mathf.Floor(GameData.TotalTime / 60) + ":" + Mathf.Floor(GameData.TotalTime - Mathf.Floor(GameData.TotalTime / 60) * 60) + " = " + (221 - (GameData.TotalTime / 110)) + " / 220";
            ToSkips.text = "Total Skips: " + GameData.Skips + " = " + (5 - GameData.Skips) + " / 5";
            ToScore.text = "Final Score: " + (FinalTime + FinalDeaths + FinalSkips) + " / 335";
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
            if (GameData.LevelDeaths >= SkipAmount && Skip.activeInHierarchy == false)
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
