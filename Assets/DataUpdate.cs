using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataUpdate : MonoBehaviour {

    public Text Deaths;
    public Text Time;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Deaths.text = "Deaths: " + GameData.LevelDeaths;
        Time.text = "Time: " + Mathf.Round(GameData.LevelTime / 60) + ":" + Mathf.Round(GameData.LevelTime - Mathf.Round(GameData.LevelTime / 60)*6000);
	}
}
