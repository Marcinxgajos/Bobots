using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFix : MonoBehaviour {

    AttackCubeShoter atCbShSc;

    Quaternion rotation;
    void Awake()
    {
        atCbShSc = GetComponent<AttackCubeShoter>();
        rotation = transform.rotation;
    }
    void LateUpdate()
    {
        transform.rotation = rotation *  Quaternion.Euler(0, 0, atCbShSc.degreeAngle);
    }
}
