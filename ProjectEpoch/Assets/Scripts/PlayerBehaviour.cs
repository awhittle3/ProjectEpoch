#define KINECTMODE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerBehaviour : MonoBehaviour {

	public Transform arms;
	public float armRotationSpeed;
	public float rotationSpeed;
	public Transform bomb;
	public Transform bombSpawn;
	public Transform bombExplosionPosSpawn;

	// TODO: Possibly refactor bomb into its own class
	private Transform bombInstance;
	private float bombSize;
	private float bombTimer;
	public float bombTimeout = 15f;
	public float bombGrowSpeed;
	public float throwForce;
	//private Vector3 explosionPos;

	public Transform kRightArm;
	public Transform kLeftArm;
	public Transform kLowerTorso;


	// Use this for initialization
	void Start () {
		bombInstance = null;
		bombSize = 0.01f;
		bombTimer = 0f;
		//explosionPos = bombExplosionPosSpawn.position;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey ("escape")) {
			Application.Quit ();
		}

		#if (KINECTMODE)
		if (armsUp()){
		#else
	
		if (Input.GetMouseButton (0)) {
			// Rotate the arms!
			arms.Rotate (Vector3.left, armRotationSpeed * Time.deltaTime);
		#endif

			chargeBomb ();
		} else {
			if (bombInstance != null) {
				
				if (bombTimer < 0.001f) {
					// Remove constraints
					bombInstance.GetComponent<Rigidbody> ().constraints = 0;
					bombInstance.GetComponent<Rigidbody>().useGravity = true;


					// Determine direction of flight
					Vector3 diff = kLeftArm.position - kRightArm.position;
					diff.y = 0f;
					float angle = Vector3.Angle(Vector3.left, diff);

					// Move the motivating force around
					GameObject tempExplosion = new GameObject();
					tempExplosion.transform.position = bombExplosionPosSpawn.position; // Copying
					tempExplosion.transform.RotateAround(this.transform.position, Vector3.up, angle - 45f);

					// Let 'er fly
					bombInstance.GetComponent<Rigidbody> ().AddExplosionForce (throwForce, tempExplosion.transform.position, 200f);
				}

				bombTimer += Time.deltaTime;

				if (bombTimer > bombTimeout) {
					// Destroy after some time
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
			//explosionPos = bombExplosionPosSpawn.position;
			bombSize = 0.01f;
			bombTimer = 0f;
			bombInstance = Instantiate (bomb, bombSpawn.position, bombSpawn.rotation);
			bombInstance.transform.localScale = new Vector3 (bombSize, bombSize, bombSize);
			// Freeze in space
			bombInstance.GetComponent<Rigidbody>().useGravity = false;
		}
		bombInstance.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

		bombSize += bombGrowSpeed;
		bombInstance.transform.localScale = new Vector3 (bombSize, bombSize, bombSize);
		// Raise it up a bit each time it grows
		bombInstance.position += new Vector3(0f, bombGrowSpeed/2f, 0f);
		//explosionPos += new Vector3 (0f, 0f, -bombGrowSpeed/2);
	}

	#if (KINECTMODE)

	// Simple function for determining whether arms are above the waist
	bool armsUp(){
			return (kRightArm.position.y > kLowerTorso.position.y && kLeftArm.position.y > kLowerTorso.position.y);
	}

	#endif
}


