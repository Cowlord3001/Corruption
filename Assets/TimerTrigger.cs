using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour {

    public float MaxTime;
    float CurrentTime;
    GameObject Coll;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        CurrentTime -= Time.deltaTime;
        if(CurrentTime < 0)
        {
            //Coll...
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Timer();
            Coll = collision.gameObject;
        }
    }

    void Timer ()
    {
        CurrentTime = MaxTime;
    }
}
