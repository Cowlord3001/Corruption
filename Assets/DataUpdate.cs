using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DataUpdate : MonoBehaviour {

    public Text Deaths;
    public Text tTime;
    public GameObject Skip;
    public int SkipAmount;

	// Use this for initialization
	void Start ()
    {
        Skip.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        Deaths.text = "Deaths: " + GameData.LevelDeaths;
        tTime.text = "Time: " + Mathf.Floor(GameData.LevelTime / 60) + ":" + Mathf.Floor(GameData.LevelTime - Mathf.Floor(GameData.LevelTime / 60)*60);
        if(GameData.LevelDeaths >= SkipAmount && Skip.activeInHierarchy == false)
        {
            Skip.SetActive(true);
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

    public void SkipButton()
    {
        GetComponent<AudioSource>().Play();
        Invoke("LoadScene", .2f);
    }
}
