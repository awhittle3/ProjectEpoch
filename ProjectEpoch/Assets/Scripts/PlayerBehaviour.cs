using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

	public Transform arms;
	public float armRotationSpeed;
	public float rotationSpeed;
	public Transform bomb;
	public Transform bombSpawn;
	public Transform bombExplosionPos;

	// TODO: Possibly refactor bomb into its own class
	private Transform bombInstance;
	private float bombSize;
	private float bombTimer;
	public float bombTimeout = 15f;

	public float throwForce;


	// Use this for initialization
	void Start () {
		bombInstance = null;
		bombSize = 0.01f;
		bombTimer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			// Rotate the arms!
			arms.Rotate (Vector3.left, armRotationSpeed * Time.deltaTime);
			chargeBomb ();
		} else {
			if (bombInstance != null) {
				
				if (bombTimer < 0.001f) {
					// Remove constraints
					bombInstance.GetComponent<Rigidbody> ().constraints = 0;
					// Let 'er fly
					bombInstance.GetComponent<Rigidbody> ().AddExplosionForce (throwForce, bombExplosionPos.position, 200f);
				}

				bombTimer += Time.deltaTime;

				if (bombTimer > bombTimeout) {
					Destroy (bombInstance.gameObject);
				}
			}

			// Experimented with using arms.rotation.eulerangles.x as a constraint, but it does not behave as expected.
		}
			
		this.transform.Rotate(Vector3.up, Input.GetAxis ("Horizontal") * armRotationSpeed * Time.deltaTime);
	}

	void chargeBomb(){
		//Create bomb if none exists
		if (bombInstance == null) {
			bombSize = 0.01f;
			bombTimer = 0f;
			bombInstance = Instantiate (bomb, bombSpawn.position, bombSpawn.rotation);
			bombInstance.transform.localScale = new Vector3 (bombSize, bombSize, bombSize);
			// Freeze in space
			bombInstance.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		}
		bombSize += 0.001f;
		bombInstance.transform.localScale = new Vector3 (bombSize, bombSize, bombSize);
		// Raise it up a bit each time it grows
		bombInstance.position += new Vector3(0f, 0.001f, 0f);
	}
}
