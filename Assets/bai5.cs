using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bai5 : MonoBehaviour
{
    public MeshFilter meshFilter;
    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[] {
            new Vector3(0,0,0),
            new Vector3(1,0,0),
            new Vector3(0,1,0),
            new Vector3(1,1,0)
        };
        mesh.triangles = new int[]
        {
            0,1,2,
            1,2,3,
            2,3,0,
            0,3,1
        };
        meshFilter.mesh = mesh;
    }

    
}
