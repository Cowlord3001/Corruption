using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persist : MonoBehaviour {

    public static bool Song = false;
    bool Original = false;

	// Use this for initialization
	void Start ()
    {
        if (Song == false)
        {
            Song = true;
            Original = true;
            DontDestroyOnLoad(gameObject);
        }

        if (Original == false)
        {
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
