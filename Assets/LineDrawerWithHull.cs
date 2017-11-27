using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class LineDrawerWithHull : MonoBehaviour {




    public Transform []points;
    private MeshFilter mf;
    private Mesh mesh;
    public Vector3[] vertices;

    public void SetMeshBy2Points(float fillLenght)
    {
        ModifyMeshBy2Points(points[0].transform.localPosition, points[1].transform.localPosition,fillLenght);
    }

    public void ModifyMeshBy2Points(Vector2 v1,Vector2 v2,float fillLenght)
    {
        if (points.Length == 2)
        {
           // mf = GetComponent<MeshFilter>();
            mesh = mf.mesh;
            Vector3[] v;
            v = new Vector3[2];
            v[0] = new Vector3(v1.x, v1.y, 0);
          
            Vector3 vTmp = v2 - v1;
            vTmp *= fillLenght;
            v[1] = new Vector3(v1.x + vTmp.x,v1.y + vTmp.y,0);
            vertices = new Vector3[2 * 3];

            Vector2[] uvs = new Vector2[2 * 3];



            
                vertices[0] = v[0];
                vertices[1] = v[1];
                vertices[2] = vertices[0] - vertices[1];
                vertices[ 2] = Vector3RotateByZ(vertices[2], 90).normalized / 5;
                vertices[3] = vertices[2] + vertices[1];
                vertices[2] = vertices[2] + vertices[0];

                uvs[0] = new Vector2(0, fillLenght);
                uvs[1] = new Vector2(0, 0);
                uvs[2] = new Vector2(1, fillLenght);
                uvs[3] = new Vector2(1, 0);


            

            int[] triangles = new int[2 * 3];
     
                triangles[0] = 2;
                triangles[ 1] = 0;
                triangles[2] = 3;
                triangles[3] = 1;
                triangles[4] = 3;
                triangles[5] = 0;


            mesh.vertices = vertices;
            mesh.uv = uvs;
            mesh.triangles = triangles;
            UnityEditor.MeshUtility.Optimize(mesh);
            mesh.RecalculateNormals();
        }
        else
        {
            Debug.Log("you cant modify meshy by 2 points");
            return;
        }
    }

    public Vector3 Vector3RotateByZ(Vector3 vec, float _angle)
    {
        
        float _cos = Mathf.Cos(_angle * Mathf.Deg2Rad);
        float _sin = Mathf.Sin(_angle * Mathf.Deg2Rad);

        float _x2 = vec.x * _cos - vec.y * _sin;
        float _y2 = vec.x * _sin + vec.y * _cos;

        return new Vector3(_x2, _y2, vec.z);
    }

    // Use this for initialization
    void Start ()
    {
        if (Application.isPlaying)
        {
            mf = GetComponent<MeshFilter>();
            mesh = mf.mesh;
        }
        Vector2[] uvs;
        int[] triangles;
        if (points.Length == 2)
        {
            vertices = new Vector3[points.Length * 2];

            uvs = new Vector2[points.Length * 2];

            vertices[0] = points[0].position;
            vertices[1] = points[1].position;
            vertices[2] = vertices[0] - vertices[1];
            vertices[2] = Vector3RotateByZ(vertices[2], 90).normalized / 5;
            vertices[3] = vertices[2] + vertices[1];
            vertices[2] = vertices[2] + vertices[0];

            uvs[0] = new Vector2(0, 1);
            uvs[1] = new Vector2(0, 0);
            uvs[2] = new Vector2(1, 1);
            uvs[3] = new Vector2(1, 0);




            triangles = new int[2 * 3];

            triangles[0] = 2;
            triangles[1] = 0;
            triangles[2] = 3;
            triangles[3] = 1;
            triangles[4] = 3;
            triangles[5] = 0;

        }
        else
        {


            vertices = new Vector3[points.Length * 4];

            uvs = new Vector2[points.Length * 4];
            for (int i = 0, j = 0; i < points.Length; i++, j += 4)
            {
                vertices[j] = points[i].position;
                vertices[j + 1] = points[(i + 1) % points.Length].position;
                vertices[j + 2] = vertices[j] - vertices[j + 1];
                vertices[j + 2] = Vector3RotateByZ(vertices[j + 2], 90).normalized / 5;
                vertices[j + 3] = vertices[j + 2] + vertices[j + 1];
                vertices[j + 2] = vertices[j + 2] + vertices[j];

                uvs[j] = new Vector2(0, 1);
                uvs[j + 1] = new Vector2(0, 0);
                uvs[j + 2] = new Vector2(1, 1);
                uvs[j + 3] = new Vector2(1, 0);
            }

            triangles = new int[points.Length * 6];
            for (int i = 0, j = 0, k = 0; i < points.Length; i++, j += 4, k += 6)
            {
                triangles[k] = j + 2;
                triangles[k + 1] = j;
                triangles[k + 2] = j + 3;
                triangles[k + 3] = j + 1;
                triangles[k + 4] = j + 3;
                triangles[k + 5] = j;

            }

        }




        // mesh.Clear();
        if (Application.isPlaying)
        {
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;
            UnityEditor.MeshUtility.Optimize(mesh);
            mesh.RecalculateNormals();
        }




    }
	
	// Update is called once per frame
	void Update ()
    {


       // Debug.Log(Vector2.Dot(new Vector2(0.8f, -0.5f), new Vector2(-0.7f, 0.8f)));
    }

    void OnDrawGizmos()
    {
        /*
        if (Application.isPlaying && 1 == 2)
        {
            for (int j = 0; j < points.Length * 4; j += 4)
            {
                Gizmos.DrawLine(vertices[j], vertices[j + 1]);
                Gizmos.DrawLine(vertices[j + 1], vertices[j + 3]);
                Gizmos.DrawLine(vertices[j], vertices[j + 2]);
            }
        }
        */
    }

}
