﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	// List of points that the enemy bounces between
	public List<Transform> points;
	public float speed;  // User defined
	public float turnaroundTime;  // Timeout for 

	public ParticleSystem particles;

	private float turnaroundTimer;
	private Vector3 direction;  // Direction of travel
	private int destination;  // Destination of travel

	// Use this for initialization
	void Start () {
		turnaroundTimer = 0.0f;
		direction = Vector3.zero;
		particles.Stop ();
	}
	
	// Update is called once per frame
	void Update () {
		turnaroundTimer += Time.deltaTime;

		// On timeout, choose a new destination
		if (turnaroundTimer > turnaroundTime) {
			turnaroundTimer = 0.0f; // Reset timer
			// Randomly choose a point from the list
			destination = Random.Range (0, points.Count);
			particles.Stop();
		}

		Vector3 distance = points [destination].position - this.transform.position;

		// If the distance is sufficiently large, translate
		if (distance.magnitude > 1f) {
			// Determine direction this should travel
			direction = Vector3.Normalize (distance);
			this.transform.Translate (direction * speed * Time.deltaTime);
		}
	}


	void OnCollisionEnter(Collision col){
		Debug.Log ("KO");
		particles.Play();
	}
}
