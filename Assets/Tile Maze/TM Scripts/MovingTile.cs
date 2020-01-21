using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTile : MonoBehaviour {

    public GameObject[] Waypoints;
    int W;
    public float Speed;
    //Acceleration Bool?
    bool Moving;
    float T;
    Vector2 StartPos;
    Vector2 TargetPos;
    float d;

	// Use this for initialization
	void Start ()
    {
        if (Waypoints.Length == 0)
        {

        }
        else
        {
            W = 0;
            transform.position = Waypoints[Waypoints.Length - 1].transform.position;
            StartPos = Waypoints[Waypoints.Length - 1].transform.position;
            TargetPos = Waypoints[W].transform.position;

            d = (TargetPos - StartPos).magnitude;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (Waypoints.Length == 0)
        {

        }
        else
        {
            T += (Speed / d) * Time.deltaTime;
            transform.position = Vector2.Lerp(StartPos, TargetPos, T);
            if (T > 1)
            {
                //StopMovement(); (Sets Moving to false)
                T = 0;
                transform.position = Waypoints[W].transform.position;
                StartPos = Waypoints[W].transform.position;
                W++;

                if (W == Waypoints.Length)
                {
                    W = 0;
                }

                TargetPos = Waypoints[W].transform.position;
                d = (TargetPos - StartPos).magnitude;

            }
        }
        
    }
}
