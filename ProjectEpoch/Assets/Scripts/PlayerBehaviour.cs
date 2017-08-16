using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

	public Transform arms;
	public float rotationSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			// Rotate the arms!
			arms.Rotate (Vector3.left, rotationSpeed * Time.deltaTime);
			chargeBomb ();
		} else {
			// Do nothing.
			// Experimented with using arms.rotation.eulerangles.x as a constraint, but it does not behave as expected.
		}
	}

	void chargeBomb(){

	}
}
