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
    public AudioSource Jump;
    public AudioSource Death;
    public AudioSource End;
    public AudioSource ShieldShatter;

    bool Dead = false;

    bool ShieldOn;
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

        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            ShieldOn = true;
        }
        else
        {
            ShieldOn = false;
        }

        Boss1Attack.GreenHit = false;

    }
	
	// Update is called once per frame
	void Update ()
    {

        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space)) && CanJump == true && JumpCool == true && Dead == false)
        {
            Jump.Play();
            Mybody.velocity = Vector3.up * jumpheight;
            CanJump = false;
        }
        //New Stuff: Allows unlimited jump if in Stage 3
        else if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space)) && JumpCool == false && Dead == false)
        {
            Jump.Play();
            Mybody.velocity = Vector3.up * jumpheight;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Death.Play();
            GetComponent<SpriteRenderer>().color = new Color(100 / 255, 0, 0);
            reload();
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

        GameData.LevelTime += Time.deltaTime;
        GameData.TotalTime += Time.deltaTime;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Spike" && Playtesting_Mode == false && ShieldOn == false)
        {
            if(Dead != true)
            {
                Death.Play();
            }
            Dead = true;
            GetComponent<SpriteRenderer>().color = new Color(100/255, 0, 0);
            speed = 0;
            CanJump = false;
            JumpCool = true;
            Invoke("reload", 1);
        }

        else if (collision.gameObject.tag == "Spike" && Playtesting_Mode == false && ShieldOn == true)
        {
            Invoke("ShieldBreak", 1);
        }

        else if (collision.gameObject.tag == "Spike" && Playtesting_Mode == true)
        {

        }

        else if(collision.gameObject.tag == "Finish")
        {
            if (Dead != true)
            {
                End.Play();
            }
            Dead = true;
            speed = 0;
            CanJump = false;
            JumpCool = true;
            Invoke("NextStage", 1);
        }

        else if (collision.gameObject.tag == "Reflected")
        {
            End.Play();
            Boss1Attack.GreenHit = true;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-35, 0);
            collision.gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
        }

    }

    void BlockKillSFX()
    {
        Death.Play();
    }

    void ShieldBreak()
    {
        ShieldOn = false;
        if (transform.childCount != 1)
        {
            ShieldShatter.Play();
            Destroy(transform.GetChild(1).gameObject);
        }
    }

    void reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameData.TotalDeaths++;
        GameData.LevelDeaths++;
    }

    void NextStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameData.LevelDeaths = 0;
        GameData.LevelTime = 0;
        if(SceneManager.GetActiveScene().buildIndex != 1)
        {
            Persist.Song = false;
            Destroy(GameObject.Find("Music"));
        }
        
    }
}
