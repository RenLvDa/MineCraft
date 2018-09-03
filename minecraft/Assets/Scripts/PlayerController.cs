using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Renlvda.Util;
using Renlvda.Voxel;

public class PlayerController : MonoBehaviour {

	//视线范围
	public int viewRange = 30;

	void Update () {
		for (float x = transform.position.x - Chunk.width * 3; x < transform.position.x + Chunk.width * 3; x += Chunk.width) {
			for (float z = transform.position.z - Chunk.width * 3; z < transform.position.z + Chunk.width * 3; z += Chunk.width) {
				int xx = Chunk.width * Mathf.FloorToInt (x / Chunk.width);
				int zz = Chunk.width * Mathf.FloorToInt (z / Chunk.width);
				if (!Map.Instance.ChunkExists (xx, 0, zz)) {
					Map.Instance.CreateChunk (new Vector3int (xx, 0, zz));
				}
			}
		}
	}
}
