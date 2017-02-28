using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundFactory : MonoBehaviour {

    public GameObject BackGroundPrefab;
    public List<GameObject> MotherObjects = new List<GameObject>();

    // Use this for initialization
    void Start () {
        foreach(var m in MotherObjects)
        {
            GameObject instance = (GameObject)Instantiate(BackGroundPrefab);
            instance.transform.parent = m.transform;
            instance.transform.localPosition = m.transform.localPosition;

            instance.transform.localScale = new Vector3(10, 10, 0);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
