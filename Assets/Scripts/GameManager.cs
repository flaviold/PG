using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static int placar1 = 0;
	public static int placar2 = 0;

	public static int round = 0;

	public Text p1;
	public Text p2;
	public Text roundSign;
	public Text winSign;

	public GameObject player1Model;
	public GameObject player2Model;
	public Transform spawnPoint1;
	public Transform spawnPoint2;

	public static bool restart = false;
	public static bool roundOver = false;
	public static bool gameOver = false;
	
	private static GameObject player1;
	private static GameObject player2;

	// Use this for initialization
	void Start () {
		player1 = (GameObject)Instantiate(player1Model, spawnPoint1.position , spawnPoint1.rotation);
		player1.name = "1";
		player2 = (GameObject)Instantiate(player2Model, spawnPoint2.position, spawnPoint2.rotation);
		player2.name = "2";
		player2.GetComponent<Joystick1_Input>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		HUDUpdate();
		if (restart && !gameOver){
			if (player1) Destroy(player1);
			if (player2) Destroy(player2);

			player1 = (GameObject)Instantiate(player1Model, spawnPoint1.position, spawnPoint1.rotation);
			player1.name = "1";
			player2 = (GameObject)Instantiate(player2Model, spawnPoint2.position, spawnPoint2.rotation);
			player2.name = "2";
			player2.GetComponent<Joystick1_Input>().enabled = false;
			restart = false;
		}
	}

	public static void Kill(string playerName) {
		if (playerName == "1") {
			Destroy(player1);
			placar2++;
		} else {
			Destroy(player2);
			placar1++;
		}
		
		if (round < 5) {
			round++;
			roundOver = true;
		}
	}

	public static void GameOver() {
		if (player1) Destroy(player1);
		if (player2) Destroy(player2);

		RestartCount();
		Application.LoadLevel("Menu");
	}

	public void HUDUpdate() {
		p1.text = "Player 1: " + GameManager.placar1;
		p2.text = "Player 2: " + GameManager.placar2;
		if (round == 5) {
			if (placar1 > placar2){
				winSign.text = "Player 1 Won";
			} else {
				winSign.text = "Player 2 Won";
			}
			winSign.GetComponent<Animator>().SetTrigger("GameOver");
		} else {
			roundSign.text = "Round " + GameManager.round;
		}

		if (roundOver) {
			roundOver = false;
			string winner = "";
			if (player1) {
				winner = "Player 1";
				Destroy(player1);
			}
			if (player2) {
				winner = "Player 2";
				Destroy(player2);
			}
			winSign.text = winner + " won the Round";
			winSign.GetComponent<Animator>().SetTrigger("RoundOver");
		}
	}

	public static void RestartCount() {
		placar1 = 0;
		placar2 = 0;
		
		round = 0;
	}
}
