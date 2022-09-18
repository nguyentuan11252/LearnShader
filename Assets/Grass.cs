using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    public GameObject objGrass;
    public Texture2D texture;
    public float size;
    public MeshFilter meshFilter;
    public List<Vector3> vert;
    public List<int> tris;
    public List<Color> col;
    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = new Mesh();
        float coeff = size / texture.height;
        var meshPref = objGrass.GetComponent<MeshFilter>().sharedMesh;
        var vertPref = meshPref.vertices;
        for (int x = 0; x < texture.height; x++)
        {
            for (int y = 0; y < texture.width; y++)
            {
                var pos = new Vector3(x + Random.Range(0.3f,2f), 0, y + Random.Range(0.3f, 0.7f)) * coeff;
                for (int i = 0; i < vertPref.Length; i++)
                {
                    var v = vertPref[i];
                    v += pos;
                    vert.Add(v);
                }

                var color = texture.GetPixel(x, y);
                col.Add(color);
            }
        }
        for (int i = 0; i < vert.Count - 4; i += 4)
        {
            tris.Add(i);
            tris.Add(i + 1);
            tris.Add(i + 2);
            tris.Add(i);
            tris.Add(i + 3);
            tris.Add(i + 2);
        }
        Color[] colorVert = new Color[vert.Count];
        for (int i = 0; i < colorVert.Length - 4; i += 4)
        {
            int y;
            if (i != 0)
                y = i / 4;
            else
                y = 0;
            colorVert[i] = col[y] * (0.2f + 0.8f * vert[i].y);
            colorVert[i + 1] = col[y] * (0.2f + 0.8f * vert[i + 1].y);
            colorVert[i + 2] = col[y] * (0.2f + 0.8f * vert[i + 2].y);
            colorVert[i + 3] = col[y] * (0.2f + 0.8f * vert[i + 3].y);
        }
        Vector2[] uv = new Vector2[vert.Count];
        for (int i = 0; i < uv.Length; i++)
        {
            uv[i] = new Vector2(vert[i].x+ Random.Range(-0.5f,0.5f)  * vert[i].y, vert[i].z+ Random.Range(-0.5f, 0.5f) * vert[i].y);
        }
        mesh.vertices = vert.ToArray();
        mesh.colors = colorVert;
        mesh.triangles = tris.ToArray();
        mesh.uv = uv;
        meshFilter.sharedMesh = mesh;
    }
}


