using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCube : MonoBehaviour
{
    Mesh _mesh;
    // Start is called before the first frame update
    void Start()
    {
        _mesh = new Mesh();
        _mesh.vertices = new Vector3[] {
            new Vector3(0,0,0),
            new Vector3(1,0,0),
            new Vector3(0,1,0),
            new Vector3(1,1,0)
        };
        _mesh.triangles = new int[]
        {
            0,1,2,
            1,2,3,
            2,3,0,
            0,3,1
        };
        GetComponent<MeshFilter>().mesh = _mesh;
    }
}
