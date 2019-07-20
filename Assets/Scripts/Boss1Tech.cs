using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Tech : MonoBehaviour {

    public GameObject[] BossTiles;
    public float TileLength;

	// Use this for initialization
	void Start ()
    {
        SpawnTiles();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Script that delets tiles
    void SpawnTiles()
    {
        int numtiles = Random.Range(3, 5);
        for (int i = 1; i <= numtiles; i++)
        {
            int tiletypes = Random.Range(1, BossTiles.Length);
            Instantiate(BossTiles[tiletypes], transform.position - (TileLength+5) * i * Vector3.right + Vector3.forward*3, Quaternion.identity);
        }
        Instantiate(BossTiles[0], transform.position - (TileLength + 5) * (numtiles + 1) * Vector3.right + Vector3.forward * 3, Quaternion.identity);
    }
}
