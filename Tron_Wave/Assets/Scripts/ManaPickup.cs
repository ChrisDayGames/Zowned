using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPickup : MonoBehaviour {

    public Vector2 minSpawnPos;
    public Vector2 maxSpawnPos;
    public float spawnWait = 1;

    // Use this for initialization
    void Start () {
        transform.position = new Vector3(1000, 1000, 1000);
        Invoke("Respawn", spawnWait);
    }

    public void Respawn () {

        float x = Random.Range(minSpawnPos.x, maxSpawnPos.x);
        float y = Random.Range(minSpawnPos.y, maxSpawnPos.y);

        transform.position = new Vector3(x, y, 0);
    }

    void OnTriggerEnter (Collider other) {

        if (other.gameObject.tag == "Player") {

            transform.position = new Vector3(1000, 1000, 1000);
            Invoke("Respawn", spawnWait);

        }

    }

}
