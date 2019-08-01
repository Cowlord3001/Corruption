using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChange : MonoBehaviour {

    public Color[] Colors;
    Image MyImage;
    int i;

    // Use this for initialization
    void Start ()
    {
        i = 0;
        MyImage = gameObject.GetComponent<Image>();
        InvokeRepeating("NotUpdate", 0, 1);
    }
	
	// Update is called once per frame
	void NotUpdate ()
    {
        if (i == Colors.Length)
        {
            i = 0;
        }
        MyImage.color = Colors[i];
        i++;
      
    }
}
