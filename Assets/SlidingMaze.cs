using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingMaze : MonoBehaviour {

    public GameObject RedTile, PurpleTile, StartTile, EndTile, PinkTile;
    public int[ , ] Tiles;


	// Use this for initialization
	void Start ()
    {
        Tiles = new int[35, 19];
        Invoke("MainPath", 1);
        //MainPath();
        //DrawBoard();
        //Tiles[_,_] = _ {0-2}
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void DrawBoard()
    {
        for (int i = 0; i < 35; i++)
        {
            for (int j = 0; j < 19; j++)
            {
                if(Tiles[i,j] == 0)
                {
                    Instantiate(PurpleTile, transform.position + new Vector3(i, j, 0), Quaternion.identity);

                }
                else if(Tiles[i,j] == 1)
                {
                    Instantiate(RedTile, transform.position + new Vector3(i, j, 0), Quaternion.identity);

                }
                else if (Tiles[i, j] == 2)
                {
                    Instantiate(StartTile, transform.position + new Vector3(i, j, 0), Quaternion.identity);

                }
                else if (Tiles[i, j] == -1)
                {
                    Instantiate(PinkTile, transform.position + new Vector3(i, j, 0), Quaternion.identity);
                }
            }
        }
    }

    void MainPath()
    {
        int X, Y;
        int Start = Random.Range(0, 19);
        Tiles[0, Start] = 2;
        X = 0;
        Y = Start;
        for (int i = 0; i < 7; i++)
        {
            Vector2 Dir = RandDir(X, Y);
            Vector2 BlockCoords = RandCoords(X, Y, Dir);
            //if( (BlockCoords.x == 34 && (int)Dir.x > 0)
            //    || (BlockCoords.x == 0 && (int)Dir.x < 0)
            //    || (BlockCoords.y == 18 && (int)Dir.y > 0)
            //    || (BlockCoords.y == 0 && (int)Dir.y < 0))
            //{
            //    Tiles[(int)BlockCoords.x, (int)BlockCoords.y] = -1;

            //    X = (int)BlockCoords.x;
            //    Y = (int)BlockCoords.y;
            //}
            //else
            //{

            if(Mathf.Abs(Dir.x) > .01)
            {
                for (int j = 0; j < 19; j++)
                {
                    Tiles[(int)BlockCoords.x, (int)j] = -1;
                }
            }
            else
            {
                for (int j = 0; j < 35; j++)
                {
                    Tiles[j, (int)BlockCoords.y] = -1;
                }
            }

            Tiles[(int)BlockCoords.x, (int)BlockCoords.y] = 1;

            X = (int)BlockCoords.x - (int)Dir.x;
            Y = (int)BlockCoords.y - (int)Dir.y;
            //}

            Debug.Log(Dir);
        }

        DrawBoard();
    }

    Vector2 RandDir(int X, int Y)
    {

        List<Vector2> Dirs = new List<Vector2>();
        
        if (Y < 17 && Tiles[X, Y + 1] == 0 && Tiles[X, Y + 2] == 0)
            {
                Dirs.Add(Vector2.up);
            }

        if (Y > 1 && Tiles[X, Y - 1] == 0 && Tiles[X, Y - 2] == 0)
        {
            Dirs.Add(Vector2.down);
        }

        if (X < 33 && Tiles[X + 1, Y] == 0 && Tiles[X + 2, Y] == 0)
        {
            Dirs.Add(Vector2.right);
        }

        if (X > 1 && Tiles[X - 1, Y] == 0 && Tiles[X - 2, Y] == 0)
        {
            Dirs.Add(Vector2.left);
        }

        int i = Random.Range(0, Dirs.Count);
        return Dirs[i];

        //while (true)
        //{

        //    int Rand = Random.Range(0, 3);

        //    if(Rand == 0)
        //    {
        //        if(Y == 19)
        //        {

        //        }
        //        else if(Tiles[X, Y+1] == 0)
        //        {
        //            return Vector2.up;
        //        }
        //    }

        //    if (Rand == 1)
        //    {
        //        if (X == 35)
        //        {

        //        }
        //        else if (Tiles[X + 1, Y] == 0)
        //        {
        //            return Vector2.right;
        //        }
        //    }

        //    if (Rand == 2)
        //    {
        //        if (Y == 0)
        //        {

        //        }
        //        else if (Tiles[X, Y - 1] == 0)
        //        {
        //            return Vector2.down;
        //        }
        //    }

        //    if (Rand == 3)
        //    {
        //        if (X == 0)
        //        {

        //        }
        //        else if (Tiles[X - 1, Y] == 0)
        //        {
        //            return Vector2.left;
        //        }
        //    }
        //}
    }

    Vector2 RandCoords(int X, int Y, Vector2 Dir)
    {
        X += (int)Dir.x;
        Y += (int)Dir.y;

        Tiles[X, Y] = -1;

        X += (int)Dir.x;
        Y += (int)Dir.y;

        List<int> GoodBlocks = new List<int>();
        while (X < 35 && Y < 19 && X >= 0 && Y >= 0)
        {
            //if(Tiles[X, Y] == 1)
            //{
            //    Debug.Log("Hit a Red");
            //    break;
            //}

            if (Tiles[X, Y] != -1 && Tiles[X, Y] != 2)
            {
                if (Dir.x == 0)
                {
                    GoodBlocks.Add(Y);
                }
                else if (Dir.y == 0)
                {
                    GoodBlocks.Add(X);
                }

            }

            X += (int)Dir.x;
            Y += (int)Dir.y;
        }

        int End = Random.Range(0, GoodBlocks.Count);

        Vector2 EndCoords;
        if (Dir.x == 0)
        {
            EndCoords = new Vector2(X, GoodBlocks[End]);
            for (int i = 0; i < End; i++)
            {
                Tiles[X, GoodBlocks[i]] = -1;
            }
        }
        else
        {
            EndCoords = new Vector2(GoodBlocks[End], Y);
            for (int i = 0; i < End; i++)
            {
                Tiles[GoodBlocks[i], Y] = -1;
            }
        }
        return EndCoords;
    }
}
