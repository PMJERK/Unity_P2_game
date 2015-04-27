using UnityEngine;
using System.Collections;

public class Puppet_Controller : MonoBehaviour {
	
	private Animator animator;
	
	private Transform rig; 
	public Transform[] particleMoney = new Transform[2];
	
	private SpriteRenderer[] sprites; 
	private SpriteRenderer[] legSprite = new SpriteRenderer[4];
	
	ParticleSystem[] lootEmitter = new ParticleSystem[2]; 

	Vector3 movement;
	
	//Primitive data types
	bool postTransition, check;
	public bool visible = true;
	public float speed, scale; 
	
	// USE THIS FOR INITIALIZATION
	void Start () {
		
		rig = GameObject.Find ("CharacterRig").transform;
		lootEmitter = GameObject.Find ("CharacterRig").GetComponentsInChildren<ParticleSystem>();
		animator = GetComponentInChildren<Animator>();
		sprites =  GetComponentsInChildren<SpriteRenderer> ();

		//Initialize leg-sprites
		legSprite[0] = GameObject.Find("Left_leg_run").GetComponent<SpriteRenderer>();
		legSprite[1] = GameObject.Find("Right_leg_run").GetComponent<SpriteRenderer>();
		legSprite[2] = GameObject.Find("Left_leg_idle").GetComponent<SpriteRenderer>();
		legSprite[3] = GameObject.Find("Right_leg_idle").GetComponent<SpriteRenderer>();

		//Make sure no particles are emitted when the game starts
		lootEmitter[0].enableEmission = false; 
		lootEmitter[1].enableEmission = false; 
		
		//Hide leg-sprites that are not needed
		legSprite [0].enabled = false; 
		legSprite [1].enabled = false;
		
	}
	
	// UPDATE IS CALLED ONCE PER FRAME
	void Update () {
		
		emitterControl(); //This could be optimized
		
		if (visible == true) {
			animate ();
			controller ();
		}
		
	}

	void emitterControl() {
		//control loot drop
		if (Input.GetKeyDown(KeyCode.R) && visible == true) {
			
			gameObject.GetComponent<Loot>().dropLoot = !gameObject.GetComponent<Loot>().dropLoot;
			lootEmitter[0].enableEmission = !lootEmitter[0].enableEmission;
			lootEmitter[1].enableEmission = !lootEmitter[1].enableEmission;
		} else if (visible == false) {
			gameObject.GetComponent<Loot>().dropLoot = false;
			lootEmitter[0].enableEmission = false;
			lootEmitter[1].enableEmission = false;
		}

	}
	
	//CONTROLLER METHOD FOR THE PUPPET-OBJECT  
	void controller() {
		
		float horizontal = Input.GetAxis ("Horizontal"); //change later
		//print (vertical*speed);

		if (Input.GetKey(KeyCode.W)) {
			movement = new Vector3 (0, 0, 1*speed);
			transform.Translate (movement); 
		} 

		if (Input.GetKey(KeyCode.S)) {
			movement = new Vector3 (0, 0, -1*speed);
			transform.Translate (movement); 
		} 

		if (Input.GetKey(KeyCode.A)) {
			movement = new Vector3 (-1*speed, 0, 0);
			transform.Translate (movement); 
		} 

		if (Input.GetKey(KeyCode.D)) {
		movement = new Vector3 (1*speed, 0, 0);
		transform.Translate (movement); 
	}






		if (horizontal > 0) {
			rig.transform.localScale = new Vector3(-scale,scale,scale);
			particleMoney[0].localScale = new Vector3(-scale,scale,scale);
			particleMoney[1].localScale = new Vector3(-scale,scale,scale);
			
		}
		
		if (horizontal < 0 ) {
			rig.transform.localScale = new Vector3(scale,scale,scale);
			particleMoney[0].localScale = new Vector3(scale,scale,scale);
			particleMoney[1].localScale = new Vector3(scale,scale,scale);
			
		}
	}
	
	//ANIMATION METHOD FOR THE PUPPET-OBJECT 
	void animate() {
		
		if (animator.IsInTransition(0) == true) {
			postTransition = true; 
		}
		
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) {
			
			animator.SetBool ("running", true);
			
			//Display running sprites
			legSprite [0].enabled = true;
			legSprite [1].enabled = true;
			legSprite [2].enabled = false;
			legSprite [3].enabled = false;
			
		} 	else if (animator.IsInTransition(0) && postTransition) {
			
			check = true;
		} 	else {
			animator.SetBool ("running", false);
		}
		
		if (check && !animator.IsInTransition(0)) {
			
			//Display idle sprites
			legSprite [0].enabled = false;
			legSprite [1].enabled = false;
			legSprite [2].enabled = true;
			legSprite [3].enabled = true;
			
			check = false;
		}
	}	
	
	public void visibility() {
		//Hide sprites when entering building
		if (visible == false) {
			
			for (int i = 0; i < sprites.Length; i++) {
				sprites [i].enabled = false;
			}
			for (int i = 0; i < legSprite.Length; i++) {
				legSprite [i].enabled = false;
			}

		} else if (visible == true) {

			for (int i = 0; i < sprites.Length; i++) {
				sprites [i].enabled = true;
			}
			for (int i = 0; i < legSprite.Length; i++) {
				legSprite [i].enabled = true;
			}
			
			legSprite [0].enabled = false;
			legSprite [1].enabled = false;
					}
	}
}
