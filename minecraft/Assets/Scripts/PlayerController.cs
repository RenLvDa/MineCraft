using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Renlvda.Util;
using Renlvda.Voxel;

public class PlayerController : MonoBehaviour
{

	//视线范围
	public int viewRange = 30;
	public Ray ray;

	void Awake(){
		ray = (Ray)Camera.main.ScreenPointToRay (Input.mousePosition);
	}

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

//		if (Input.GetMouseButton (0)) {
//			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
//			RaycastHit hitInfo;
//			if (Physics.Raycast (ray, out hitInfo, 200)) {
//				
//				Chunk oldChunk = hitInfo.collider.gameObject.GetComponent<Chunk> ();
//				Vector3int chunkPosition = new Vector3int (oldChunk.position);
//
//				Vector3int blockPosition = new Vector3int (hitInfo.point);
//				//blockPosition -= chunkPosition;
//				Debug.Log (blockPosition);
//
//				oldChunk.blocks[blockPosition.x,blockPosition.y,blockPosition.z] = 1;
//				oldChunk.ReFreshMesh ();
//			}	
//		}
	}
}
