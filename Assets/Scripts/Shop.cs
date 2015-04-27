using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Shop : MonoBehaviour {
	
	public Renderer[] buildingRenderer; 
	public Color[] originalColor;
	private Vector3 originalScale;  
	public static int AMOUNT_TO_REMEMBER = 5; 
	public static GameObject[] order; 
	public static Text remaining; 
	public GameObject enterArea;

	float scaleValue;
	bool entered;
	static int num;
	public AudioClip alarmClip; 

	GameObject puppet; 
	
	/* This script is used to control how the puppet
	 * object interacts with the buildings in the game. 
	 * The puppet object should e.g. only be able to
	 * enter a building, if he is standing next to a
	 * building.
	*/
	
	
	// Use this for initialization
	void Start () {
		order = new GameObject[AMOUNT_TO_REMEMBER];

		remaining = GameObject.Find("Remaining").GetComponent<Text>();
		
		originalScale = transform.localScale; 
		
		buildingRenderer = GetComponentsInChildren<Renderer>();
		
		originalColor = new Color[buildingRenderer.Length];
		
		for (int i = 0; i < buildingRenderer.Length; i++) {
			originalColor[i] = buildingRenderer[i].sharedMaterial.color;
		}

		puppet = GameObject.FindGameObjectWithTag("Puppet");

		remaining.text = "" + order.Length + "/" + order.Length + " remaining";
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (enterArea.GetComponent<Enter_Area>().enterable == true && Input.GetKeyDown(KeyCode.E) && this.entered == false) {
			entered = true; 

			if (num < order.Length) {
			order[num] = gameObject;
			num++;
				remaining.text = "" + (order.Length - num) + "/" + order.Length + " remaining";
			}

			puppet.gameObject.GetComponent<Puppet_Controller>().visible = false;
			puppet.gameObject.GetComponent<Puppet_Controller>().visibility(); 

			AudioSource.PlayClipAtPoint(alarmClip,transform.position);

			StartCoroutine(robbery());  
		} 

		if (entered == true) {
			animate();
		} 

		//This part of the code logs the buildings into an array list

	}
	
	public void animate() {
			for (int i = 0; i < buildingRenderer.Length; i++) {
				buildingRenderer[i].material.color = Color.Lerp(Color.red,Color.white,0.5f);
			}
			scaleValue += 5f * Time.deltaTime;
			transform.localScale += new Vector3 (Mathf.Cos(scaleValue)/10,Mathf.Cos(scaleValue)/10,Mathf.Cos(scaleValue)/10);
	}

	void reset() {
			for (int i = 0; i < buildingRenderer.Length; i++) {
				buildingRenderer[i].material.color = originalColor[i];
			}
			
			transform.localScale = originalScale;
	}

	IEnumerator robbery() {
		yield return new WaitForSeconds(3);
		entered = false;
		reset(); 
		puppet.gameObject.GetComponent<Puppet_Controller>().visible = true;
		puppet.gameObject.GetComponent<Puppet_Controller>().visibility(); 

	}

}
