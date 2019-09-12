using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTile : MonoBehaviour {

    public bool Reversable;
    GameObject[] TileMem;

    public GameObject[] Tile;
    public GameObject Type;


	// Use this for initialization
	void Start ()
    {
        TileMem = new GameObject[Tile.Length];
        for (int i = 0; i < Tile.Length; i++)
        {
            TileMem[i] = TileMem[i];
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void Change()
    {
        foreach (GameObject item in Tile)
        {
            item.tag = Type.tag;
            item.GetComponent<SpriteRenderer>().color = Type.GetComponent<SpriteRenderer>().color;
        }
    }
}
