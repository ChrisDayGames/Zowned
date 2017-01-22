using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static string state = "mainMenu";

    public MenuNavigation menuNav;

    public ManaPickup[] manaPickUps;
    public Player[] players;

    public GameObject[] walls;

    public static int p1Score = 0;
    public static int p2Score = 0;

	void Start () {
		
	}
	
	void Update () {

        if (state == "mainMenu") {

            if(Input.GetButtonDown("p1ButtonStart") || Input.GetButtonDown("p2ButtonStart")) {
                menuNav.GoToScreen("Controls Menu");
                state = "startMenu";
            }
                
        } else if (state == "startMenu") {

            if (Input.GetButtonDown("p1ButtonStart") || Input.GetButtonDown("p2ButtonStart")) {
                menuNav.StartGame();
                state = "playing";
            }

        } else if (state == "playing") {

        } else if (state == "gameover") {

            if (Input.GetButtonDown("p1ButtonStart") || Input.GetButtonDown("p2ButtonStart")) {

                RestartGame();

            }

        }
		
		
		
	}

    public void RestartGame() {

        menuNav.StartGame();

        //reset level values
        for (int i = 0; i < manaPickUps.Length; i++)
            manaPickUps[i].ResetValues();

        for (int i = 0; i < players.Length; i++) {
            players[i].ResetValues();
            players[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < walls.Length; i++)
            walls[i].SetActive(true);

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        GameObject[] bullets2 = GameObject.FindGameObjectsWithTag("Bullet2");

        for(int i = 0; i < bullets.Length; i++) {
            Destroy(bullets[i]);
        }

        for (int i = 0; i < bullets2.Length; i++) {
            Destroy(bullets2[i]);
        }

    }

}
