using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTile : MonoBehaviour {

    public GameObject[] Waypoints; //Good
    int CurrentWaypoint;
    public float Speed;

    float d; //Old

    float T; //Lerp
    bool Moving;
    Vector2 StartPos;
    Vector2 TargetPos;

    public bool OldMove;

	// Use this for initialization
	void Start ()
    {
        if (OldMove == true)
        {
            if (Waypoints.Length == 0)
            {

            }
            else
            {
                CurrentWaypoint = 0;
                transform.position = Waypoints[Waypoints.Length - 1].transform.position;
                StartPos = Waypoints[Waypoints.Length - 1].transform.position;
                TargetPos = Waypoints[CurrentWaypoint].transform.position;

                d = (TargetPos - StartPos).magnitude;
            }
        }
        else
        {

        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (OldMove == true)
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
                    transform.position = Waypoints[CurrentWaypoint].transform.position;
                    StartPos = Waypoints[CurrentWaypoint].transform.position;
                    CurrentWaypoint++;

                    if (CurrentWaypoint == Waypoints.Length)
                    {
                        CurrentWaypoint = 0;
                    }

                    TargetPos = Waypoints[CurrentWaypoint].transform.position;
                    d = (TargetPos - StartPos).magnitude;

                }
            }
        }
        else
        {
            if(Moving == true)
            {
                T += Speed * Time.deltaTime;
                transform.position = Vector2.Lerp(StartPos, TargetPos, T);
                if (T >= 1)
                {
                    transform.position = TargetPos;
                    Moving = false;
                    T = 0;
                }
            }
        }
    }

    void Step()
    {
        if(OldMove != true)
        {
            if ((StartPos - (Vector2) Waypoints[CurrentWaypoint].transform.position).magnitude <= .5)
            {
                CurrentWaypoint++;
                CurrentWaypoint = CurrentWaypoint % Waypoints.Length;
            }
            StartPos = transform.position;
            TargetPos = StartPos + ((Vector2)Waypoints[CurrentWaypoint].transform.position - StartPos).normalized;
            Moving = true;
        }
    }
}
