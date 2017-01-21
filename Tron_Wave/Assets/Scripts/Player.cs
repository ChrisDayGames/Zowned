using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public string tag = "Bullet";
	public GameObject bullet; 
	public float speed = 5;
	public float rotationSpeed = 5;
	public float distancePerEmission = 1;

    /////////
	public string horizontalAxis;
	public string verticalAxis;

	public string aButton;
	public string bButton;

    public float attackPower;
    public float minAttack;
    public float maxAttack;

    public float growthRate = 5;

    public Transform spawnPosition;

    //public float minEmission;
    //public float maxEmission;

    /////////

	private List<GameObject> bullets = new List<GameObject> ();

	private LineRenderer line;

    private Rigidbody rb;

    public float spawnRate = 0.05f;
    float spawnTimer;

    public float hp;
    public float maxHp = 100;

    [Header ("Mana Bar")]
    public float mana;
    public float maxMana = 100;
    public float manaRechargeRate = 1;
    public float shotCost = 1;
    public float pickupRechargePercent = 30;
    public Bar manaBar;

	// Use this for initialization
	void Start () {
		line  = GetComponent <LineRenderer> ();
        rb = GetComponent<Rigidbody>();

        attackPower = minAttack;
        spawnTimer = Time.time;

        hp = maxHp;
        mana = maxMana / 2;
	}
	
	// Update is called once per frame
	void Update () {

		float xAxis = Input.GetAxisRaw(horizontalAxis);
		float yAxis = Input.GetAxisRaw(verticalAxis);

        Vector3 direction = Vector3.zero;

		if(xAxis > 0) {
            direction += new Vector3(1, 0, 0);
		}

		if(xAxis < 0) {
            direction += new Vector3(-1, 0, 0);
		}

		if(yAxis > 0) {
            direction += new Vector3(0, 1, 0);
		}

        if (yAxis < 0) {
            direction += new Vector3(0, -1, 0);
        }

        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);


        if (Input.GetButton(aButton)) {

            attackPower = maxAttack;
            Shoot();

        }

        if(Input.GetButton(bButton)) {

            attackPower = minAttack;
            Shoot();

        }



        RechargeMana();
        manaBar.SetSize (mana / maxMana);

        //UpdateLineRenderer();
	}

    public void Shoot () {

        if (Time.time > spawnTimer) {

            if (HasEnoughMana(shotCost)) {

                GameObject go = Instantiate<GameObject>(bullet, spawnPosition.position, transform.rotation);
                go.GetComponent<PingPongPosition>().SetAttack(attackPower);

                GameObject gg = Instantiate<GameObject>(bullet, spawnPosition.position, transform.rotation);
                gg.GetComponent<PingPongPosition>().SetAttack(attackPower);
                gg.GetComponent<PingPongPosition>().inverted = true;

                SpendMana(shotCost);
                spawnTimer = Time.time + spawnRate;

            }

        }

    }

    public void RechargeMana () {

        if (mana < maxMana) {
            mana += manaRechargeRate * Time.deltaTime;
        } else {
            mana = maxMana;
        }

    }

    public bool HasEnoughMana (float spendAmount) {

        if (mana >= spendAmount) {
            return true;
        }

        return false;

    }

    public void SpendMana (float spendAmount) {
        mana -= spendAmount;
    }
       
    public void DealDamage(float amount) {

        hp -= amount;

        if(hp < 0) {
            gameObject.SetActive(false);
        }

    }

	void UpdateLineRenderer () {

		Vector3[] bulletPositions = new Vector3[bullets.Count];

		for (int i = 0; i < bulletPositions.Length; i++) {
			bulletPositions [i] = bullets[i].transform.position;
		}

		line.numPositions = bullets.Count;
		line.SetPositions (bulletPositions);

	}

	void OnTriggerEnter (Collider other) {

		if (other.gameObject.tag == tag) {

            gameObject.SetActive(false);

            CameraShake2D.instance.ShakeCamera(0.1f, 0.5f, 1f);

        }

        if (other.gameObject.tag == "Recharge") {

            mana += pickupRechargePercent;
            if (mana > maxMana) {
                mana = maxMana;
            }

        }


    }

}
