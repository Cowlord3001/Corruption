using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Attack : MonoBehaviour {

    public bool SpreadAttack;
    public GameObject SpreadProjectile;
    public float SpreadSpeed;
    public float SpreadAngle;
    public float SRoF;

    public bool TargetAttack;
    public GameObject TargetProjectile;
    public float TargetSpeed;
    public float TRoF;

    public bool GreenAttack;
    public GameObject GreenProjectile;
    public float GreenSpeed;
    public static bool GreenHit;

    Vector2 Dir;
    public GameObject Player;

    private void OnEnable()
    {
        if (SpreadAttack == true)
        {
            InvokeRepeating("Spread", 0, SRoF);
        }

        else if (TargetAttack == true)
        {
            InvokeRepeating("Target", 0, TRoF);
        }

        else if (GreenAttack == true)
        {
            InvokeRepeating("Green", 0, 8);
        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void Green()
    {
        if (GreenHit != true)
        {

            GameObject GO = Instantiate(GreenProjectile, transform.position, Quaternion.identity);
            GO.GetComponent<Rigidbody2D>().gravityScale = 0;
            GO.GetComponent<Rigidbody2D>().velocity = Vector3.right * GreenSpeed;
            Destroy(GO, 8);
        }
    }

    //private void Start()
    //{
    //    if (SpreadAttack == true)
    //    {
    //        InvokeRepeating("Spread", 0, SRoF);
    //    }

    //    else if (TargetAttack == true)
    //    {
    //        InvokeRepeating("Target", 0, TRoF);
    //    }
    //}


    // Update is called once per frame
    void Update () {
		
	}


    void Spread()
    {
        Dir = Player.transform.position - transform.position;

        for (int i = 0; i < 3; i++)
        {
            GameObject GO = Instantiate(SpreadProjectile, transform.position, Quaternion.identity * Quaternion.FromToRotation(Vector3.up, Dir)*Quaternion.Euler(0,0,SpreadAngle * i));
            GO.transform.position += GO.transform.up.normalized * 2.5f;
            GO.GetComponent<Rigidbody2D>().gravityScale = 0;
            GO.GetComponent<Rigidbody2D>().velocity = GO.transform.up.normalized * SpreadSpeed;
            Destroy(GO, 12);

            if (i != 0)
            {
                GO = Instantiate(SpreadProjectile, transform.position, Quaternion.identity * Quaternion.FromToRotation(Vector3.up, Dir) * Quaternion.Euler(0, 0, SpreadAngle * i * -1));
                GO.transform.position += GO.transform.up.normalized * 2.5f;
                GO.GetComponent<Rigidbody2D>().gravityScale = 0;
                GO.GetComponent<Rigidbody2D>().velocity = GO.transform.up.normalized * SpreadSpeed;
                Destroy(GO, 12);
            }
        }
        
    }

    void SpreadSpawn()
    {

    }

    void Target()
    {
        Dir = Player.transform.position - transform.position;
        Invoke("TargetSpawn", 0);
        Invoke("TargetSpawn", .1f);
        Invoke("TargetSpawn", .2f);
    }

    void TargetSpawn()
    {
        GameObject GO = Instantiate(TargetProjectile, transform.position, Quaternion.identity*Quaternion.FromToRotation(Vector3.up, Dir));
        GO.GetComponent<Rigidbody2D>().gravityScale = 0;
        GO.GetComponent<Rigidbody2D>().velocity = Dir.normalized * TargetSpeed;
        Destroy(GO, 12);
    }
}
