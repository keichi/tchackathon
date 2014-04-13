using UnityEngine;
using System.Collections;

public class VJSequencer : MonoBehaviour
{
		public GameObject[] Effects = new GameObject[4];
		int index = 0;
		float time = 10.0f;
		// Use this for initialization
		void Start ()
		{
				Effects [index].SetActive (true);
		}
		// Update is called once per frame
		void Update ()
		{
				time -= Time.deltaTime;
				if (time <= 0.0f) {
						time = 10.0f;
						Effects [index++].SetActive (false);
						index = Effects.Length <= index ? 0 : index;
						Effects [index].SetActive (true);
				}
		}
}
