using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKiller : MonoBehaviour {

    public float xLimit = 15f;
	
	void Update () {

        if (transform.position.x > xLimit || transform.position.x < -xLimit)
            Destroy(gameObject);

	}

}
