using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Renlvda.Util;

namespace Renlvda.Voxel
{
	public class Map : MonoBehaviour
	{
		public static Map Instance;

		public static GameObject chunkPrefab;

		public Dictionary<Vector3int,GameObject> chunks = new Dictionary<Vector3int, GameObject> ();

		//当时是否正在生产chunk
		private bool spawningChunk = false;

		void Awake(){
			Instance = this;
			chunkPrefab = Resources.Load ("Prefab/Chunk") as GameObject;
		}
			
		//生成Chunk
		public void CreateChunk(Vector3int pos){
			if (spawningChunk)
				return;
			StartCoroutine (SpawnChunk (pos));
		}

		public IEnumerator SpawnChunk(Vector3int pos){
			spawningChunk = true;
			Instantiate (chunkPrefab, pos, Quaternion.identity);
			yield return 0;
			spawningChunk = false;
		}

		//通过Chunk的坐标来判断它是否存在
		public bool ChunkExists(Vector3int woeldPosition){
			return this.ChunkExists (woeldPosition.x, woeldPosition.y, woeldPosition.z);
		}

		//通过Chunk的坐标来判断它是否存在
		public bool ChunkExists(int x, int y, int z){
			return chunks.ContainsKey (new Vector3int (x, y, z));
		}
			
	}
}
