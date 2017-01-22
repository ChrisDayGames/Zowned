using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public string tag = "Bullet";
	public GameObject bullet; 
	public float speed = 5;
	public float rotationSpeed = 5;
	public float distancePerEmission = 1;

    /////////
    public MenuNavigation menuNav;

	public string horizontalAxis;
	public string verticalAxis;

	public string aButton;
	public string bButton;

    public float attackPower;
    public float minAttack;
    public float maxAttack;

    public float growthRate = 5;

    public Transform spawnPosition;

    public Vector3 originalSpawnPosition;

	private Animator _animator;

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

    public Text p1ScoreText;
    public Text p2ScoreText;

    [Header ("Mana Bar")]
    public float mana;
    public float maxMana = 100;
    public float manaRechargeRate = 1;
    public float shotCost = 1;
    public float pickupRechargePercent = 30;

    public Bar vignette;
    public Bar manaBar;

    // Use this for initialization
    void Start () {
		line  = GetComponent <LineRenderer> ();
        rb = GetComponent<Rigidbody>();

        attackPower = minAttack;
        spawnTimer = Time.time;

        hp = maxHp;
        mana = 0;
		_animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

        if (GameController.state != "playing")
            return;

		float xAxis = Input.GetAxisRaw(horizontalAxis);
		float yAxis = Input.GetAxisRaw(verticalAxis);

        Vector3 direction = Vector3.zero;

		if(xAxis > 0) {
            direction += new Vector3(1, 0, 0);
			if (!(Input.GetButton (aButton)) || !(Input.GetButton (bButton))) { 
				_animator.Play (Animator.StringToHash ("BMove"));
			}
		}

		if(xAxis < 0) {
            direction += new Vector3(-1, 0, 0);
			if (!(Input.GetButton (aButton)) || !(Input.GetButton (bButton))) { 
				_animator.Play (Animator.StringToHash ("BMove"));
			}

		}

		if(yAxis > 0) {
            direction += new Vector3(0, 1, 0);
			if (!(Input.GetButton (aButton)) || !(Input.GetButton (bButton))) { 
				_animator.Play (Animator.StringToHash ("BMove"));
			}

		}

        if (yAxis < 0) {
            direction += new Vector3(0, -1, 0);
			if (!(Input.GetButton (aButton)) || !(Input.GetButton (bButton))) { 
				_animator.Play (Animator.StringToHash ("BMove"));
			}

        }


        rb.MovePosition(transform.position + direction.normalized * speed * Time.deltaTime);


        if (Input.GetButton(aButton)) {

			_animator.Play( Animator.StringToHash( "BFire" ) );
            attackPower = maxAttack;
            Shoot();

        }

        if(Input.GetButton(bButton)) {

			_animator.Play( Animator.StringToHash( "BFire" ) );
            attackPower = minAttack;
            Shoot();

        }



        RechargeMana();
        //vignette.SetOpacity (mana / maxMana);
        manaBar.SetSize (mana / maxMana);

        //UpdateLineRenderer();
	}

    public void Shoot () {

        if (Time.time > spawnTimer) {

            if (HasEnoughMana(shotCost)) {

				GameObject go = Instantiate<GameObject>(bullet, spawnPosition.position, Quaternion.Euler(0,0,90f));
                go.GetComponent<PingPongPosition>().SetAttack(attackPower);

				GameObject gg = Instantiate<GameObject>(bullet, spawnPosition.position, Quaternion.Euler(0,0,90f));
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

    public void ResetValues() {

        transform.position = originalSpawnPosition;

        mana = 0;
        manaBar.SetSize(mana / maxMana);

    }

    void UpdateScoreUI() {

        p1ScoreText.text = GameController.p1Score.ToString();
        p2ScoreText.text = GameController.p2Score.ToString();

    }

    void ShowGameOverMenu() {

        GameController.state = "gameover";

        menuNav.ShowPopUp("GameOver Panel");

    }

	void OnTriggerEnter (Collider other) {

        if (GameController.state != "playing")
            return;

		if (other.gameObject.tag == tag) {

            gameObject.SetActive(false);

            CameraShake2D.instance.ShakeCamera(0.1f, 0.5f, 1f);

            GameController.state = "pregameover";
            
            if(tag == "Bullet") {
                GameController.p1Score++;
            } else if(tag == "Bullet2") {
                GameController.p2Score++;
            }

            UpdateScoreUI();

            Invoke("ShowGameOverMenu", 1f);

        }

        if (other.gameObject.tag == "Recharge") {

            mana += pickupRechargePercent;
            if (mana > maxMana) {
                mana = maxMana;
            }

        }

        if (other.gameObject.tag == "HalfCharge") {

            mana += pickupRechargePercent / 8;
            if (mana > maxMana) {
                mana = maxMana;
            }

        }

    }

}
