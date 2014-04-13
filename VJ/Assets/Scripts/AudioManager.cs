using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
	public AudioSource audioSource;
	public float audioTime = 0.0f;
	// Use this for initialization
	void Start ()
	{
	
	}
	// Update is called once per frame
	void Update ()
	{
		audioTime = audioSource.time;
	}
}
