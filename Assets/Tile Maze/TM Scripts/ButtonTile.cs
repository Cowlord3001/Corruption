using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTile : MonoBehaviour {

    public bool Reversable;
    public bool MultTypes;
    Color[] ColorMem;
    string[] TagMem;

    public GameObject[] Tile;
    public GameObject Type;
    public GameObject[] Types;

    public bool ButtonDown;


	// Use this for initialization
	void Start ()
    {
        TagMem = new string[Tile.Length];
        for (int i = 0; i < Tile.Length; i++)
        {
            TagMem[i] = Tile[i].tag;
        }

        ColorMem = new Color[Tile.Length];
        for (int i = 0; i < Tile.Length; i++)
        {
            ColorMem[i] = Tile[i].GetComponent<SpriteRenderer>().color;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void Change()
    {
        if (ButtonDown == false)
        {
            if (MultTypes == true)
            {
                for (int i = 0; i < Types.Length; i++)
                {
                    Tile[i].tag = Types[i].tag;
                    Tile[i].GetComponent<SpriteRenderer>().color = Types[i].GetComponent<SpriteRenderer>().color;
                }
            }
            else
            {
                foreach (GameObject item in Tile)
                {
                    item.tag = Type.tag;
                    item.GetComponent<SpriteRenderer>().color = Type.GetComponent<SpriteRenderer>().color;
                }
            }
            ButtonDown = true;
        }

        else
        {
            if(Reversable == true)
            {
                for (int i = 0; i < Tile.Length; i++)
                {
                    Tile[i].tag = TagMem[i];
                    Tile[i].GetComponent<SpriteRenderer>().color = ColorMem[i];
                }
                ButtonDown = false;
            }
        }
    }

    public void Reload()
    {
        for (int i = 0; i < Tile.Length; i++)
        {
            Tile[i].tag = TagMem[i];
            Tile[i].GetComponent<SpriteRenderer>().color = ColorMem[i];
        }
        ButtonDown = false;
    }
}
