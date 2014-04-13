using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	public GameObject camera;
	public Vector3 endPos;

	public VJGameObjectTrigger voice;

	public bool isPause = false;

	// Use this for initialization
	void Start () {
		endPos = camera.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (isPause) {
			return;
		}
		Vector3 pos = endPos;
		pos.z += (3.0f - voice.lastReturnedValue * 3.0f);

		camera.transform.localPosition -= (camera.transform.localPosition - pos) * 0.1f;
	}
}
