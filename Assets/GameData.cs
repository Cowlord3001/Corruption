using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {

    public static int TotalDeaths;
    public static int LevelDeaths;
    public static float TotalTime;
    public static float LevelTime;
    public static int Skips;

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
