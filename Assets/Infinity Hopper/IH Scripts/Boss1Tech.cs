using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Tech : MonoBehaviour {
    
    public GameObject[] BossTiles;
    public float TileLength;
    static int BossHealth;
    GameObject ATile;
    public GameObject EndPortal;

    public AudioSource Damage;
    public AudioSource Death;

    // Use this for initialization
    void Start ()
    {
        SpawnTiles();
        if (BossHealth == 2 || BossHealth == 1)
        {

        }
        else
        {
            BossHealth = 2;
        }
        //Debug.Log(BossHealth);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void die()
    {
        Instantiate(EndPortal, new Vector3(transform.position.x, 0, 0), Quaternion.identity);
        Destroy(gameObject.transform.parent.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Reflected")
        {
            if (BossHealth == 1)
            {
                Destroy(collision.gameObject);
                ATile.SetActive(false);
                Rigidbody2D Mybody = gameObject.transform.parent.gameObject.AddComponent<Rigidbody2D>();
                Mybody.angularVelocity = 720;
                InvokeRepeating("DeathSounds", 0, .25f);
                Invoke("die", 5);
            }

            else
            {
                BossHealth -= 1;
                Damage.Play();
                Destroy(collision.gameObject);
                ATile.SetActive(false);
                SpawnTiles();
                Boss1Attack.GreenHit = false;
            }
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
        if (ATile == null)
        {
            ATile = Instantiate(BossTiles[0], transform.position - (TileLength + 5) * (numtiles + 1) * Vector3.right + Vector3.forward * 3, Quaternion.identity);
        }

        else
        {
            ATile.transform.position = transform.position - (TileLength + 5) * (numtiles + 1) * Vector3.right + Vector3.forward * 3;
            ATile.SetActive(true);
        }

    }

    void DeathSounds()
    {
        Death.Play();
    }
}
