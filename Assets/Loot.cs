﻿using UnityEngine;
using System.Collections;

public class Loot : MonoBehaviour {

	GameObject collectable;
	public bool dropLoot; 
	bool isSpawning = false;
	public float minTime = 5.0f; 
	public float maxTime = 10.0f;

	
	// Use this for initialization
	void Start () {
	
		collectable = GameObject.Find ("Collectable");
	}
	
	// Update is called once per frame
	void Update () {

		if(!isSpawning && dropLoot)
		{
			isSpawning = true; //Yep, we're going to spawn
			StartCoroutine(SpawnObject(Random.Range(minTime, maxTime)));
		}
	}
		
		IEnumerator Example() {
			
		Instantiate(collectable,new Vector3(transform.position.x,transform.position.y + 2, transform.position.z), Quaternion.identity);	

			yield return new WaitForSeconds(5);
		}

	IEnumerator SpawnObject(float seconds)
	{
		Debug.Log ("Waiting for " + seconds + " seconds");
		
		yield return new WaitForSeconds(seconds);

		if (dropLoot) {
		Instantiate(collectable,new Vector3(transform.position.x,transform.position.y + 2, transform.position.z), Quaternion.identity);	
		}

		//We've spawned, so now we could start another spawn     
		isSpawning = false;
	}

}
