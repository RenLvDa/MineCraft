using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockList : MonoBehaviour {

	public static Dictionary<byte,Block> blocks = new Dictionary<byte,Block> ();

	void Awake () {
		Block dirt = new Block (1, "Dirt", 2, 31);
		blocks.Add (dirt.id, dirt);
	}
	
	public static Block GetBlock(byte id){
		return blocks [id];
	}
}
