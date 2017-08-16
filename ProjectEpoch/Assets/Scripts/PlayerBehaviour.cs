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
			
			arms.Rotate (Vector3.right, rotationSpeed * Time.deltaTime);

			chargeBomb ();
		} else {

		}
	}

	void chargeBomb(){

	}
}
