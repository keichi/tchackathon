using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
	public AudioSource audioSource;
	public float audioTime = 0.0f;
	public float CurrentAudioTime = 1.0f;

	// Use this for initialization
	void Start ()
	{
	}
	// Update is called once per frame
	void Update ()
	{
		audioTime = audioSource.time;

		if (Input.GetKey(KeyCode.UpArrow))
		{
			audioSource.time = audioTime - CurrentAudioTime;
		}

		if (Input.GetKey(KeyCode.DownArrow))
		{
			audioSource.time = audioTime + CurrentAudioTime;
		}
	}
}
