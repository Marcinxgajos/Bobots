using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMeshTo2Vecs : MonoBehaviour {

    public float barLenght;
    float prevBarLenght;

    LineDrawerWithHull ldwhSc;
	// Use this for initialization
	void Start ()
    {
       // barLenght = 0;
	}

    void Awake()
    {
        //barLenght = 0;
        prevBarLenght = 1;
        ldwhSc = GetComponent<LineDrawerWithHull>();
    }

    // Update is called once per frame
    void Update ()
    {
        
           // ldwhSc.SetMeshBy2Points(barLenght);
        
      //  ldwhSc.

	}
}
