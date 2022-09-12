using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bai3 : MonoBehaviour
{
	[SerializeField] private GameObject plane;
	[SerializeField] Material material;
	[SerializeField] private LayerMask layerMask;
	Camera cam;
	[SerializeField]
	private GameObject cube;
	List<Vector2> vert2D = new List<Vector2>();
	const float scale = 10;
	[SerializeField] List<Vector2> uvs = new List<Vector2>();

	private void Start()
	{
		cam = Camera.main;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
			{
				var v3 = hit.point;
				var go = Instantiate(cube, v3, Quaternion.identity);
				var vrt = new Vector2(v3.x, v3.z);
				vert2D.Add(vrt);
			}
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Polygon();
			plane.SetActive(false);
		}
	}
	public void Polygon()
	{
		var vertices2D = vert2D.ToArray();
		// Use the triangulator to get indices for creating triangles
		Triangulator tr = new Triangulator(vertices2D);
		int[] indices = tr.Triangulate();

		// Create the Vector3 vertices
		Vector3[] vertices = new Vector3[vertices2D.Length];
		for (int i = 0; i < vertices.Length; i++)
		{
			vertices[i] = new Vector3(vertices2D[i].x, 0.5f, vertices2D[i].y);
		}
		// Create the mesh
		Mesh msh = new Mesh();
		msh.vertices = vertices;
		msh.triangles = indices;
		for (int i = 0; i < vertices.Length; i++)
		{
			var uv = new Vector2(vertices[i].x / scale, vertices[i].z / scale);
			uvs.Add(uv);
		}
		msh.uv = uvs.ToArray();
		msh.RecalculateNormals();
		msh.RecalculateBounds();

		// Set up game object with mesh;
		gameObject.AddComponent(typeof(MeshRenderer));
		MeshFilter filter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
		filter.mesh = msh;
		var meshRendere = GetComponent<MeshRenderer>();
		meshRendere.material = material;
	}
}


