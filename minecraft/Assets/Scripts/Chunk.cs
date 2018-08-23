using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Renlvda.Util;

namespace Renlvda.Voxel
{
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	[RequireComponent(typeof(MeshCollider))]
	public class Chunk : MonoBehaviour
	{
		public static int width = 16;
		public static int height = 16;

		public byte[,,] blocks;
		public Vector3int position;

		private Mesh mesh;

		//面需要的点
		private List<Vector3> vertices = new List<Vector3> ();
		//生成三边面时用到的vertices的index
		private List<int> triangles = new List<int> ();

		//当前chunk是否正在生成中
		private bool isWorking = false;

		void Start ()
		{
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

			Renlvda.Util.Vector3int a = new Renlvda.Util.Vector3int ();
			Debug.Log (a.x + " " + a.y + " " + a.z);
		}

		void AddFrontFace ()
		{
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

		void AddBackFace ()
		{
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

		void AddLeftFace ()
		{
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

		void AddRightFace ()
		{
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

		void AddTopFace ()
		{
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

		void AddBottomFace ()
		{
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
}