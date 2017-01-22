using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static string state = "playing";

    public MenuNavigation menuNav;

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

                //restart game
                //menuNav.StartGame();
                //state = "playing";

                state = "playing";
                SceneManager.LoadScene(0);

            }

        }
		
		
		
	}
}
