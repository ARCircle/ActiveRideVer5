using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.transform.Rotate(new Vector3(0, 0, -0.3f));
    }

    // Update is called once per frame
    void Update () {
        this.gameObject.transform.Rotate(new Vector3(0, 0, -0.3f));

    }
}
