using UnityEngine;
using System.Collections;

public class Enter_Area : MonoBehaviour {

	public bool enterable; 

	void Start() {
	}

	void OnTriggerEnter(Collider other) {
		if (other.name == "Puppet") {
			enterable = true; 
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.name == "Puppet") {
			enterable = false; 
		}
	}
}