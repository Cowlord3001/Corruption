using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileMove : MonoBehaviour {

    //public float MinX, MinY, MaxX, MaxY;
    public float Speed;
    bool Moving;
    bool CanMove;
    Vector2 StartPos;
    Vector2 TargetPos;
    float T;
    GameObject CurrentTile;

    EndTile End;
    public GameObject OldStart;
    public float CamSpeed;
    float CamDir;

    Vector2 LastMove;
    GameObject PrevTile;

    public int Weight;

    public AudioSource Move;
    public AudioSource Finish;
    public AudioSource Yellow;

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
                LastMove = Vector2.up;
                StartMove();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) == true || Input.GetKeyDown(KeyCode.S) == true)
            {
                CanMove = false;
                Moving = true;
                TargetPos = (Vector2)transform.position + Vector2.down;
                StartPos = transform.position;
                LastMove = Vector2.down;
                StartMove();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) == true || Input.GetKeyDown(KeyCode.A) == true)
            {
                CanMove = false;
                Moving = true;
                TargetPos = (Vector2)transform.position + Vector2.left;
                StartPos = transform.position;
                LastMove = Vector2.left;
                StartMove();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) == true || Input.GetKeyDown(KeyCode.D) == true)
            {
                CanMove = false;
                Moving = true;
                TargetPos = (Vector2)transform.position + Vector2.right;
                StartPos = transform.position;
                LastMove = Vector2.right;
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
        else
        {
            Move.Play();
        }
        
        //Pink (and Start) = Floor: No special function - DONE
        //Red (and Border) = Wall: Cannot move through it - DONE
        //Yellow (and Static) = Electric: Destroys the Player & restarts the puzzle - DONE
        //Orange = Helium: Makes the Player "Light;" Light can not pass over Fan (Blue) tiles,
        //but can pass over Trapdoor (Green) tiles - DONE
        //Purple = Factory: Makes the Player "Heavy" and pushes them to the next tile;
        //Heavy can pass over Fan (Blue) tiles, but can not pass over Trapdoor (Green) tiles - DONE
        //Blue = Fan: Light can not pass over it; Overdrives when next to an Electric (Yellow) tile
        //(Nothing can pass over it when Overdriven) - DONE
        //Green = Trapdoor: Heavy can not pass over it - DONE
        //Border: Walls off the puzzle; same function as Wall (Red) - DONE
        //Static: Appears as game becomes corrupted; same function as Electric (Yellow) - DONE
        //Start: Player starts here; same function as Floor (Pink) - DONE

        //End: Touch to end puzzle - DONE
        //Grey = Button: Disables or Changes Tiles - DONE
        //Brown = Moving: Moves between Waypoints on a Loop - 
        //Teleport?
    }

    void StopMovement()
    {
        Moving = false;

        //End Function Early || Change Movement
        if(CurrentTile.tag == "Yellow")
        {
            Yellow.Play();
            Invoke("reload", 1);
            //Death Animation?
        }

        else if(CurrentTile.tag == "End")
        {
            Finish.Play();
            Invoke("NextStage", 1);
        }

        else if(CurrentTile.tag == "Purple")
        {
            Weight = 1;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(200 / 225f, 145 / 255f, 1);
            Moving = true;
            TargetPos = (Vector2)transform.position + LastMove;
            StartPos = transform.position;

            RaycastHit2D Hit = Physics2D.Raycast(StartPos + (TargetPos - StartPos) * .5f, TargetPos - StartPos, 1);
            if (Hit.collider.tag == "Red")
            {
                Moving = false;
                transform.position = CurrentTile.transform.position;
                CanMove = true;
            }
        }
        

        else
        {
            //Landing on Tile
            if(CurrentTile.tag == "Orange")
            {
                Weight = -1;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 200/255f, 145/255f);
            }

            if(CurrentTile.tag == "Green" && (Weight == 1 || Weight == 0))
            {
                CurrentTile = PrevTile;
            }

            if (CurrentTile.tag == "Blue" && (Weight == -1 || Weight == 0))
            {
                CurrentTile = PrevTile;
            }

            else if(CurrentTile.tag == "Blue")
            {
                RaycastHit2D Hit = Physics2D.Raycast(StartPos + (TargetPos - StartPos) * .5f, 
                                                     TargetPos - StartPos,
                                                     1);
                if (Hit.collider.tag == "Red")
                {
                    
                }
                else
                {
                    GameObject[] Neighbors = GetNeigh(Hit.collider.gameObject);
                    for (int i = 0; i < Neighbors.Length; i++)
                    {
                        if (Neighbors[i].tag == "Yellow")
                        {
                            CurrentTile = PrevTile;
                        }
                    }
                }
            }

            if(CurrentTile.tag == "Button")
            {
                CurrentTile.SendMessage("Change");
                //Multiple Scripts can Run -- Change Multiple Things
            }

            transform.position = CurrentTile.transform.position;

            CanMove = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Collided");
        if (CurrentTile != null)
        {
            PrevTile = CurrentTile;
        }
        CurrentTile = collision.gameObject;
    }

    GameObject[] GetNeigh(GameObject TargetTile)
    {
        GameObject[] Neighbors = new GameObject[4];

        RaycastHit2D Hit = Physics2D.Raycast((Vector2)TargetTile.transform.position + Vector2.up * .5f, Vector2.up, 1);
        if(Hit.collider != null)
            Neighbors[0] = Hit.collider.gameObject;
        Hit = Physics2D.Raycast((Vector2)TargetTile.transform.position + Vector2.down * .5f, Vector2.down, 1);
        if (Hit.collider != null)
            Neighbors[1] = Hit.collider.gameObject;
        Hit = Physics2D.Raycast((Vector2)TargetTile.transform.position + Vector2.left * .5f, Vector2.left, 1);
        if (Hit.collider != null)
            Neighbors[2] = Hit.collider.gameObject;
        Hit = Physics2D.Raycast((Vector2)TargetTile.transform.position + Vector2.right * .5f, Vector2.right, 1);
        if (Hit.collider != null)
            Neighbors[3] = Hit.collider.gameObject;

        return Neighbors;
    }

    void reload()
    {
        transform.position = OldStart.transform.position;
        Moving = false;
        CanMove = true;
        Weight = 0;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
    }

    void NextStage()
    {
        End = CurrentTile.GetComponent<EndTile>();

        if(End.NewStart != null)
        {
            transform.position = End.NewStart.transform.position;
            OldStart = End.NewStart;
            Weight = 0;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);

            CamDir = End.CamX - Camera.main.transform.position.x;

            InvokeRepeating("CameraUpdate", 0, .008f);
        }

        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void CameraUpdate()
    {

        float sizedif = Mathf.Abs (End.CamSize - Camera.main.orthographicSize);
        float posdif = Mathf.Abs (End.CamX - Camera.main.transform.position.x);

        if (sizedif > .1)
        {
            Camera.main.orthographicSize += Time.deltaTime * (CamSpeed/2 * CamDir/Mathf.Abs(CamDir));
        }

        if (posdif > .1)
        {
            Camera.main.transform.position += Time.deltaTime * (CamSpeed * CamDir / Mathf.Abs(CamDir) * Vector3.right);
        }

        //Debug.Log(sizedif);
        //Debug.Log(posdif);

        if (sizedif <= .1 && posdif <= .1)
        {
            CancelInvoke("CameraUpdate");
            Moving = false;
            CanMove = true;
            Camera.main.orthographicSize = End.CamSize;
            Camera.main.transform.position = new Vector3(End.CamX, 0, -10);
        }
    }

}
