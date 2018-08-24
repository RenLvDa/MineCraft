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

		private bool spawningChunk = false;

		void Awake(){
			Instance = this;
			chunkPrefab = Resources.Load ("Prefab/Chunk") as GameObject;
		}

		void Start(){
			StartCoroutine (SpawnChunk (new Vector3int (0, 0, 0)));
		}

		public IEnumerator SpawnChunk(Vector3int pos){
			spawningChunk = true;
			Instantiate (chunkPrefab, pos, Quaternion.identity);
			yield return 0;
			spawningChunk = false;
		}
	}
}
