using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	public Transform focusPoint;
	public float speed;
	public float turnaroundTime;

	private float turnaroundTimer;
	private float direction = 1.0f;

	// Use this for initialization
	void Start () {
		turnaroundTimer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.RotateAround (focusPoint.position, Vector3.up, direction * speed * Time.deltaTime);
		turnaroundTimer += Time.deltaTime;
		if (turnaroundTimer > turnaroundTime) {
			turnaroundTimer = 0.0f;
			direction *= -1.0f;
		}
	}
}
