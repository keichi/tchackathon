using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VJSequencer : MonoBehaviour
{
	public GameObject[] Effects = new GameObject[4];
	private int currentEffectIndex = 0;
	private Dictionary<string, int> MOOD_DIC = new Dictionary<string, int> () {
		{ "Energizing",2 },
		{ "Sensual",0 },
		{ "Defiant",3 },
		{ "Easygoing",3 },
		{ "Cool",2 },
		{ "Urgent",2 },
		{ "Excited",3 },
		{ "Sophisticated",3 },
		{ "Fiery",2 },
		{ "Melancholy",2 },
		{ "Somber",2 },
		{ "Other",3 },
		{ "Fun",2 },
		{ "Tender",2 },
		{ "Blue",3 },
		{ "Sentimental",2 },
		{ "Peaceful",2 },
		{ "Yearning",3 },
		{ "Romantic",2 },
		{ "Stirring",3 },
		{ "Brooding",2 },
		{ "Empowering",2 },
		{ "Rowdy",1 },
		{ "Lively",3 },
		{ "Aggressive",2 },
	};
	// Use this for initialization
	void Start ()
	{
		//Effects [currentEffectIndex].SetActive (true);
	}
	// Update is called once per frame
	void Update ()
	{
	}

	public void ChangeMood (string mood)
	{
		int index = 0;
		if (MOOD_DIC.TryGetValue (mood, out index)) {
			ChangeEffect (MOOD_DIC [mood]);
			var camera = gameObject.GetComponent<CameraMove> ();
			camera.isPause = mood.Equals("Sensual");
		}
	}

	private void ChangeEffect (int index)
	{
		if (currentEffectIndex != index) {
			Effects [currentEffectIndex].SetActive (false);
			Effects [index].SetActive (true);
			currentEffectIndex = index;
		}
	}
}
