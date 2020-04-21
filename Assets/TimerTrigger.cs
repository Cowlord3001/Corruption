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
            //Debug.Log("Other Trigger Start");
            Player.GetComponent<TileMove>().MazeReload();
            CurrentTime = MaxTime + 1;
            Player.GetComponent<TileMove>().LongStatic.Play();
            Player.GetComponent<TileMove>().InvokeRepeating("MazeScreenSpook", MaxTime / 3, 1 / (2f / 3f * MaxTime));
            //Debug.Log(1 / (2f / 3f * MaxTime));
            //Debug.Log(MaxTime);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Initial Trigger Start");
        if (collision.tag == "Player")
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Timer();
            Player = collision.gameObject;
            TimeOn = true;
            Player.GetComponent<TileMove>().LongStatic.Play();
            Player.GetComponent<TileMove>().LongStatic.volume = 0;
            Player.GetComponent<TileMove>().InvokeRepeating("MazeScreenSpook", MaxTime / 3, 1 / (2f / 3f * MaxTime));
        }
    }

    void Timer ()
    {
        CurrentTime = MaxTime;
    }

}
