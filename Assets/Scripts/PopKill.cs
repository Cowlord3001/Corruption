using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopKill : MonoBehaviour {

    public HopperControls Player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
