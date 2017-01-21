using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongPosition : MonoBehaviour {

	public float forwardSpeed = 2;
	public float frequency = 1;
	public float amplitude = 1;
	public string tag = "Bullet";

	private Vector3 initialPos;
	private float initialTime;

    public float minSpeed;
    public float maxSpeed;

    public float minFrequency;
    public float maxFrequency;

    public float minAmplitude;
    public float maxAmplitude;

    public bool inverted = false;

    public float bulletDamage = 34f;

    // Use this for initialization
    void Start () {

		initialPos = transform.position;
		initialTime = Time.time;
		
	}
	
	// Update is called once per frame
	void Update () {

		float xOffset =  (-1 + Mathf.PingPong((initialTime + Time.time) * frequency, 2)) * amplitude;

        if (inverted)
            xOffset = -xOffset;

        transform.Translate (new Vector3 (xOffset * Time.deltaTime, forwardSpeed * Time.deltaTime, 0));

    }

    public void SetAttack(float attackPower) {

        frequency = minFrequency + (maxFrequency - minFrequency) * (attackPower / 5);

        amplitude = maxAmplitude - (maxAmplitude - minAmplitude) * (attackPower / 5);

        forwardSpeed = minSpeed + (maxSpeed - minSpeed) * (attackPower / 5);

    }

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Wall" || other.gameObject.tag == tag)
			gameObject.SetActive (false);

    }

}
