using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingMaze : MonoBehaviour {

    public GameObject RedTile, PurpleTile, StartTile, EndTile, PinkTile, BorderTile, BlueTile;
    public int[ , ] Tiles;
	Vector2[] PathPoints;

    List<Vector3> ScoreQueue;

    // Use this for initialization
    void Start ()
    {
		PathPoints = new Vector2[7]; //If changes made, this may break
		
        Tiles = new int[35, 19];

        ScoreQueue = new List<Vector3>();
		
		bool Success = MainPath();
		
		while(Success == false) // Add one for Sidepath
		{
			//Debug.Log("Failed. Retrying");
			Tiles = new int[35, 19];
            Success = MainPath();
        }

        for (int i = 0; i < 7; i++)
        {
            SidePath(i);
        }

        Scatter();


        //for (int i = 0; i < ScoreQueue.Count; i++)
        //{
        //    Debug.Log(ScoreQueue[i]);
        //}

        Hide();

        DrawBoard();

        //Debug.Log(Score(0, 0));
        //Tiles[_,_] = _ {0-2}
    }
	

	// Update is called once per frame
	void FixedUpdate ()
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
                    //Instantiate(StartTile, transform.position + new Vector3(i, j, 0), Quaternion.identity);
                    StartTile.transform.position = transform.position + new Vector3(i, j, 0);
                }
                else if (Tiles[i, j] == -1)
                {
                    Instantiate(PurpleTile, transform.position + new Vector3(i, j, 0), Quaternion.identity);
                }
                else if (Tiles[i, j] == 3)
                {
                    Instantiate(EndTile, transform.position + new Vector3(i, j, 0), Quaternion.identity);
                }
                else if (Tiles [i, j] == 10)
                {
                    Instantiate(BorderTile, transform.position + new Vector3(i, j, 0), Quaternion.identity);
                }
                else if (Tiles [i, j] == 20)
                {
                    Instantiate(BlueTile, transform.position + new Vector3(i, j, 0), Quaternion.identity);
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

            //Debug.Log(Dir);
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
            if(i == 6)
            {
                Tiles[(int)BlockCoords.x, (int)BlockCoords.y] = 3;
            }
            else
            {
                Tiles[(int)BlockCoords.x, (int)BlockCoords.y] = 1;
            }

            //Move Path walker back one
            X = (int)BlockCoords.x - (int)Dir.x;
            Y = (int)BlockCoords.y - (int)Dir.y;
            //}

        }

		return true;
    }

    bool SidePath(int PathNum)
    {
        Vector2 Dir = Vector2.zero; //Might be bad & broken????
        int X, Y;
        X = (int)PathPoints[PathNum].x;
        Y = (int)PathPoints[PathNum].y;

        //Main Path Loop
        for (int i = 0; i < 3; i++) //Random number of blocks (Dynamic - Maximum of __, if impossible, end but don't reset)? For main path too?
        {
            Dir = RandDir(X, Y, -Dir);
            //PathPoints[i] = new Vector2(X, Y);
            if (Dir == Vector2.zero)
            {

                return false;

            }
            Vector2 BlockCoords = RandCoords(X, Y, Dir);

            //Debug.Log(Dir);
            //Horizontal or Vertical   //Soft Ban for buckshot?
            if (Mathf.Abs(Dir.x) > .01)
            {
                //Horizontal
                //////////////////////if statements?
                for (int a = X; a <= (int)BlockCoords.x; a++)
                {
                    if (Y != 0 && Tiles[a, Y - 1] == 0)
                        Tiles[a, Y - 1] = -1;
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
            Tiles[(int)BlockCoords.x, (int)BlockCoords.y] = 10;

            //Move Path walker back one
            X = (int)BlockCoords.x - (int)Dir.x;
            Y = (int)BlockCoords.y - (int)Dir.y;
            //}

        }

        return true;
    }

    bool Scatter()
    {
        do
        {
            ScoreQueue.Clear();
            for (int i = 0; i < 35; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    if (Tiles[i, j] == 0)
                    {

                        AddList(i, j);

                    }
                }
            }

            if(ScoreQueue.Count == 0)
            {
                break;
            }

            int temp = 40; //Prone to Change
            
            if(ScoreQueue.Count < 40) //Prone to Change
            {
                temp = ScoreQueue.Count;
            }

            //Debug.Log("Queue = " + ScoreQueue.Count);
            //Debug.Log("Temp = " + temp);

            int RandQueueIndex = Random.Range(0, temp);

            int x = (int)ScoreQueue[RandQueueIndex].x;
            int y = (int)ScoreQueue[RandQueueIndex].y;

            for (int i = 0; i < 5; i++) //y
            {
                for (int j = 0; j < 5; j++) //x
                {
                    //Debug.Log((x-2+j) + ", " + (y-2+i));
                    if (x - 2 + j >= 0 && x - 2 + j < 35 && y - 2 + i >= 0 && y - 2 + i < 19 && Tiles[x - 2 + j, y - 2 + i] == 0)
                    {
                        Tiles[x - 2 + j, y - 2 + i] = 20;
                    }
                }
            }
            Tiles[x, y] = 1;

                    } while (ScoreQueue.Count > 30); //Prone to Change

        return true;
    }

    void Hide()
    {
        for (int i = 0; i < 35; i++) //x
        {
            for (int j = 0; j < 19; j++) //y
            {
                if(Tiles[i, j] == 0 || Tiles[i, j] == 20)
                {
                    Tiles[i, j] = -1;
                }
                else if (Tiles[i, j] == 10)
                {
                    Tiles[i, j] = 1;
                }
                else
                {

                }
            }

        }
    }

    Vector2 RandDir(int X, int Y, Vector2 BanDir)
    {

        List<Vector2> Dirs = new List<Vector2>();
        bool ValidDir = false;

        //Border

        for (int u = Y; u < 19; u++)
        {
            if (Tiles[X, u] == 1 || Tiles[X, u] == 3 || Tiles[X, u] == 10)
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
            if (Tiles[X, u] == 1 || Tiles[X, u] == 3 || Tiles[X, u] == 10)
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
            if (Tiles[u, Y] == 1 || Tiles[u, Y] == 3 || Tiles[u, Y] == 10)
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
            if (Tiles[u, Y] == 1 || Tiles[u, Y] == 3 || Tiles[u, Y] == 10)
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

            if (Tiles[X, Y] == 1 || Tiles[X, Y] == 3 || Tiles[X, Y] == 10)
            {
                break;
            }

            if (/*Tiles[X, Y] != -1 && */Tiles[X, Y] != 2)
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

    int Score (int x, int y)
    {
        int score = 0;

        for (int i = 0; i < 5; i++) //y
        {
            for (int j = 0; j < 5; j++) //x
            {
                //Debug.Log((x-2+j) + ", " + (y-2+i));
                if(x-2+j >= 0 && x-2+j < 35 && y-2+i >= 0 && y-2+i < 19 && Tiles[x-2+j, y-2+i] == 0)
                {
                    //Debug.Log(Tiles[x - 2 + j, y - 2 + i]);
                    score += 1;
                }
            }
        }
        return score;
    }

    void AddList(int x, int y)
    {
        if(Tiles[x,y] == 0)
        {
            if (ScoreQueue.Count == 0)
            {
                ScoreQueue.Add(new Vector3(x, y, Score(x, y)));
            }
            else
            {
                for (int i = 0; i < ScoreQueue.Count; i++)
                {
                    int z = Score(x, y);
                    if(z >= ScoreQueue[i].z)
                    {
                        ScoreQueue.Insert(i, new Vector3(x, y, z));
                        break;
                    }
                    else if(i == ScoreQueue.Count - 1)
                    {
                        ScoreQueue.Add(new Vector3(x, y, z));
                        break;
                    }
                }
            }
        }
    }
    
}
