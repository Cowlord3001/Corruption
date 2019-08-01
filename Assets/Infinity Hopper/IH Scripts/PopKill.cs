using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopKill : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
