using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	// List of points that the enemy bounces between
	public List<Transform> points;
	public float speed;  // User defined
	public float turnaroundTime;  // Timeout for 

	private float turnaroundTimer;
	private Vector3 direction;  // Direction of travel
	private int destination;  // Destination of travel

	// Use this for initialization
	void Start () {
		turnaroundTimer = 0.0f;
		direction = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		turnaroundTimer += Time.deltaTime;

		// On timeout, choose a new destination
		if (turnaroundTimer > turnaroundTime) {
			turnaroundTimer = 0.0f; // Reset timer
			// Randomly choose a point from the list
			destination = Random.Range (0, points.Count);
		}

		// Determine direction this should travel
		direction = Vector3.Normalize(points[destination].position - this.transform.position);
		this.transform.Translate(direction * speed * Time.deltaTime);
	}
}
