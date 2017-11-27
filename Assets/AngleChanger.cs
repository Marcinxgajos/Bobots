using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleChanger : MonoBehaviour {


    Vector2 trg;
    Vector2 src;
    public Transform crossHairTr;

	// Use this for initialization
	void Start ()
    {
        trg = Vector2.zero;
        src = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update ()
    {
        trg = crossHairTr.position;
        src = transform.position;
	}

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(trg, src);
    }
}
