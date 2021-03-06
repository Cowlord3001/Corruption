﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrue : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            HopperControls.CanJump = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            HopperControls.CanJump = false;
        }
    }

}
