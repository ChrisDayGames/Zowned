using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPickup : MonoBehaviour {

    public Vector2 minSpawnPos;
    public Vector2 maxSpawnPos;
    public float spawnWait = 1;

    float spawnTimer;

    public Vector3 originalSpawnPosition;

    bool dontRespawn = false;

    public GameObject collectionEffect;

    // Use this for initialization
    void Start () {

        spawnTimer = Time.time;

    }

    public void Respawn () {

        if(!dontRespawn) {

            float x = Random.Range(minSpawnPos.x, maxSpawnPos.x);
            float y = Random.Range(minSpawnPos.y, maxSpawnPos.y);

            transform.position = new Vector3(x, y, 0);

        }
        
    }

    void AnimateCollection() {

        GameObject effect = Instantiate(collectionEffect, transform.position, Quaternion.identity) as GameObject;

    }

    void OnTriggerEnter (Collider other) {

        if (other.gameObject.tag == "Player") {

            AnimateCollection();

            dontRespawn = false;

            transform.position = new Vector3(1000, 1000, 1000);
            Invoke("Respawn", spawnWait);

        }

    }

    public void ResetValues() {

        transform.position = originalSpawnPosition;
        dontRespawn = true;

    }

}
