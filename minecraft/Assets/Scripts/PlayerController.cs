using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Renlvda.Util;
using Renlvda.Voxel;

public class PlayerController : MonoBehaviour
{

	//视线范围
	public int viewRange = 30;

	void Update ()
	{
		for (float x = transform.position.x - Chunk.width * 5; x < transform.position.x + Chunk.width * 5; x += Chunk.width) {
			for (float y = transform.position.y - Chunk.height * 2; y < transform.position.z + Chunk.height * 2; y += Chunk.height) {
				if (y <= Chunk.height * 16 && y > 0) {
					for (float z = transform.position.z - Chunk.width * 5; 
						z < transform.position.z + Chunk.width * 5; z += Chunk.width) {
						int xx = Chunk.width * Mathf.FloorToInt (x / Chunk.width);
						int yy = Chunk.height * Mathf.FloorToInt (y / Chunk.height);
						int zz = Chunk.width * Mathf.FloorToInt (z / Chunk.width);
						if (!Map.Instance.ChunkExists (xx, yy, zz)) {
							Map.Instance.CreateChunk (new Vector3int (xx, yy, zz));
						}
					}
				}
			}
		}
	}
}
