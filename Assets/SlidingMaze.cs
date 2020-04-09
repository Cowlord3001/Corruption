using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingMaze : MonoBehaviour {

    public GameObject RedTile, PurpleTile, StartTile, EndTile, PinkTile, BorderTile, BlueTile, GreyTile;
    public int[ , ] Tiles;
	Vector2[] PathPoints;
    public int MainPathLength;

    List<GameObject> TileObjects;

    List<Vector3> ScoreQueue;

    public bool Developer_Mode;

    // Use this for initialization
    void Start ()
    {
        TileObjects = new List<GameObject>();

		PathPoints = new Vector2[MainPathLength];

        ScoreQueue = new List<Vector3>();

        StartCoroutine(CreateBoard());
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
                    GameObject GO = Instantiate(PinkTile, transform.position + new Vector3(i, j, 0), Quaternion.identity);
                    TileObjects.Add(GO);
                }
                else if(Tiles[i,j] == 1)
                {
                    GameObject GO = Instantiate(RedTile, transform.position + new Vector3(i, j, 0), Quaternion.identity);
                    TileObjects.Add(GO);
                }
                else if (Tiles[i, j] == 2)
                {
                    //Instantiate(StartTile, transform.position + new Vector3(i, j, 0), Quaternion.identity);
                    StartTile.transform.position = transform.position + new Vector3(i, j, 0);
                }
                else if (Tiles[i, j] == -1)
                {
                    GameObject GO = Instantiate(PurpleTile, transform.position + new Vector3(i, j, 0), Quaternion.identity);
                    TileObjects.Add(GO);
                }
                else if (Tiles[i, j] == 3)
                {
                    EndTile.transform.position = transform.position + new Vector3(i, j, 0);
                }
                else if (Tiles [i, j] == 10)
                {
                    GameObject GO = Instantiate(BorderTile, transform.position + new Vector3(i, j, 0), Quaternion.identity);
                    TileObjects.Add(GO);
                }
                else if (Tiles [i, j] == 20)
                {
                    GameObject GO = Instantiate(BlueTile, transform.position + new Vector3(i, j, 0), Quaternion.identity);
                    TileObjects.Add(GO);
                }
                else if (Tiles [i,j] == 30)
                {
                    GameObject GO = Instantiate(GreyTile, transform.position + new Vector3(i, j, 0), Quaternion.identity);
                    TileObjects.Add(GO);
                }
            }
        }
    }

    public void EraseBoard()
    {
        for (int i = 0; i < TileObjects.Count; i++)
        {
            Destroy(TileObjects[i]);
        }
        TileObjects.Clear();
    }

    bool MainPath()
    {
        Vector2 Dir = Vector2.right;
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
        X = 0;
        Y = Start;

        //Main Path Loop
        for (int i = 0; i < MainPathLength; i++)
        {
            Dir = RandDir(X, Y, -Dir);
			PathPoints[i] = new Vector2(X, Y);
			if(Dir == Vector2.zero)
			{
				
				return false;

			}

            Vector2 BlockCoords = RandCoords(X, Y, Dir);
            if(BlockCoords == Vector2.zero)
            {
                return false;
            }

            //Debug.Log(Dir);
            //Horizontal or Vertical
            if (Mathf.Abs(Dir.x) > .01)
            {
                //Horizontal
                //////////////////////if statements?
                for (int a = X; a <= Mathf.RoundToInt(BlockCoords.x); a++)
                {
                    if (Y != 0 && Tiles[a, Y-1] == 0)
                    Tiles[a, Y-1] = -1;
                    if (Y != 18 && Tiles[a, Y + 1] == 0)
                    Tiles[a, Y + 1] = -1;
                    if (Tiles[a, Y] == 0)
                    Tiles[a, Y] = -1;
                }
                for (int a = X; a >= Mathf.RoundToInt(BlockCoords.x); a--)
                {
                    if (Y != 0 && Tiles[a, Y - 1] == 0)
                        Tiles[a, Y - 1] = -1;
                    if (Y != 18 && Tiles[a, Y + 1] == 0)
                        Tiles[a, Y + 1] = -1;
                    if (Tiles[a, Y] == 0)
                        Tiles[a, Y] = -1;
                }
                //Ban perpindicular to Horizontal
                //for (int j = 0; j < 19; j++)
                //{
                //    if(Tiles[Mathf.RoundToInt(BlockCoords.x), Mathf.RoundToInt(j)] == 0)
                //    Tiles[Mathf.RoundToInt(BlockCoords.x), Mathf.RoundToInt(j)] = -1;
                //}
            }
            else
            {
                //Vertically

                for (int a = Y; a <= Mathf.RoundToInt(BlockCoords.y); a++)
                {
                    if (X != 0 && Tiles[X - 1, a] == 0)
                        Tiles[X - 1, a] = -1;
                    if (X != 34 && Tiles[X + 1, a] == 0)
                        Tiles[X + 1, a] = -1;
                    if (Tiles[X, a] == 0)
                        Tiles[X, a] = -1;
                }
                for (int a = Y; a >= Mathf.RoundToInt(BlockCoords.y); a--)
                {
                    if (X != 0 && Tiles[X - 1, a] == 0)
                        Tiles[X - 1, a] = -1;
                    if (X != 34 && Tiles[X + 1, a] == 0)
                        Tiles[X + 1, a] = -1;
                    if (Tiles[X, a] == 0)
                        Tiles[X, a] = -1;
                }
                //Ban perpindicular to Vertical
                //for (int j = 0; j < 35; j++)
                //{
                //    if(Tiles[j, Mathf.RoundToInt(BlockCoords.y)] == 0)
                //    Tiles[j, Mathf.RoundToInt(BlockCoords.y)] = -1;
                //}
            }

            //Place Block at destination coords
            if(i == MainPathLength - 1)
            {
                Tiles[Mathf.RoundToInt(BlockCoords.x), Mathf.RoundToInt(BlockCoords.y)] = 3;
            }
            else
            {
                Tiles[Mathf.RoundToInt(BlockCoords.x), Mathf.RoundToInt(BlockCoords.y)] = 1;
            }

            //Move Path walker back one
            X = Mathf.RoundToInt(BlockCoords.x) - Mathf.RoundToInt(Dir.x);
            Y = Mathf.RoundToInt(BlockCoords.y) - Mathf.RoundToInt(Dir.y);

            //Ban Blocks Right of Start
            if (i == 0)
            {
                //Debug.Log(Dir);
                for (int j = 1; j < 35; j++)
                {
                    if (Tiles[j, Start] == 0)
                        Tiles[j, Start] = -1;
                }
            }


        }

		return true;
    }

    bool SidePath(int PathNum)
    {
        Vector2 Dir = Vector2.zero; //Might be bad & broken????
        int X, Y;
        X = Mathf.RoundToInt(PathPoints[PathNum].x);
        Y = Mathf.RoundToInt(PathPoints[PathNum].y);

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
            if (BlockCoords == Vector2.zero)
            {
                return false;
            }

            //Debug.Log(Dir);
            //Horizontal or Vertical   //Soft Ban for buckshot?
            if (Mathf.Abs(Dir.x) > .01)
            {
                //Horizontal
                //////////////////////if statements?
                for (int a = X; a <= Mathf.RoundToInt(BlockCoords.x); a++)
                {
                    if (Y != 0 && Tiles[a, Y - 1] == 0)
                        Tiles[a, Y - 1] = -1;
                    if (Y != 18 && Tiles[a, Y + 1] == 0)
                        Tiles[a, Y + 1] = -1;
                    if (Tiles[a, Y] == 0)
                        Tiles[a, Y] = -1;
                }
                for (int a = X; a >= Mathf.RoundToInt(BlockCoords.x); a--)
                {
                    if (Y != 0 && Tiles[a, Y - 1] == 0)
                        Tiles[a, Y - 1] = -1;
                    if (Y != 18 && Tiles[a, Y + 1] == 0)
                        Tiles[a, Y + 1] = -1;
                    if (Tiles[a, Y] == 0)
                        Tiles[a, Y] = -1;
                }
                //Ban perpindicular to Horizontal
                //for (int j = 0; j < 19; j++)
                //{
                //    if(Tiles[Mathf.RoundToInt(BlockCoords.x), Mathf.RoundToInt(j)] == 0)
                //    Tiles[Mathf.RoundToInt(BlockCoords.x), Mathf.RoundToInt(j)] = -1;
                //}
            }
            else
            {
                //Vertically

                for (int a = Y; a <= Mathf.RoundToInt(BlockCoords.y); a++)
                {
                    if (X != 0 && Tiles[X - 1, a] == 0)
                        Tiles[X - 1, a] = -1;
                    if (X != 34 && Tiles[X + 1, a] == 0)
                        Tiles[X + 1, a] = -1;
                    if (Tiles[X, a] == 0)
                        Tiles[X, a] = -1;
                }
                for (int a = Y; a >= Mathf.RoundToInt(BlockCoords.y); a--)
                {
                    if (X != 0 && Tiles[X - 1, a] == 0)
                        Tiles[X - 1, a] = -1;
                    if (X != 34 && Tiles[X + 1, a] == 0)
                        Tiles[X + 1, a] = -1;
                    if (Tiles[X, a] == 0)
                        Tiles[X, a] = -1;
                }
                //Ban perpindicular to Vertical
                //for (int j = 0; j < 35; j++)
                //{
                //    if(Tiles[j, Mathf.RoundToInt(BlockCoords.y)] == 0)
                //    Tiles[j, Mathf.RoundToInt(BlockCoords.y)] = -1;
                //}
            }

            //Place Block at destination coords
            Tiles[Mathf.RoundToInt(BlockCoords.x), Mathf.RoundToInt(BlockCoords.y)] = 10;

            //Move Path walker back one
            X = Mathf.RoundToInt(BlockCoords.x) - Mathf.RoundToInt(Dir.x);
            Y = Mathf.RoundToInt(BlockCoords.y) - Mathf.RoundToInt(Dir.y);

            SidePath(X, Y);

        }

        return true;
    }

    bool SidePath(int X, int Y)
    {
        Vector2 Dir = Vector2.zero; //Might be bad & broken????

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
            if (BlockCoords == Vector2.zero)
            {
                return false;
            }

            //Debug.Log(Dir);
            //Horizontal or Vertical   //Soft Ban for buckshot?
            if (Mathf.Abs(Dir.x) > .01)
            {
                //Horizontal
                //////////////////////if statements?
                for (int a = X; a <= Mathf.RoundToInt(BlockCoords.x); a++)
                {
                    if (Y != 0 && Tiles[a, Y - 1] == 0)
                        Tiles[a, Y - 1] = -1;
                    if (Y != 18 && Tiles[a, Y + 1] == 0)
                        Tiles[a, Y + 1] = -1;
                    if (Tiles[a, Y] == 0)
                        Tiles[a, Y] = -1;
                }
                for (int a = X; a >= Mathf.RoundToInt(BlockCoords.x); a--)
                {
                    if (Y != 0 && Tiles[a, Y - 1] == 0)
                        Tiles[a, Y - 1] = -1;
                    if (Y != 18 && Tiles[a, Y + 1] == 0)
                        Tiles[a, Y + 1] = -1;
                    if (Tiles[a, Y] == 0)
                        Tiles[a, Y] = -1;
                }
                //Ban perpindicular to Horizontal
                //for (int j = 0; j < 19; j++)
                //{
                //    if(Tiles[Mathf.RoundToInt(BlockCoords.x), Mathf.RoundToInt(j)] == 0)
                //    Tiles[Mathf.RoundToInt(BlockCoords.x), Mathf.RoundToInt(j)] = -1;
                //}
            }
            else
            {
                //Vertically

                for (int a = Y; a <= Mathf.RoundToInt(BlockCoords.y); a++)
                {
                    if (X != 0 && Tiles[X - 1, a] == 0)
                        Tiles[X - 1, a] = -1;
                    if (X != 34 && Tiles[X + 1, a] == 0)
                        Tiles[X + 1, a] = -1;
                    if (Tiles[X, a] == 0)
                        Tiles[X, a] = -1;
                }
                for (int a = Y; a >= Mathf.RoundToInt(BlockCoords.y); a--)
                {
                    if (X != 0 && Tiles[X - 1, a] == 0)
                        Tiles[X - 1, a] = -1;
                    if (X != 34 && Tiles[X + 1, a] == 0)
                        Tiles[X + 1, a] = -1;
                    if (Tiles[X, a] == 0)
                        Tiles[X, a] = -1;
                }
                //Ban perpindicular to Vertical
                //for (int j = 0; j < 35; j++)
                //{
                //    if(Tiles[j, Mathf.RoundToInt(BlockCoords.y)] == 0)
                //    Tiles[j, Mathf.RoundToInt(BlockCoords.y)] = -1;
                //}
            }

            //Place Block at destination coords
            Tiles[Mathf.RoundToInt(BlockCoords.x), Mathf.RoundToInt(BlockCoords.y)] = 30;

            //Move Path walker back one
            X = Mathf.RoundToInt(BlockCoords.x) - Mathf.RoundToInt(Dir.x);
            Y = Mathf.RoundToInt(BlockCoords.y) - Mathf.RoundToInt(Dir.y);



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

            int x = Mathf.RoundToInt(ScoreQueue[RandQueueIndex].x);
            int y = Mathf.RoundToInt(ScoreQueue[RandQueueIndex].y);

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
                else if (Tiles[i, j] == 10 || Tiles[i, j] == 30)
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

        //Check North
        for (int u = Y+1; u < 19; u++)
        {
            if (Tiles[X, u] == 1 || Tiles[X, u] == 3 || Tiles[X, u] == 10 || Tiles[X, Y] == 30)
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

        //Check South
        for (int u = Y-1; u > -1; u--)
        {
            if (Tiles[X, u] == 1 || Tiles[X, u] == 3 || Tiles[X, u] == 10 || Tiles[X, Y] == 30)
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

        //Check East
        for (int u = X+1; u < 35; u++)
        {
            if (Tiles[u, Y] == 1 || Tiles[u, Y] == 3 || Tiles[u, Y] == 10 || Tiles[X, Y] == 30)
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

        //Check West
        for (int u = X-1; u > -1; u--)
        {
            if (Tiles[u, Y] == 1 || Tiles[u, Y] == 3 || Tiles[u, Y] == 10 || Tiles[X, Y] == 30)
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

        //Choose Random Direction
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
        X += Mathf.RoundToInt(Dir.x);
        Y += Mathf.RoundToInt(Dir.y);

        if (Tiles[X, Y] == 0 || Tiles[X, Y] == -1) //Protection against Dir being bad (red blocks, etc.)
        {
            Tiles[X, Y] = -1;
        }
        else
        {
            return Vector2.zero;
        }

        X += Mathf.RoundToInt(Dir.x);
        Y += Mathf.RoundToInt(Dir.y);
        
        List<int> GoodBlocks = new List<int>();
        while (X < 35 && Y < 19 && X >= 0 && Y >= 0)
        {

            if (Tiles[X, Y] == 1 || Tiles[X, Y] == 3 || Tiles[X, Y] == 10 || Tiles[X, Y] == 30)
            {
                break;
            }

            if (Tiles[X, Y] != 2)
            {
                if (Mathf.RoundToInt(Dir.x) == 0)
                {
                    GoodBlocks.Add(Y);
                }
                else if (Mathf.RoundToInt(Dir.y) == 0)
                {
                    GoodBlocks.Add(X);
                }

            }

            X += Mathf.RoundToInt(Dir.x);
            Y += Mathf.RoundToInt(Dir.y);
        }

        int End = Random.Range(/*1*/0, GoodBlocks.Count);

        if(GoodBlocks.Count == 0)
        {
            return Vector2.zero;
        }
        
        Vector2 EndCoords;
        if (Mathf.RoundToInt(Dir.x) == 0)
        {
            EndCoords = new Vector2(X, GoodBlocks[End]);
            //if (EndCoords != Vector2.zero/* && Tiles[X, GoodBlocks[End]] == 0*/)
            //{
            //    for (int i = 0; i < End; i++)
            //    {
            //        Tiles[X, GoodBlocks[i]] = -1;
            //    }
            //}
        }
        else if (Mathf.RoundToInt(Dir.y) == 0)
        {
            EndCoords = new Vector2(GoodBlocks[End], Y);
            //if (EndCoords != Vector2.zero/* && Tiles[GoodBlocks[End], Y] == 0*/)
            //{
            //    for (int i = 0; i < End; i++)
            //    {
            //        Tiles[GoodBlocks[i], Y] = -1;
            //    }
            //}
        }
        //Failure
        else
        {
            EndCoords = new Vector2(0, 0);
        }

        //Don't place on banned tile
        if (Tiles[Mathf.RoundToInt(EndCoords.x), Mathf.RoundToInt(EndCoords.y)] == -1)
        {
            return Vector2.zero;
        }
        else
        {
            return EndCoords;
        }
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
    
    public IEnumerator CreateBoard()
    {
        Tiles = new int[35, 19];
        bool Success = MainPath();

        while (Success == false)
        {
            Tiles = new int[35, 19];
            Success = MainPath();
        }

        if (Developer_Mode == true)
        {
            DrawBoard();
            yield return new WaitForSeconds(2f);
            EraseBoard();
        }

        for (int i = 0; i < MainPathLength; i++)
        {
            SidePath(i);
            //DrawBoard();
            //yield return new WaitForSeconds(2f);
            //EraseBoard();
        }

        if (Developer_Mode == true)
        {
            DrawBoard();
            yield return new WaitForSeconds(2f);
            EraseBoard();
        }

        Scatter();

        if (Developer_Mode == true)
        {
            DrawBoard();
            yield return new WaitForSeconds(2f);
            EraseBoard();
        }

        if (Developer_Mode != true)
        {
            Hide();
        }

        DrawBoard();
    }
}
