using UnityEngine;
using System.Collections;

public class Touch : MonoBehaviour {

	Ray ray; 
	GameObject selectedObject;
	RaycastHit hit;
	int num;
	public Material selectedMat; 
	public bool endGame = false;
	public bool checking; 

	public AudioClip pickup; 
	public AudioClip wrongClip;
	public AudioClip rightClip; 


	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {

		ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Input.GetKeyDown(KeyCode.Q)) {
			endGame = !endGame;
		}

		if (endGame) {
		selectBuilding();
		}

		selectCollectable();

	}

	void selectBuilding() {

		if (num >= Shop.order.Length) {
			Game_Controller.finished = true;
		}

		if (Input.GetMouseButtonDown(0) && checking == false) {

			if (Physics.Raycast(ray, out hit, 200))
				
				print (hit.collider.gameObject);
			
			if (hit.collider.gameObject.tag == "Building") { 
				selectedObject = hit.collider.gameObject;

				if (num < Shop.order.Length) {
					checking = true;
					StartCoroutine(CheckIfMatch());

				}
			}
		}
	}

	IEnumerator CheckIfMatch ()
	{

		if (checking) {
			Renderer[] renderSelected; 
		
			renderSelected = selectedObject.GetComponentsInChildren<Renderer>();
		
			for (int i = 0; i < renderSelected.Length; i++) {
				renderSelected[i].material.color = Color.white;
			}

			yield return new WaitForSeconds(2);

				if (Shop.order[num] == selectedObject) {

					for (int i = 0; i < renderSelected.Length; i++) {
						renderSelected[i].material.color = Color.green;
						} 

				print ("match");
				num++;
				print (num);

				Game_Controller.score += 100;
				AudioSource.PlayClipAtPoint(rightClip, selectedObject.transform.position);


				yield return new WaitForSeconds(1);

				for (int i = 0; i < renderSelected.Length; i++) {
					renderSelected[i].material.color = selectedObject.GetComponent<Shop>().originalColor[i];
				} 


				checking = false;


					} else {
					print("Not match");
						for (int i = 0; i < renderSelected.Length; i++) {
							renderSelected[i].material.color = Color.red;
						} 

				Game_Controller.score -= 50;
				AudioSource.PlayClipAtPoint(wrongClip, selectedObject.transform.position);

				yield return new WaitForSeconds(1);

				for (int i = 0; i < renderSelected.Length; i++) {
					renderSelected[i].material.color = selectedObject.GetComponent<Shop>().originalColor[i];
				} 
				checking = false;
				}
			}
		}

	void selectCollectable() {

		if (Input.GetMouseButtonDown(0) && checking == false) {

			if (Physics.Raycast(ray, out hit, 200))

		if (hit.collider.gameObject.tag == "Collectable") { 
			selectedObject = hit.collider.gameObject;
				Game_Controller.coinAmount += 1;

				//Play sound
				AudioSource.PlayClipAtPoint(pickup,GameObject.FindGameObjectWithTag("Puppet").transform.position);
			
				Renderer[] renderSelected; 

				Destroy (selectedObject);
			
				renderSelected = selectedObject.GetComponentsInChildren<Renderer>();
			
				for (int i = 0; i < renderSelected.Length; i++) {
					renderSelected[i].material = selectedMat;
				}
			}
		}
	}



}

