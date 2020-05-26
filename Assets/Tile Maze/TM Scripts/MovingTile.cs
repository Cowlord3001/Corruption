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

    public bool IsSeeking;
    GameObject Player;

    GameObject CurrentTile;
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
            if(IsSeeking == true)
            {
                Player = GameObject.Find("Player");
            }
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
            if (IsSeeking != true)
            {
                StartPos = transform.position;
                if ((StartPos - (Vector2)Waypoints[CurrentWaypoint].transform.position).magnitude <= .2)
                {
                    CurrentWaypoint++;
                    CurrentWaypoint = CurrentWaypoint % Waypoints.Length;
                }
                TargetPos = StartPos + ((Vector2)Waypoints[CurrentWaypoint].transform.position - StartPos).normalized;
                //Debug.Log("Step Towards " + TargetPos);
                Moving = true;

                if(CurrentTile != null)
                {
                    CurrentTile.transform.position += Vector3.back;
                }
                RaycastHit2D Hit = Physics2D.Raycast(StartPos + (TargetPos - StartPos) * .5f, TargetPos - StartPos, 1);
                CurrentTile = Hit.collider.gameObject;
                CurrentTile.transform.position += Vector3.forward;
            }
            else
            {
                StartPos = transform.position;
                int PlayX = Mathf.RoundToInt(Player.transform.position.x - transform.position.x);
                int PlayY = Mathf.RoundToInt(Player.transform.position.y - transform.position.y);

                TargetPos = (new Vector2(PlayX, 0)).normalized;
                RaycastHit2D HitX = Physics2D.Raycast(StartPos + (TargetPos) * .5f, TargetPos, 1);

                TargetPos = (new Vector2(0, PlayY)).normalized;
                RaycastHit2D HitY = Physics2D.Raycast(StartPos + (TargetPos) * .5f, TargetPos, 1);

                int rand = Random.Range(0, 2);

                if ((rand == 0 && PlayX != 0) || (rand == 1 && PlayX != 0 && PlayY == 0))
                {
                    if (HitX.collider.tag == "Red" || HitX.collider.tag == "Blue")
                    {
                        if (HitY.collider.tag == "Red" || HitY.collider.tag == "Blue")
                        {
                            //Stuck
                        }
                        else
                        {
                            if (HitY.collider.transform.position.z < 1f)
                            {
                                TargetPos = StartPos + (new Vector2(0, PlayY)).normalized;
                                Moving = true;
                            }
                            else
                            {
                                //Can't Move
                            }
                        }
                    }
                    else
                    {
                        if (HitX.collider.transform.position.z < 1f)
                        {
                            TargetPos = StartPos + (new Vector2(PlayX, 0)).normalized;
                            Moving = true;
                        }
                        else
                        {
                            //Can't Move
                        }
                    }
                }
                else if (PlayY != 0)
                {
                    if (HitY.collider.tag == "Red" || HitY.collider.tag == "Blue")
                    {
                        if (HitX.collider.tag == "Red" || HitX.collider.tag == "Blue")
                        {
                            //Stuck
                        }
                        else
                        {
                            if (HitX.collider.transform.position.z < 1f)
                            {
                                TargetPos = StartPos + (new Vector2(PlayX, 0)).normalized;
                                Moving = true;
                            }
                            else
                            {
                                //Can't Move
                            }
                        }
                    }
                    else
                    {
                        if (HitY.collider.transform.position.z < 1f)
                        {
                            TargetPos = StartPos + (new Vector2(0, PlayY)).normalized;
                            Moving = true;
                        }
                        else
                        {
                            //Can't Move
                        }
                    }
                }
                else
                {
                    //Hit Player / Stuck
                }

                if (Moving == true)
                {
                    if (CurrentTile != null)
                    {
                        CurrentTile.transform.position += Vector3.back;
                    }
                    RaycastHit2D Hit = Physics2D.Raycast(StartPos + (TargetPos - StartPos) * .5f, TargetPos - StartPos, 1);
                    CurrentTile = Hit.collider.gameObject;
                    CurrentTile.transform.position += Vector3.forward;
                }
            }
        }
    }

    void Reload()
    {
        if(OldMove != true)
        {
            if (IsSeeking != true)
            {
                transform.position = Waypoints[Waypoints.Length - 1].transform.position;
                CurrentWaypoint = 0;
                StartPos = Vector2.zero;
                TargetPos = Vector2.zero;
                Moving = false;
                T = 0;
                //Debug.Log(transform.position);

                CurrentTile.transform.position += Vector3.back;
                CurrentTile = null;
                //WaitReload = false;
            }
            else
            {
                transform.position = Waypoints[0].transform.position;
                StartPos = Vector2.zero;
                TargetPos = Vector2.zero;
                Moving = false;
                T = 0;
            }
        }
    }

    //void Freeze()
    //{
    //    Moving = false;
    //}
}
