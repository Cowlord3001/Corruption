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
    //public static bool WaitReload;

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
        if(OldMove != true /*&& WaitReload == false*/)
        {
            StartPos = transform.position;
            if ((StartPos - (Vector2) Waypoints[CurrentWaypoint].transform.position).magnitude <= .2)
            {
                CurrentWaypoint++;
                CurrentWaypoint = CurrentWaypoint % Waypoints.Length;
            }
            TargetPos = StartPos + ((Vector2)Waypoints[CurrentWaypoint].transform.position - StartPos).normalized;
            Debug.Log("Step Towards " + TargetPos);
            Moving = true;
        }
    }

    void Reload()
    {
        if(OldMove != true)
        {
            transform.position = Waypoints[Waypoints.Length - 1].transform.position;
            CurrentWaypoint = 0;
            StartPos = Vector2.zero;
            TargetPos = Vector2.zero;
            Moving = false;
            T = 0;
            Debug.Log(transform.position);

            //WaitReload = false;
        }
    }

    void Freeze()
    {
        Moving = false;
    }
}
