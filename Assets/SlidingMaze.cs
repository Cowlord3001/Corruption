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
        Vector2 Dir = Vector2.left;
        int X, Y;
        int Start = Random.Range(0, 19);
        Tiles[0, Start] = 2;
        Tiles[0, Start + 1] = -1;
        Tiles[0, Start - 1] = -1;
        for (int j = 1; j < 35; j++)
        {
            if (Tiles[j, Start] == 0)
                Tiles[j, Start] = -1;
        }
        X = 0;
        Y = Start;
        for (int i = 0; i < 7; i++)
        {
            Dir = RandDir(X, Y, Dir);
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
                    if(Tiles[(int)BlockCoords.x, (int)j] == 0)
                    Tiles[(int)BlockCoords.x, (int)j] = -1;
                }
            }
            else
            {
                for (int j = 0; j < 35; j++)
                {
                    if(Tiles[j, (int)BlockCoords.y] == 0)
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

    Vector2 RandDir(int X, int Y, Vector2 BanDir)
    {

        List<Vector2> Dirs = new List<Vector2>();
        bool ValidDir = false;

        //Border

        for (int u = Y; u < 19; u++)
        {
            if (Tiles[X, u] == 1)
            {
                break;
            }

            if (Tiles[X,u] == 0)
            {
                ValidDir = true;
            }
        }

        if (ValidDir == true && Vector2.up != BanDir)
            {
                Dirs.Add(Vector2.up);
            }

        ValidDir = false;

        //Border

        for (int u = Y; u > -1; u--)
        {
            if (Tiles[X, u] == 1)
            {
                break;
            }

            if (Tiles[X, u] == 0)
            {
                ValidDir = true;
            }
        }

        if (ValidDir == true && Vector2.down != BanDir)
        {
            Dirs.Add(Vector2.down);
        }

        ValidDir = false;

        //Border

        for (int u = X; u < 35; u++)
        {
            if (Tiles[u, Y] == 1)
            {
                break;
            }

            if (Tiles[u, Y] == 0)
            {
                ValidDir = true;
            }
        }

        if (ValidDir == true && Vector2.right != BanDir)
        {
            Dirs.Add(Vector2.right);
        }

        ValidDir = false;

        //Border

        for (int u = X; u > -1; u--)
        {
            if (Tiles[u, Y] == 1)
            {
                break;
            }

            if (Tiles[u, Y] == 0)
            {
                ValidDir = true;
            }
        }

        if (ValidDir == true && Vector2.left != BanDir)
        {
            Dirs.Add(Vector2.left);
        }

        ValidDir = false;

        //Border

        int i = Random.Range(0, Dirs.Count);

        if(Dirs.Count == 0)
        {
            DrawBoard();
        }
        return Dirs[i];
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
