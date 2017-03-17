using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class idoustage3 : MonoBehaviour {

	float timer = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position -= new Vector3(0f, 0f, 20f*Time.deltaTime);

		timer += Time.deltaTime;

		if(timer > 5){
			if (Input.anyKeyDown) {
				UnityEngine.SceneManagement.SceneManager.LoadScene("main3");
			}

				
			}
	}
}