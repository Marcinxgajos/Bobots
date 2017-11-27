using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCubeShoter : MonoBehaviour {

    public float degreeAngle;

    public int typeOfWeapon;

    public Transform tr;

    public Transform navigatorTr;

    public float maximumPower;

    public float powerMultiplier;

    float prevPowerMultiplier;

    float power;
    float prevPower;
    int pressPowerCntr;

    float angleOfFire;

    private IEnumerator coroutine;

    private LineDrawerWithHull ldwhSc;

    bool isShotting;

    public Vector2 trgP;

    private void Awake()
    {
        GameObject variableForPrefab = (GameObject)Resources.Load("prefabs/AttackGre", typeof(GameObject));
        //  explosTr = GameObject.Find("Explosion").transform;
        tr = variableForPrefab.transform;
        Transform childWithDrawHull;
        childWithDrawHull = transform.GetChild(1);
        ldwhSc = childWithDrawHull.GetComponent<LineDrawerWithHull>();
        if(ldwhSc == null)
        {
            Debug.LogWarning("ldwhSc is null in" + transform.name);
        }
        power = 0;
        pressPowerCntr = 0;
        prevPowerMultiplier = 0;
        powerMultiplier = 0;
    }
    // Use this for initialization
    void Start ()
    {

        isShotting = false;
        coroutine = Fire(0.5f);
    }
    private IEnumerator Fire(float waitTime)
    {
            isShotting = true;
             Transform tmp;
            Vector2 trg;
           trgP =  trg = LineFuctions.RotateVecPFromI(Vector2.zero, degreeAngle, Vector2.up);
         //   Rigidbody2D rg = tr.GetComponent<Rigidbody2D>();
          //  trg = trg *powerMultiplier;
        //    rg.velocity = trg;
       // rg.velocity = new Vector2(trg.x * powerMultiplier, trg.y * powerMultiplier);
            tmp = Instantiate(tr, this.transform.position + new Vector3(trg.x*2, trg.y*2), this.transform.rotation, null);
        //  tmp.GetComponent<Rigidbody2D>().velocity = new Vector2(trg.x * powerMultiplier, trg.y * powerMultiplier);
        tmp.GetComponent<Rigidbody2D>().velocity = new Vector2(trg.x * powerMultiplier, trg.y * powerMultiplier);
        yield return new WaitForSeconds(waitTime);



            isShotting = false;
        prevPowerMultiplier = powerMultiplier;
        powerMultiplier = 0;
        navigatorTr.GetComponent<ControlNavigator>().ChangeBobot();
            
           
            //  print("WaitAndPrint " + Time.time);
        
    }
 
	
	// Update is called once per frame
	void Update ()
    {
        prevPower = power;
        power = Input.GetAxis("Fire1");

        if (powerMultiplier == 0)
        {
            ldwhSc.SetMeshBy2Points(prevPowerMultiplier / maximumPower);
        }
        else
        {
            ldwhSc.SetMeshBy2Points(powerMultiplier / maximumPower);
        }


        if(power > 0.1f && !isShotting)
        {
            powerMultiplier += power/5;
            if(powerMultiplier > maximumPower)
            {
                StartCoroutine(Fire(1.0f));
                prevPower = 0;
                power = 0;
                pressPowerCntr = 0;
            }
            //isShotting = true;
           // StartCoroutine(Fire(1.0f));

        }

        if(prevPower > power && !isShotting) //this means you release power
        {
            StartCoroutine(Fire(1.0f));
            prevPower = 0;
            power = 0;
            pressPowerCntr = 0;
        }

        angleOfFire = Input.GetAxis("HorizontalWeaponAngle");
        degreeAngle += angleOfFire;


    }
}
