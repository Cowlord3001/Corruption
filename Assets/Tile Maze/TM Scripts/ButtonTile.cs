using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTile : MonoBehaviour {

    public bool Reversable;
    public bool MultTypes;
    Color[] ColorMem;
    string[] TagMem;
    //public GameObject EndTile;

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
                    if (Types[i].tag == "End") //Set Tile to EndTile
                    {
                        Tile[i].SetActive(false);
                        Types[i].transform.position = Tile[i].transform.position;
                        Types[i].SetActive(true);
                    }
                    else if(Tile[i].tag == "End") //Set EndTile to Tile
                    {
                        Tile[i].SetActive(false);
                        Types[i].transform.position = Tile[i].transform.position;
                        Types[i].SetActive(true);
                    }
                    else
                    {
                        Tile[i].tag = Types[i].tag;
                        Tile[i].GetComponent<SpriteRenderer>().color = Types[i].GetComponent<SpriteRenderer>().color;
                    }
                }
            }
            else
            {
                if (Type.tag == "End") //Set Tile to EndTile
                {
                    Tile[0].SetActive(false);
                    Type.transform.position = Tile[0].transform.position;
                    Type.SetActive(true);
                }
                else if (Tile[0].tag == "End") //Set EndTile to Tile
                {
                    Tile[0].SetActive(false);
                    Type.transform.position = Tile[0].transform.position;
                    Type.SetActive(true);
                }
                else
                {
                    foreach (GameObject item in Tile)
                    {
                        item.tag = Type.tag;
                        item.GetComponent<SpriteRenderer>().color = Type.GetComponent<SpriteRenderer>().color;
                    }
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
                    if ((MultTypes == true && Types[i].tag == "End") || (MultTypes == false && Type.tag == "End")) //Set Tile to EndTile
                    {
                        Tile[i].SetActive(true);
                        if (MultTypes == true)
                        {
                            Types[i].SetActive(false);
                        }
                        else
                        {
                            Type.SetActive(false);
                        }
                    }
                    else if ((MultTypes == true && Tile[i].tag == "End") || (MultTypes == false && Tile[0].tag == "End")) //Set EndTile to Tile
                    {
                        Tile[i].SetActive(true);
                        if (MultTypes == true)
                        {
                            Types[i].SetActive(false);
                        }
                        else
                        {
                            Type.SetActive(false);
                        }
                    }
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
            if ((MultTypes == true && Types[i].tag == "End") || (MultTypes == false && Type.tag == "End")) //Set Tile to EndTile
            {
                Tile[i].SetActive(true);
                if (MultTypes == true)
                {
                    Types[i].SetActive(false);
                }
                else
                {
                    Type.SetActive(false);
                }
            }
            else if ((MultTypes == true && Tile[i].tag == "End") || (MultTypes == false && Tile[0].tag == "End")) //Set EndTile to Tile
            {
                Tile[i].SetActive(true);
                if (MultTypes == true)
                {
                    Types[i].SetActive(false);
                }
                else
                {
                    Type.SetActive(false);
                }
            }
            Tile[i].tag = TagMem[i];
            Tile[i].GetComponent<SpriteRenderer>().color = ColorMem[i];
        }
        ButtonDown = false;
    }
}
