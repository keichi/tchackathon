using UnityEngine;
using System.Collections;

public class RotationY : MonoBehaviour {

	public float angularVelocity = 45;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(transform.right, angularVelocity*Time.deltaTime);
	}
}
