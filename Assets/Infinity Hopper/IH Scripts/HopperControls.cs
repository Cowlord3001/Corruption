using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HopperControls : MonoBehaviour {

    public float jumpheight;
    public float speed;
    public static bool CanJump;
    //New Bool
    public static bool JumpCool;
    Rigidbody2D Mybody;

    public bool Playtesting_Mode;

	// Use this for initialization
	void Start ()
    {
        CanJump = true;
        Mybody = gameObject.GetComponent<Rigidbody2D>();

        //New Stuff: Sets JumpCool depending on stage
        if (SceneManager.GetActiveScene().buildIndex != 3 && SceneManager.GetActiveScene().buildIndex != 4 && SceneManager.GetActiveScene().buildIndex != 5)
        {
            JumpCool = true;
        }
        else
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

        if (collision.gameObject.tag == "Spike" && Playtesting_Mode == false)
        {
            speed = 0;
            CanJump = false;
            JumpCool = true;
            Invoke("reload", 1);
        }

        else if (collision.gameObject.tag == "Spike" && Playtesting_Mode == true)
        {

        }

        else if(collision.gameObject.tag == "Finish")
        {
            speed = 0;
            CanJump = false;
            JumpCool = true;
            Invoke("NextStage", 1);
        }

        else if (collision.gameObject.tag == "Reflected")
        {
            Boss1Attack.GreenHit = true;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-23, 0);
            collision.gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
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
