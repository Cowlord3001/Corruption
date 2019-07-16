using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//New Script: Causes camera to pan out to double its distance in Stage 3
public class CameraPan : MonoBehaviour {
	
	// Update is called once per frame
	void Update ()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3 && Camera.main.orthographicSize <= 10)
        {
            CameraScale();
        }
    }

    void CameraScale()
    {
        Camera.main.orthographicSize += Time.deltaTime;
    }
}
