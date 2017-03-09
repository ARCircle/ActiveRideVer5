using UnityEngine;
using System.Collections;

public class Movie : MonoBehaviour {

	MovieTexture movieTexture;

	void Start () {
		movieTexture = (MovieTexture)(GetComponent<Renderer>().material.mainTexture);
		movieTexture.Play();
	}
}