﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeOldPowerTorque : MonoBehaviour {

    Rigidbody2D rg;
    public Vector3[] points;
    public float torquePower;
    private Vector2 src;
    private Vector2 trg;
    // Use this for initialization
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        points = new Vector3[4];
        src = Vector2.zero;
        trg = Vector2.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(src, trg);
    }

    // Update is called once per frame
    void Update()
    {
        BoxCollider2D colli = GetComponent<BoxCollider2D>();
        BoxCollider2D collider = (BoxCollider2D)this.gameObject.GetComponent<Collider2D>();

        float top = collider.offset.y + (collider.size.y / 3f);
        float btm = collider.offset.y - (collider.size.y / 3f);
        float left = collider.offset.x - (collider.size.x / 3f);
        float right = collider.offset.x + (collider.size.x / 3f);

        points[0] = transform.TransformPoint(new Vector3(left, top, 0f));
        points[1] = transform.TransformPoint(new Vector3(right, top, 0f));
        points[2] = transform.TransformPoint(new Vector3(left, btm, 0f));
        points[3] = transform.TransformPoint(new Vector3(right, btm, 0f));
        //  points = colli.
        float AnglePower = Input.GetAxis("Horizontal");

        float xRel, yMax;
        yMax = -1000;
        xRel = (AnglePower > 0.3) ? 3000 : -3000;
        if (xRel == 3000)
        {
            for (int i = 0; i < 4; i++)
            {
                if (points[i].y > yMax)
                    yMax = points[i].y;
                if (points[i].x < xRel)
                    xRel = points[i].x;
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                if (points[i].y > yMax)
                    yMax = points[i].y;
                if (points[i].x > xRel)
                    xRel = points[i].x;
            }
        }

        if (Mathf.Abs(AnglePower) > 0.3 && rg != null)
        {
            //isShotting = true;
            // rg.AddTorque(AnglePower*torquePower);
            src = new Vector2(xRel, yMax);
            trg = src + Vector2.left * torquePower * AnglePower;
            rg.AddForceAtPosition(Vector2.left * torquePower * AnglePower, new Vector2(xRel, yMax));
            //    rg.AddForceAtPosition(Vector2.left * torquePower, (Vector2)transform.position + collider.offset);

        }
        // BoxCollider2D collider = (BoxCollider2D)gameObject.collider2D;





    }
}
