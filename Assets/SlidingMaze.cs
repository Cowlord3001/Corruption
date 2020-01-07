using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingMaze : MonoBehaviour {

    public GameObject RedTile, PurpleTile, StartTile, EndTile, PinkTile;
    public int[ , ] Tiles;
	Vector2[] PathPoints;

	// Use this for initialization
	void Start ()
    {
		PathPoints = new Vector2[7];
		
        Tiles = new int[35, 19];
		
		bool Success = MainPath();
		
		while(Success == false)
		{
			Debug.Log("Failed. Retrying");
			Success = MainPath();
			Tiles = new int[35, 19];
			
		}
        DrawBoard();
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
                    Instantiate(PinkTile, transform.position + new Vector3(i, j, 0), Quaternion.identity);

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
                    Instantiate(PurpleTile, transform.position + new Vector3(i, j, 0), Quaternion.identity);
                }
            }
        }
    }

    bool MainPath()
    {
        Vector2 Dir = Vector2.left;
        int X, Y;
        int Start = Random.Range(0, 19);
        Tiles[0, Start] = 2;
        //Start Tile Banning
        if(Start != 18)
        {
            Tiles[0, Start + 1] = -1;
        }
        if(Start != 0)
        {
            Tiles[0, Start - 1] = -1;
        }
        for (int j = 1; j < 35; j++)
        {
            if (Tiles[j, Start] == 0)
                Tiles[j, Start] = -1;
        }
        X = 0;
        Y = Start;

        //Main Path Loop
        for (int i = 0; i < 7; i++)
        {
            Dir = RandDir(X, Y, -Dir);
			PathPoints[i] = new Vector2(X, Y);
			if(Dir == Vector2.zero)
			{
				
				return false;

			}
            Vector2 BlockCoords = RandCoords(X, Y, Dir);

            Debug.Log(Dir);
            //Horizontal or Vertical
            if (Mathf.Abs(Dir.x) > .01)
            {
                //Horizontal
                //////////////////////if statements?
                for (int a = X; a <= (int)BlockCoords.x; a++)
                {
                    if (Y != 0 && Tiles[a, Y-1] == 0)
                    Tiles[a, Y-1] = -1;
                    if (Y != 18 && Tiles[a, Y + 1] == 0)
                    Tiles[a, Y + 1] = -1;
                }
                for (int a = X; a >= (int)BlockCoords.x; a--)
                {
                    if (Y != 0 && Tiles[a, Y - 1] == 0)
                        Tiles[a, Y - 1] = -1;
                    if (Y != 18 && Tiles[a, Y + 1] == 0)
                        Tiles[a, Y + 1] = -1;
                }
                //Ban perpindicular to Horizontal
                //for (int j = 0; j < 19; j++)
                //{
                //    if(Tiles[(int)BlockCoords.x, (int)j] == 0)
                //    Tiles[(int)BlockCoords.x, (int)j] = -1;
                //}
            }
            else
            {
                //Vertically

                for (int a = Y; a <= (int)BlockCoords.y; a++)
                {
                    if (X != 0 && Tiles[X - 1, a] == 0)
                        Tiles[X - 1, a] = -1;
                    if (X != 34 && Tiles[X + 1, a] == 0)
                        Tiles[X + 1, a] = -1;
                }
                for (int a = Y; a >= (int)BlockCoords.y; a--)
                {
                    if (X != 0 && Tiles[X - 1, a] == 0)
                        Tiles[X - 1, a] = -1;
                    if (X != 34 && Tiles[X + 1, a] == 0)
                        Tiles[X + 1, a] = -1;
                }
                //Ban perpindicular to Vertical
                //for (int j = 0; j < 35; j++)
                //{
                //    if(Tiles[j, (int)BlockCoords.y] == 0)
                //    Tiles[j, (int)BlockCoords.y] = -1;
                //}
            }

            //Place Block at destination coords
            Tiles[(int)BlockCoords.x, (int)BlockCoords.y] = 1;

            //Move Path walker back one
            X = (int)BlockCoords.x - (int)Dir.x;
            Y = (int)BlockCoords.y - (int)Dir.y;
            //}

        }

		return true;
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
			return Vector2.zero;
            //DrawBoard();
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
