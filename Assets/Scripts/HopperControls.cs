﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HopperControls : MonoBehaviour {

    public float jumpheight;
    public float speed;
    public static bool CanJump;
    //New Bool
    bool JumpCool;
    Rigidbody2D Mybody;

	// Use this for initialization
	void Start ()
    {
        CanJump = true;
        Mybody = gameObject.GetComponent<Rigidbody2D>();

        //New Stuff: Sets JumpCool depending on stage
        if (SceneManager.GetActiveScene().buildIndex != 3)
        {
            JumpCool = true;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            JumpCool = false;
        }

    }
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0) && CanJump == true && JumpCool == true)
        {
            Mybody.velocity = Vector3.up * jumpheight;
            CanJump = false;
        }
        //New Stuff: Allows unlimited jump if in Stage 3
        else if (Input.GetKeyDown(KeyCode.Mouse0) && JumpCool == false)
        {
            Mybody.velocity = Vector3.up * jumpheight;
        }

        if(Mybody.velocity.y < 0)
        {
            Mybody.gravityScale = 2;
        }
        else
        {
            Mybody.gravityScale = 1;
        }

        Mybody.velocity = new Vector3(speed, Mybody.velocity.y, 0);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Spike")
        {
            speed = 0;
            CanJump = false;
            Invoke("reload", 1);
        }

        else if(collision.gameObject.tag == "Finish")
        {
            speed = 0;
            CanJump = false;
            Invoke("NextStage", 1);
        }

    }

    void reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void NextStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
