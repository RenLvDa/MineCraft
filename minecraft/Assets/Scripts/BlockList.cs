using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockList : MonoBehaviour {

	public static Dictionary<byte,Block> blocks = new Dictionary<byte,Block> ();

	void Awake () {
		Block dirt = new Block (1, "Dirt", 2, 31);
		blocks.Add (dirt.id, dirt);

		Block grass = new Block (2, "Grass", 3, 31, 0, 31, 2, 31);
		blocks.Add (grass.id, grass);
	}
	
	public static Block GetBlock(byte id){
		return blocks.ContainsKey (id) ? blocks [id] : null;
	}
}
