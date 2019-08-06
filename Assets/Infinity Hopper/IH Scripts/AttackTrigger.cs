using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    public GameObject[] Attacks;
    GameObject GreenAttackNode;
    
    
    void Attack()
    {
        int x = Random.Range(0, 4);
        int y = Random.Range(0, 4);
        while (y == x)
        {
            y = Random.Range(0, 4);
        }

        for (int i = 0; i < 4; i++)
        {
            Attacks[i].SetActive(false);
        }

        Attacks[x].SetActive(true);
        Attacks[y].SetActive(true);
    }

    void GreenAttack()
    {
        GreenAttackNode.SetActive(true);
    }

    private void OnDisable()
    {
        CancelInvoke();
        if (Attacks.Length == 4)
        {
            for (int i = 0; i < 4; i++)
            {
                Attacks[i].SetActive(false);
            }
            GreenAttackNode.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if(Attacks.Length == 4)
        {
            for (int i = 0; i < 4; i++)
            {
                Attacks[i].SetActive(false);
            }
            GreenAttackNode.SetActive(false);
        }
    }

    // Use this for initialization
    void Start ()
    {
        Attacks = GameObject.FindGameObjectsWithTag("AttackNode");
        GreenAttackNode = GameObject.Find("GreenAttackNode");
        for (int i = 0; i < 4; i++)
        {
            Attacks[i].SetActive(false);
        }
        GreenAttackNode.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            InvokeRepeating("Attack", 0, 5);
            Invoke("GreenAttack", 20);
        }
    }
}
