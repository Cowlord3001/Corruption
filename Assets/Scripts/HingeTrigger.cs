using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeTrigger : MonoBehaviour {

    HingeJoint2D MyJoint;

	// Use this for initialization
	void Start ()
    {
        MyJoint = gameObject.GetComponent<HingeJoint2D>() ;
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && MyJoint.useMotor != true)
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        else if (collision.gameObject.tag == "Player" && MyJoint.useMotor == true)
        {
            JointMotor2D x = MyJoint.motor;
            x.motorSpeed = 1000;

        }

    }
}
