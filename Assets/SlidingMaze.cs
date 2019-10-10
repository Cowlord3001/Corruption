using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingMaze : MonoBehaviour {

    public GameObject RedTile, PurpleTile, StartTile, EndTile;
    public int[ , ] Tiles;


	// Use this for initialization
	void Start ()
    {
        Tiles = new int[35, 19];
        MainPath();
        DrawBoard();
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
            }
        }
    }

    void MainPath()
    {
        int X, Y;
        int Start = Random.Range(0, 18);
        Tiles[0, Start] = 2;
        X = 0;
        Y = Start;
        Vector2 Dir = RandDir(X, Y);
    }

    Vector2 RandDir(int X, int Y)
    {
        while (true)
        {
            int Rand = Random.Range(0, 3);

            if(Rand == 0)
            {
                if(Y == 19)
                {

                }
                else if(Tiles[X, Y+1] == 0)
                {
                    return Vector2.up;
                }
            }

            if (Rand == 1)
            {
                if (X == 35)
                {

                }
                else if (Tiles[X + 1, Y] == 0)
                {
                    return Vector2.right;
                }
            }

            if (Rand == 2)
            {
                if (Y == 0)
                {

                }
                else if (Tiles[X, Y - 1] == 0)
                {
                    return Vector2.down;
                }
            }

            if (Rand == 3)
            {
                if (X == 0)
                {

                }
                else if (Tiles[X - 1, Y] == 0)
                {
                    return Vector2.left;
                }
            }
        }
    }
}
