using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockKill : MonoBehaviour {

    public HopperControls Player;

    static bool dead = false;

    private void Start()
    {
        dead = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && Player != null)
        {
            if(dead != true)
            {
                Player.SendMessage("BlockKillSFX");
            }
            dead = true;
            Player.speed = 0;
            HopperControls.CanJump = false;
            HopperControls.JumpCool = true;
            Player.GetComponent<SpriteRenderer>().color = new Color(100 / 255, 0, 0);
            Invoke("reload", 1);
        }
    }

    void reload()
    {
        Player.SendMessage("reload");
    }

}
