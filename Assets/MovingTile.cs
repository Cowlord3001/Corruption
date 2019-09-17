using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTile : MonoBehaviour {

    public GameObject[] Waypoints;
    public float Speed;
    //Acceleration Bool?
    bool Moving;
    float T;
    Vector2 StartPos;
    Vector2 TargetPos;

    bool Loop;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Moving == false)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) == true || Input.GetKeyDown(KeyCode.W) == true)
            {
                Moving = true;
                TargetPos = (Vector2)transform.position + Vector2.up;
                StartPos = transform.position;
            }
            
        }
        else if (Moving == true)
        {
            T += Speed * Time.deltaTime;
            transform.position = Vector2.Lerp(StartPos, TargetPos, T);
            if (T > 1)
            {
                //StopMovement(); (Sets Moving to false)
                T = 0;
            }
        }
    }
}
