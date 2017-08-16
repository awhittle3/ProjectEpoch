using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

	public Transform arms;
	public float armRotationSpeed;
	public float rotationSpeed;
	public Transform bomb;
	public Transform bombSpawn;

	private Transform bombInstance;
	private float bombSize;


	// Use this for initialization
	void Start () {
		bombInstance = null;
		bombSize = 0.01f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			// Rotate the arms!
			arms.Rotate (Vector3.left, armRotationSpeed * Time.deltaTime);
			chargeBomb ();
		} else {
			if (bombInstance != null) {
				
			}

			// Experimented with using arms.rotation.eulerangles.x as a constraint, but it does not behave as expected.
		}
			
		this.transform.Rotate(Vector3.up, Input.GetAxis ("Horizontal") * armRotationSpeed * Time.deltaTime);
	}

	void chargeBomb(){
		
		if (bombInstance == null) {
			bombInstance = Instantiate (bomb, bombSpawn.position, bombSpawn.rotation);
			bombInstance.transform.localScale = new Vector3 (bombSize, bombSize, bombSize);
			bombInstance.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		}
		bombSize += 0.001f;
		bombInstance.transform.localScale = new Vector3 (bombSize, bombSize, bombSize);
		bombInstance.position += new Vector3(0f, 0.001f, 0f);
	}
}
