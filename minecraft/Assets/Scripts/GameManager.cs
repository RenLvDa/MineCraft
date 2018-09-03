using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {

	public static int randomSeed;

	void Awake(){
		TimeSpan timeSpan = DateTime.UtcNow - new DateTime (1970, 1, 1, 0, 0, 0, 0);
		randomSeed = (int)timeSpan.TotalSeconds;
	}
}
