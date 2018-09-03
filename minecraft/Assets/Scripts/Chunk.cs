using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Renlvda.Util;

namespace Renlvda.Voxel
{
	[RequireComponent (typeof(MeshFilter))]
	[RequireComponent (typeof(MeshRenderer))]
	[RequireComponent (typeof(MeshCollider))]
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

		//所有的uv信息
		private List<Vector2> uv = new List<Vector2> ();
		//uv贴图每行每列的宽度(0~1),这里我的贴图是32x32的，所以是1/32
		public static float textureOffset = 1 / 32f;
		//让uv稍微缩小一点，避免出现它旁边的贴图
		public static float shrinkSize = 0.001f;

		//当前chunk是否正在生成中
		private bool isWorking = false;

		void Start ()
		{
//			mesh = new Mesh ();
//
//			AddFrontFace ();
//			AddBackFace ();
//			AddLeftFace ();
//			AddRightFace ();
//			AddTopFace ();
//			AddBottomFace ();
//
//			mesh.vertices = vertices.ToArray ();
//			mesh.triangles = triangles.ToArray ();
//
//			mesh.RecalculateBounds ();
//			mesh.RecalculateNormals ();
//
//			GetComponent<MeshFilter> ().mesh = mesh;
			position = new Vector3int (this.transform.position);
			if (Map.Instance.chunks.ContainsKey (position)) {
				Destroy (this);
			} else {
				this.name = "(" + position.x + "," + position.y + "," + position.z + ")";

			}
		}

		void StartFunction ()
		{
			mesh = new Mesh ();
			mesh.name = "Chunk";

			StartCoroutine (CreateMap ());
		}

		IEnumerator CreateMap ()
		{
			while (!isWorking) {
				yield return null;
			}
			isWorking = true;
			blocks = new byte[width, height, width];
			for (int x = 0; x < Chunk.width; x++) {
				for (int y = 0; y < Chunk.height; y++) {
					for (int z = 0; z < Chunk.width; z++) {
						blocks [x, y, z] = 1;
					}
				}
			}
				
			StartCoroutine (CreateMesh ());
		}


		IEnumerator CreateMesh ()
		{
			vertices.Clear ();
			triangles.Clear ();

			//把所有面的点和面的索引添加进去
			for (int x = 0; x < Chunk.width; x++) {
				for (int y = 0; y < Chunk.height; y++) {
					for (int z = 0; z < Chunk.width; z++) {
						//获取当前坐标的block对象
						Block block = BlockList.GetBlock (this.blocks [x, y, z]);
						if (block == null)
							continue;
						
						if (IsBlockTransparent (x + 1, y, z)) {
							AddFrontFace (x, y, z, block);
						}
						if (IsBlockTransparent (x - 1, y, z)) {
							AddBackFace (x, y, z, block);
						}
						if (IsBlockTransparent (x, y, z + 1)) {
							AddRightFace (x, y, z, block);
						}
						if (IsBlockTransparent (x, y, z - 1)) {
							AddLeftFace (x, y, z, block);
						}
						if (IsBlockTransparent (x, y + 1, z)) {
							AddTopFace (x, y, z, block);
						}
						if (IsBlockTransparent (x, y - 1, z)) {
							AddBottomFace (x, y, z, block);
						}
					}
				}
			}

			//为点和index赋值
			mesh.vertices = vertices.ToArray ();
			mesh.triangles = triangles.ToArray ();
			mesh.uv = uv.ToArray ();

			//重新计算顶点和法线
			mesh.RecalculateBounds ();
			mesh.RecalculateNormals ();

			//将生成好的面赋值给组件
			this.GetComponent<MeshFilter> ().mesh = mesh;
			this.GetComponent<MeshCollider> ().sharedMesh = mesh;

			yield return null;
			isWorking = false;
		}


		public static bool IsBlockTransparent (int x, int y, int z)
		{
			if (x >= width || y >= height || z >= width || x < 0 || y < 0 || z < 0) {
				return true;
			}
			return false;
		}


		//前面
		void AddFrontFace (int x, int y, int z, Block block)
		{
			//第一个三角面
			triangles.Add (0 + vertices.Count);
			triangles.Add (3 + vertices.Count);
			triangles.Add (2 + vertices.Count);

			//第二个三角面
			triangles.Add (2 + vertices.Count);
			triangles.Add (1 + vertices.Count);
			triangles.Add (0 + vertices.Count);

			//添加4个点
			vertices.Add (new Vector3 (0 + x, 0 + y, 0 + z));
			vertices.Add (new Vector3 (0 + x, 0 + y, 1 + z));
			vertices.Add (new Vector3 (0 + x, 1 + y, 1 + z));
			vertices.Add (new Vector3 (0 + x, 1 + y, 0 + z));

			//添加uv坐标点，跟上面4个点循环的顺序一致
			uv.Add (new Vector2 (block.textureFrontX * textureOffset, 
				block.textureFrontY * textureOffset)
				+ new Vector2 (shrinkSize, shrinkSize));
			uv.Add (new Vector2 (block.textureFrontX * textureOffset + textureOffset, 
				block.textureFrontY * textureOffset) 
				+ new Vector2 (-shrinkSize, shrinkSize));
			uv.Add (new Vector2 (block.textureFrontX * textureOffset + textureOffset, 
				block.textureFrontY *	textureOffset + textureOffset) 
				+ new Vector2 (-shrinkSize, -shrinkSize));
			uv.Add (new Vector2 (block.textureFrontX * textureOffset, 
				block.textureFrontY * textureOffset + textureOffset) 
				+ new Vector2 (shrinkSize, -shrinkSize));
		}

		//背面
		void AddBackFace (int x, int y, int z, Block block)
		{
			triangles.Add (0 + vertices.Count);
			triangles.Add (3 + vertices.Count);
			triangles.Add (2 + vertices.Count);

			triangles.Add (2 + vertices.Count);
			triangles.Add (1 + vertices.Count);
			triangles.Add (0 + vertices.Count);

			vertices.Add (new Vector3 (-1 + x, 0 + y, 1 + z));
			vertices.Add (new Vector3 (-1 + x, 0 + y, 0 + z));
			vertices.Add (new Vector3 (-1 + x, 1 + y, 0 + z));
			vertices.Add (new Vector3 (-1 + x, 1 + y, 1 + z));

		}

		void AddLeftFace (int x, int y, int z, Block block)
		{
			triangles.Add (0 + vertices.Count);
			triangles.Add (3 + vertices.Count);
			triangles.Add (2 + vertices.Count);

			triangles.Add (2 + vertices.Count);
			triangles.Add (1 + vertices.Count);
			triangles.Add (0 + vertices.Count);

			vertices.Add (new Vector3 (-1 + x, 0 + y, 0 + z));
			vertices.Add (new Vector3 (0 + x, 0 + y, 0 + z));
			vertices.Add (new Vector3 (0 + x, 1 + y, 0 + z));
			vertices.Add (new Vector3 (-1 + x, 1 + y, 0 + z));

		}

		void AddRightFace (int x, int y, int z, Block block)
		{
			triangles.Add (0 + vertices.Count);
			triangles.Add (3 + vertices.Count);
			triangles.Add (2 + vertices.Count);

			triangles.Add (2 + vertices.Count);
			triangles.Add (1 + vertices.Count);
			triangles.Add (0 + vertices.Count);

			vertices.Add (new Vector3 (0 + x, 0 + y, 1 + z));
			vertices.Add (new Vector3 (-1 + x, 0 + y, 1 + z));
			vertices.Add (new Vector3 (-1 + x, 1 + y, 1 + z));
			vertices.Add (new Vector3 (0 + x, 1 + y, 1 + z));

		}

		void AddTopFace (int x, int y, int z, Block block)
		{
			triangles.Add (0 + vertices.Count);
			triangles.Add (3 + vertices.Count);
			triangles.Add (2 + vertices.Count);

			triangles.Add (2 + vertices.Count);
			triangles.Add (1 + vertices.Count);
			triangles.Add (0 + vertices.Count);

			vertices.Add (new Vector3 (0 + x, 1 + y, 0 + z));
			vertices.Add (new Vector3 (0 + x, 1 + y, 1 + z));
			vertices.Add (new Vector3 (-1 + x, 1 + y, 1 + z));
			vertices.Add (new Vector3 (-1 + x, 1 + y, 0 + z));

		}

		void AddBottomFace (int x, int y, int z, Block block)
		{
			triangles.Add (0 + vertices.Count);
			triangles.Add (3 + vertices.Count);
			triangles.Add (2 + vertices.Count);

			triangles.Add (2 + vertices.Count);
			triangles.Add (1 + vertices.Count);
			triangles.Add (0 + vertices.Count);

			vertices.Add (new Vector3 (-1 + x, 0 + y, 0 + z));
			vertices.Add (new Vector3 (-1 + x, 0 + y, 1 + z));
			vertices.Add (new Vector3 (0 + x, 0 + y, 1 + z));
			vertices.Add (new Vector3 (0 + x, 0 + y, 0 + z));

		}
	}
}