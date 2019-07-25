using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Tech : MonoBehaviour {
    
    public GameObject[] BossTiles;
    public float TileLength;
    int BossHealth = 3;
    GameObject ATile;

	// Use this for initialization
	void Start ()
    {
        SpawnTiles();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Reflected")
        {
            BossHealth -= 1;
            SpawnTiles();
            Destroy(ATile);
        }
    }

    //Script that delets tiles
    void SpawnTiles()
    {
            int numtiles = Random.Range(3, 5);
            for (int i = 1; i <= numtiles; i++)
            {
                int tiletypes = Random.Range(1, BossTiles.Length);
                Instantiate(BossTiles[tiletypes], transform.position - (TileLength + 5) * i * Vector3.right + Vector3.forward * 3, Quaternion.identity);
            }
        ATile = Instantiate(BossTiles[0], transform.position - (TileLength + 5) * (numtiles + 1) * Vector3.right + Vector3.forward * 3, Quaternion.identity);
    }
}
