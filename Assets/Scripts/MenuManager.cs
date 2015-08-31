using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public float changeDelay = 1;
	public float changeSensitivity = 0.2f;

	public GameObject selector;

	public Text error;

	public Transform[] Arenas;

	private int index = 0;
	private float nxtChange = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		bool select = Input.GetButtonDown("Fire3");
		float change = Input.GetAxis("Vertical");

		if ((change > changeSensitivity) && (Time.time > nxtChange)) {
			nxtChange = Time.time + changeDelay;
			if (index == 0) {
				index = Arenas.Length - 1;
			} else {
				index--;
			}

			ChangeArena();
		} else if ((change < -changeSensitivity) && (Time.time > nxtChange)) {
			nxtChange = Time.time + changeDelay;
			if (index == (Arenas.Length - 1)) {
				index = 0;
			} else {
				index++;
			}
			
			ChangeArena();
		}

		if (select) {
			SelectArena();
		}

		if ((change < changeSensitivity) && (change > -changeSensitivity)){
			nxtChange = Time.time - 0.1f;
		}
	}

	private void ChangeArena(){
		Transform arena = (Transform)Arenas.GetValue(index);
		selector.transform.position = arena.position;
	}

	private void SelectArena(){
		switch (index){
		    case 0:
				SceneLoading("Arena1");
				break;

			case 1:
				ErrorMessage("Arena em desenvolvimento");
				break;

			case 2:
				ErrorMessage("Arena em desenvolvimento");
				break;

			case 3:
				ErrorMessage("Arena em desenvolvimento");
				break;
			
			case 4:
				ErrorMessage("Arena em desenvolvimento");
				break;
			
			default:
				ErrorMessage("Opçao invalida");
				break;
		}
	}

	private void ErrorMessage(string msg) {
		error.text = msg;
		error.GetComponent<Animator>().SetTrigger("Error");
	}

	private void SceneLoading(string sceneName) {
		Application.LoadLevel(sceneName);
	}
}
