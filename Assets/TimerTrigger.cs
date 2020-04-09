using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour {

    public float MaxTime;
    public static float _MaxTime;
    float CurrentTime;
    GameObject Player;
    bool TimeOn;

    // Use this for initialization
    void Start ()
    {
        TimeOn = false;
        _MaxTime = MaxTime;
	}
	
	// Update is called once per frame
	void Update ()
    {
        CurrentTime -= Time.deltaTime;
        if(CurrentTime < 0 && TimeOn == true)
        {
            Player.GetComponent<TileMove>().MazeReload();
            CurrentTime = MaxTime + 1;
            Player.GetComponent<TileMove>().Static.Play();
            Player.GetComponent<TileMove>().InvokeRepeating("MazeScreenSpook", MaxTime / 3, 1 / (2 / 3 * MaxTime));
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Timer();
            Player = collision.gameObject;
            TimeOn = true;
            Player.GetComponent<TileMove>().Static.Play();
            Player.GetComponent<TileMove>().Static.volume = 0;
            Player.GetComponent<TileMove>().InvokeRepeating("MazeScreenSpook", MaxTime / 3, 1 / (2 / 3 * MaxTime));
        }
    }

    void Timer ()
    {
        CurrentTime = MaxTime;
    }

}
