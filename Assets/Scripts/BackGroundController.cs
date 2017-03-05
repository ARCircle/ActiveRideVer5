using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundController : MonoBehaviour {

    public GameObject Circle1;
    public GameObject Circle2;
    public GameObject Circle3;
    public GameObject Circle4;

	// Use this for initialization
	void Start () {
        //this.gameObject.transform.Rotate(new Vector3(0, 0, -0.3f));
    }

    // Update is called once per frame
    void Update () {
        Circle1.gameObject.transform.Rotate(new Vector3(0, 0, -0.3f));
        Circle2.gameObject.transform.Rotate(new Vector3(0, 0, 0.2f));
    }
}
