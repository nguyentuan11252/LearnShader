using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaiStack : MonoBehaviour
{
    public int numberMesh;
    public float r1, r2;
    public MeshFilter meshFilter;
    public List<Vector3> listVrtUpside;
    public List<Vector3> listVrtDownside;
    public List<Vector3> listVrtAllSide;
    public List<int> listTriangles;
    public List<Vector3> listNormal;
    public Vector3 center = Vector3.zero;
    public Vector3 centerA = Vector3.zero;
    int currentStep;
    [Range(0, 100)]
    public int render;
    public int currentrender;
    public GameObject cube;
    public GameObject endGame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (render >= numberMesh)
        {
            End();
            return;
        }

        if (Input.GetMouseButton(0))
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                render++;
                time = delayTime;
            }
        }
        else
        {
            if (render <= 0)
                return;
            time2 -= Time.deltaTime;
            if (time2 <= 0)
            {
                render--;
                time2 = delayTime;
            }
        }
        if (currentStep == numberMesh && render == currentrender)
            return;
        listVrtUpside.Clear();
        listVrtDownside.Clear();
        listVrtAllSide.Clear();
        listTriangles.Clear();
        listNormal.Clear();
        var mesh = new Mesh();
        #region Draw Underside
        for (int i = 0; i < numberMesh; i++)
        {
            var angle = i * (2 * Mathf.PI / numberMesh);
            float x = r1 * Mathf.Sin(angle);
            float y = r1 * Mathf.Cos(angle);
            var v3 = new Vector3(center.x + x, center.y, center.z + y);
            if (i <= render)
                listVrtUpside.Add(v3);
        }
        for (int i = 0; i < numberMesh; i++)
        {
            var angle = i * (2 * Mathf.PI / numberMesh);
            float x = r2 * Mathf.Sin(angle);
            float y = r2 * Mathf.Cos(angle);
            var v3 = new Vector3(center.x + x, center.y, center.z + y);
            if (i <= render)
                listVrtUpside.Add(v3);
        }
        for (int i = 0; i < listVrtUpside.Count / 2 - 1; i++)
        {
            int index0 = i;
            int index1 = i + listVrtUpside.Count / 2;
            int index2 = (i + 1) % (listVrtUpside.Count / 2);
            listTriangles.Add(index0);
            listTriangles.Add(index1);
            listTriangles.Add(index2);
            //Debug.Log(index0 + "  " + index1 + " " + index2);
        }

        for (int i = 1; i < listVrtUpside.Count / 2; i++)
        {
            int index0 = i;
            int index1 = (listVrtUpside.Count / 2) + ((listVrtUpside.Count / 2) + i - 1) % (listVrtUpside.Count / 2);
            int index2 = i + (listVrtUpside.Count / 2);
            listTriangles.Add(index0);
            listTriangles.Add(index1);
            listTriangles.Add(index2);
            //Debug.Log(index0 + "  " + index1 + " " + index2);
        }
        #endregion
        #region Draw Upperside
        for (int i = 0; i < numberMesh; i++)
        {
            var angle = i * (2 * Mathf.PI / numberMesh);
            float x = r1 * Mathf.Sin(angle);
            float y = r1 * Mathf.Cos(angle);
            var v3 = new Vector3(centerA.x + x, centerA.y, centerA.z + y);
            if (i <= render)
                listVrtDownside.Add(v3);
        }
        for (int i = 0; i < numberMesh; i++)
        {
            var angle = i * (2 * Mathf.PI / numberMesh);
            float x = r2 * Mathf.Sin(angle);
            float y = r2 * Mathf.Cos(angle);
            var v3 = new Vector3(centerA.x + x, centerA.y, centerA.z + y);
            if (i <= render)
                listVrtDownside.Add(v3);
        }
        for (int i = 0; i < listVrtDownside.Count / 2 - 1; i++)
        {
            int index0 = i + listVrtUpside.Count;
            int index2 = i + listVrtUpside.Count + listVrtDownside.Count / 2;
            int index1 = (i + 1) % (listVrtDownside.Count / 2) + listVrtUpside.Count;
            listTriangles.Add(index0);
            listTriangles.Add(index1);
            listTriangles.Add(index2);
            //Debug.Log(index0 + "  " + index1 + " " + index2);
        }
        for (int i = 1; i < listVrtDownside.Count / 2; i++)
        {
            int index0 = i + listVrtUpside.Count;
            int index1 = (listVrtDownside.Count / 2) + ((listVrtDownside.Count / 2) + i - 1) % (listVrtDownside.Count / 2) + listVrtUpside.Count;
            int index2 = i + (listVrtDownside.Count / 2) + listVrtUpside.Count;
            listTriangles.Add(index0);
            listTriangles.Add(index2);
            listTriangles.Add(index1);
            //Debug.Log(index0 + "  " + index1 + " " + index2);
        }
        #endregion
        listVrtAllSide.AddRange(listVrtUpside);
        listVrtAllSide.AddRange(listVrtDownside);
        #region normalUpandDown
        for (int i = 0; i < listVrtDownside.Count; i++)
        {
            listNormal.Add(new Vector3(0, -1, 0));
        }
        for (int i = listVrtUpside.Count; i < listVrtAllSide.Count; i++)
        {
            listNormal.Add(new Vector3(0, 1, 0));
        }
        #endregion
        #region Draw SideFace
        listVrtAllSide.AddRange(listVrtUpside);
        listVrtAllSide.AddRange(listVrtDownside);
        for (int i = 2 * listVrtDownside.Count; i < 2 * listVrtDownside.Count + listVrtUpside.Count / 2 - 1; i++)
        {
            int index0 = i;
            int index2 = i + listVrtUpside.Count;
            int index1 = i + 1;
            listTriangles.Add(index0);
            listTriangles.Add(index1);
            listTriangles.Add(index2);
        }
        for (int i = 2 * listVrtDownside.Count; i < 2 * listVrtDownside.Count + listVrtDownside.Count / 2 - 1; i++)
        {
            int index0 = i + listVrtUpside.Count;
            int index1 = i + 1;
            int index2 = i + 1 + listVrtUpside.Count;
            listTriangles.Add(index0);
            listTriangles.Add(index1);
            listTriangles.Add(index2);
        }
        for (int i = 2 * listVrtDownside.Count + listVrtUpside.Count / 2; i < 2 * listVrtDownside.Count + listVrtUpside.Count - 1; i++)
        {
            int index0 = i;
            int index1 = i + listVrtUpside.Count;
            int index2 = i + 1;
            listTriangles.Add(index0);
            listTriangles.Add(index1);
            listTriangles.Add(index2);
        }
        for (int i = 2 * listVrtDownside.Count + listVrtDownside.Count / 2; i < 2 * listVrtDownside.Count + listVrtDownside.Count - 1; i++)
        {
            int index0 = i + listVrtUpside.Count;
            int index2 = i + 1;
            int index1 = i + 1 + listVrtUpside.Count;
            listTriangles.Add(index0);
            listTriangles.Add(index1);
            listTriangles.Add(index2);
        }
        #endregion
        #region normalSide
        for (int i = listVrtAllSide.Count / 2; i < listVrtAllSide.Count / 2 + listVrtUpside.Count / 2; i++)
        {
            listNormal.Add((listVrtAllSide[i] - center).normalized);
        }
        for (int i = listVrtAllSide.Count / 2 + listVrtUpside.Count / 2; i < listVrtAllSide.Count - listVrtUpside.Count; i++)
        {
            listNormal.Add((center - listVrtAllSide[i]).normalized);
        }
        for (int i = listVrtAllSide.Count - listVrtDownside.Count; i < listVrtAllSide.Count - listVrtDownside.Count / 2; i++)
        {
            listNormal.Add((listVrtAllSide[i] - centerA).normalized);
        }
        for (int i = listVrtAllSide.Count - listVrtDownside.Count / 2; i < listVrtAllSide.Count; i++)
        {
            listNormal.Add((centerA - listVrtAllSide[i]).normalized);
        }
        #endregion
        #region Draw Complete
        if (render >= numberMesh)
        {
            listVrtAllSide.Add(listVrtAllSide[listVrtUpside.Count / 2 - 1 + listVrtUpside.Count]);
            listVrtAllSide.Add(listVrtAllSide[listVrtDownside.Count / 2 - 1]);
            listVrtAllSide.Add(listVrtAllSide[0]);

            listVrtAllSide.Add(listVrtAllSide[0]);
            listVrtAllSide.Add(listVrtAllSide[listVrtDownside.Count]);
            listVrtAllSide.Add(listVrtAllSide[listVrtUpside.Count / 2 - 1 + listVrtUpside.Count]);

            listVrtAllSide.Add(listVrtAllSide[listVrtUpside.Count - 1]);
            listVrtAllSide.Add(listVrtAllSide[listVrtUpside.Count * 2 - 1]);
            listVrtAllSide.Add(listVrtAllSide[listVrtDownside.Count / 2]);

            listVrtAllSide.Add(listVrtAllSide[listVrtUpside.Count * 2 - 1]);
            listVrtAllSide.Add(listVrtAllSide[listVrtDownside.Count / 2 + listVrtUpside.Count]);
            listVrtAllSide.Add(listVrtAllSide[listVrtDownside.Count / 2]);

            listTriangles.Add(listVrtAllSide.Count - 12);
            listTriangles.Add(listVrtAllSide.Count - 11);
            listTriangles.Add(listVrtAllSide.Count - 10);

            listTriangles.Add(listVrtAllSide.Count - 9);
            listTriangles.Add(listVrtAllSide.Count - 8);
            listTriangles.Add(listVrtAllSide.Count - 7);

            listTriangles.Add(listVrtAllSide.Count - 6);
            listTriangles.Add(listVrtAllSide.Count - 5);
            listTriangles.Add(listVrtAllSide.Count - 4);

            listTriangles.Add(listVrtAllSide.Count - 3);
            listTriangles.Add(listVrtAllSide.Count - 2);
            listTriangles.Add(listVrtAllSide.Count - 1);


            //listTriangles.Add(listVrtAllSide.Count - 1);
            //listTriangles.Add(listVrtDownside.Count / 2 + listVrtUpside.Count);
            //listTriangles.Add(listVrtDownside.Count / 2);

            int index0 = listVrtUpside.Count;
            int index1 = (listVrtDownside.Count / 2) + ((listVrtDownside.Count / 2) - 1) % (listVrtDownside.Count / 2) + listVrtUpside.Count;
            int index2 = (listVrtDownside.Count / 2) + listVrtUpside.Count;
            listTriangles.Add(index0);
            listTriangles.Add(index2);
            listTriangles.Add(index1);

            int index3 = listVrtDownside.Count / 2 - 1 + listVrtUpside.Count;
            int index5 = listVrtDownside.Count / 2 - 1 + listVrtUpside.Count + listVrtDownside.Count / 2;
            int index4 = (listVrtDownside.Count / 2 - 1 + 1) % (listVrtDownside.Count / 2) + listVrtUpside.Count;
            listTriangles.Add(index3);
            listTriangles.Add(index4);
            listTriangles.Add(index5);

            int index6 = listVrtUpside.Count / 2 - 1;
            int index7 = listVrtUpside.Count / 2 - 1 + listVrtUpside.Count / 2;
            int index8 = (listVrtUpside.Count / 2 - 1 + 1) % (listVrtUpside.Count / 2);
            listTriangles.Add(index6);
            listTriangles.Add(index7);
            listTriangles.Add(index8);

            int index9 = 0;
            int index10 = (listVrtUpside.Count / 2) + ((listVrtUpside.Count / 2) + 0 - 1) % (listVrtUpside.Count / 2);
            int index11 = 0 + (listVrtUpside.Count / 2);
            listTriangles.Add(index9);
            listTriangles.Add(index10);
            listTriangles.Add(index11);
        }
        else
        {
            listVrtAllSide.Add(listVrtAllSide[0]);
            listVrtAllSide.Add(listVrtAllSide[listVrtUpside.Count]);
            listVrtAllSide.Add(listVrtAllSide[listVrtUpside.Count + listVrtDownside.Count / 2]);
            listVrtAllSide.Add(listVrtAllSide[0]);
            listVrtAllSide.Add(listVrtAllSide[listVrtUpside.Count + listVrtDownside.Count / 2]);
            listVrtAllSide.Add(listVrtAllSide[listVrtUpside.Count / 2]);
            listVrtAllSide.Add(listVrtAllSide[listVrtUpside.Count - 1]);
            listVrtAllSide.Add(listVrtAllSide[listVrtUpside.Count + listVrtDownside.Count - 1]);
            listVrtAllSide.Add(listVrtAllSide[listVrtUpside.Count + listVrtDownside.Count / 2 - 1]);
            listVrtAllSide.Add(listVrtAllSide[listVrtUpside.Count - 1]);
            listVrtAllSide.Add(listVrtAllSide[listVrtUpside.Count + listVrtDownside.Count / 2 - 1]);
            listVrtAllSide.Add(listVrtAllSide[listVrtUpside.Count / 2 - 1]);
            listTriangles.Add(listVrtAllSide.Count - 12);
            listTriangles.Add(listVrtAllSide.Count - 11);
            listTriangles.Add(listVrtAllSide.Count - 10);

            listTriangles.Add(listVrtAllSide.Count - 9);
            listTriangles.Add(listVrtAllSide.Count - 8);
            listTriangles.Add(listVrtAllSide.Count - 7);

            listTriangles.Add(listVrtAllSide.Count - 6);
            listTriangles.Add(listVrtAllSide.Count - 5);
            listTriangles.Add(listVrtAllSide.Count - 4);

            listTriangles.Add(listVrtAllSide.Count - 3);
            listTriangles.Add(listVrtAllSide.Count - 2);
            listTriangles.Add(listVrtAllSide.Count - 1);
            for (int i = listVrtAllSide.Count - 12; i < listVrtAllSide.Count; i++)
            {
                listNormal.Add(Vector3.up);
            }
        }
        #endregion
        mesh.vertices = listVrtAllSide.ToArray();
        mesh.triangles = listTriangles.ToArray();
        //mesh.normals = listNormal.ToArray();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        meshFilter.mesh = mesh;
        currentStep = numberMesh;
        currentrender = render;
    }
    const float delayTime = .1f;
    float time = delayTime;
    float time2 = delayTime;
    public void End()
    {
       
    }
}
