using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chunk : MonoBehaviour {

	private Mesh mesh;

	private List<Vector3> vertices = new List<Vector3> ();

	private List<int> triangles = new List<int>();

	void Start () {
		mesh = new Mesh ();

		AddFrontFace ();
		AddBackFace ();
		AddLeftFace ();
		AddRightFace ();
		AddTopFace ();
		AddBottomFace ();

		mesh.vertices = vertices.ToArray ();
		mesh.triangles = triangles.ToArray ();

		mesh.RecalculateBounds ();
		mesh.RecalculateNormals ();

		GetComponent<MeshFilter> ().mesh = mesh;
	}

	void AddFrontFace(){
		triangles.Add (0 + vertices.Count);
		triangles.Add (3 + vertices.Count);
		triangles.Add (2 + vertices.Count);

		triangles.Add (2 + vertices.Count);
		triangles.Add (1 + vertices.Count);
		triangles.Add (0 + vertices.Count);

		vertices.Add (new Vector3 (0, 0, 0));
		vertices.Add (new Vector3 (0, 0, 1));
		vertices.Add (new Vector3 (0, 1, 1));
		vertices.Add (new Vector3 (0, 1, 0));

	}

	void AddBackFace(){
		triangles.Add (0 + vertices.Count);
		triangles.Add (3 + vertices.Count);
		triangles.Add (2 + vertices.Count);

		triangles.Add (2 + vertices.Count);
		triangles.Add (1 + vertices.Count);
		triangles.Add (0 + vertices.Count);

		vertices.Add (new Vector3 (-1, 0, 1));
		vertices.Add (new Vector3 (-1, 0, 0));
		vertices.Add (new Vector3 (-1, 1, 0));
		vertices.Add (new Vector3 (-1, 1, 1));

	}

	void AddLeftFace(){
		triangles.Add (0 + vertices.Count);
		triangles.Add (3 + vertices.Count);
		triangles.Add (2 + vertices.Count);

		triangles.Add (2 + vertices.Count);
		triangles.Add (1 + vertices.Count);
		triangles.Add (0 + vertices.Count);

		vertices.Add (new Vector3 (-1, 0, 0));
		vertices.Add (new Vector3 (0, 0, 0));
		vertices.Add (new Vector3 (0, 1, 0));
		vertices.Add (new Vector3 (-1, 1, 0));

	}

	void AddRightFace(){
		triangles.Add (0 + vertices.Count);
		triangles.Add (3 + vertices.Count);
		triangles.Add (2 + vertices.Count);

		triangles.Add (2 + vertices.Count);
		triangles.Add (1 + vertices.Count);
		triangles.Add (0 + vertices.Count);

		vertices.Add (new Vector3 (0, 0, 1));
		vertices.Add (new Vector3 (-1, 0, 1));
		vertices.Add (new Vector3 (-1, 1, 1));
		vertices.Add (new Vector3 (0, 1, 1));

	}

	void AddTopFace(){
		triangles.Add (0 + vertices.Count);
		triangles.Add (3 + vertices.Count);
		triangles.Add (2 + vertices.Count);

		triangles.Add (2 + vertices.Count);
		triangles.Add (1 + vertices.Count);
		triangles.Add (0 + vertices.Count);

		vertices.Add (new Vector3 (0, 1, 0));
		vertices.Add (new Vector3 (0, 1, 1));
		vertices.Add (new Vector3 (-1, 1, 1));
		vertices.Add (new Vector3 (-1, 1, 0));

	}

	void AddBottomFace(){
		triangles.Add (0 + vertices.Count);
		triangles.Add (3 + vertices.Count);
		triangles.Add (2 + vertices.Count);

		triangles.Add (2 + vertices.Count);
		triangles.Add (1 + vertices.Count);
		triangles.Add (0 + vertices.Count);

		vertices.Add (new Vector3 (-1, 0, 0));
		vertices.Add (new Vector3 (-1, 0, 1));
		vertices.Add (new Vector3 (0, 0, 1));
		vertices.Add (new Vector3 (0, 0, 0));

	}
}
