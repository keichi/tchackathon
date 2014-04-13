using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VJSequencer : MonoBehaviour
{
	public GameObject[] Effects = new GameObject[4];
	private int currentEffectIndex = 0;
	private Dictionary<string, int> MOOD_DIC = new Dictionary<string, int> () {
		{ "Energizing",0 },
		{ "Sensual",1 },
		{ "Defiant",2 },
		{ "Easygoing",3 },
		{ "Cool",0 },
		{ "Urgent",1 },
		{ "Excited",2 },
		{ "Sophisticated",3 },
		{ "Fiery",0 },
		{ "Melancholy",1 },
		{ "Somber",2 },
		{ "Other",3 },
		{ "Fun",0 },
		{ "Tender",1 },
		{ "Blue",2 },
		{ "Sentimental",3 },
		{ "Peaceful",0 },
		{ "Yearning",1 },
		{ "Romantic",2 },
		{ "Stirring",3 },
		{ "Brooding",0 },
		{ "Empowering",1 },
		{ "Rowdy",2 },
		{ "Lively",3 },
		{ "Aggressive",0 },
	};
	// Use this for initialization
	void Start ()
	{
		Effects [currentEffectIndex].SetActive (true);
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
		}
	}

	private void ChangeEffect (int index)
	{
		if (currentEffectIndex != index) {
			Debug.LogError ("change");
			Effects [currentEffectIndex].SetActive (false);
			Effects [index].SetActive (true);
			currentEffectIndex = index;
		}
	}
}
