using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Control : MonoBehaviour {

    int ButtonMem;
    public GameObject[] Buttons;
    public bool NoLoop;

    public bool ButtonDown;

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void Change()
    {
        if(Buttons.Length == ButtonMem + 1) //End of Memory (Loop?)
        {
            if(NoLoop == true) //Don't Loop
            {
                if(ButtonDown == false)
                {
                    Buttons[ButtonMem - 1].SendMessage("Reload");
                }
                Buttons[ButtonMem].SendMessage("Change");
                ButtonDown = true;
            }
            else //Loop
            {
                Buttons[ButtonMem-1].SendMessage("Reload");
                Buttons[ButtonMem].SendMessage("Change");
                ButtonMem = 0;
            }
        }
        else //Not End of Memory
        {
            if(ButtonMem != 0) //Reload previous button
            {
                Buttons[ButtonMem-1].SendMessage("Reload");
            }
            else //Reload final button
            {
                Buttons[Buttons.Length-1].SendMessage("Reload");
            }
            Buttons[ButtonMem].SendMessage("Change");
            ButtonMem++;
        }
    }

    public void Reload()
    {
        foreach (var Grey in Buttons)
        {
            Grey.SendMessage("Reload");
        }
        ButtonMem = 0;
        ButtonDown = false;
    }
}
