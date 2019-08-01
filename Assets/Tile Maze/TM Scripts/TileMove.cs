using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMove : MonoBehaviour {

    public float MinX, MinY, MaxX, MaxY;
    public float Speed;
    bool Moving;
    bool CanMove;
    Vector2 StartPos;
    Vector2 TargetPos;
    float T;
    GameObject CurrentTile;

	// Use this for initialization
	void Start ()
    {
        CanMove = true;
	}
	


	// Update is called once per frame
	void Update ()
    {
        if(Moving == false && CanMove == true)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow) == true || Input.GetKeyDown(KeyCode.W) == true)
            {
                CanMove = false;
                Moving = true;
                TargetPos = (Vector2) transform.position + Vector2.up;
                StartPos = transform.position;
                StartMove();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) == true || Input.GetKeyDown(KeyCode.S) == true)
            {
                CanMove = false;
                Moving = true;
                TargetPos = (Vector2)transform.position + Vector2.down;
                StartPos = transform.position;
                StartMove();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) == true || Input.GetKeyDown(KeyCode.A) == true)
            {
                CanMove = false;
                Moving = true;
                TargetPos = (Vector2)transform.position + Vector2.left;
                StartPos = transform.position;
                StartMove();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) == true || Input.GetKeyDown(KeyCode.D) == true)
            {
                CanMove = false;
                Moving = true;
                TargetPos = (Vector2)transform.position + Vector2.right;
                StartPos = transform.position;
                StartMove();
            }

            //TODO: Check if Targetpos is valid (raycast?)
        }
        else if(Moving == true)
        {
            T += Speed * Time.deltaTime;
            transform.position = Vector2.Lerp(StartPos, TargetPos, T);
            if(T > 1)
            {
                StopMovement();
                T = 0;
            }
        }

	}

    void StartMove()
    {
        RaycastHit2D Hit = Physics2D.Raycast(StartPos + (TargetPos - StartPos) * .5f, TargetPos - StartPos, 1);
        Debug.Log("Tag = " + Hit.collider.tag);
        if (Hit.collider.tag == "Red")
        {
            StopMovement();
        }
    }

    void StopMovement()
    {
        Moving = false;

        transform.position = CurrentTile.transform.position;

        CanMove = true;
        //Debug.Log("Movement Complete");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Collided");
        CurrentTile = collision.gameObject;
    }

}
