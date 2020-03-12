using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pause : MonoBehaviour {

    static bool Paused;

	// Use this for initialization
	void Start () {
        if (Paused == true)
        {
            GetComponent<Image>().enabled = true;
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);

            Paused = true;
        }
        else if (Paused == false)
        {
            GetComponent<Image>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);

            Paused = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && Paused == false)
        {
            GetComponent<Image>().enabled = true;
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);

            Paused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Paused == true)
        {
            GetComponent<Image>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);

            Paused = false;
        }
	}

    public void Quit()
    {
        Application.Quit();
    }
}
