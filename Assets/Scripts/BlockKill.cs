using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockKill : MonoBehaviour {

    public HopperControls Player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
                Player.speed = 0;
                HopperControls.CanJump = false;
                Invoke("reload", 1);
            
        }
    }

    void reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
