using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Y)) {
			Application.LoadLevel (0);
		}

		if(Input.GetButtonDown("p1ButtonStart") || Input.GetButtonDown("p2ButtonStart")) {
			Application.LoadLevel (0);
		}
		
	}
}
