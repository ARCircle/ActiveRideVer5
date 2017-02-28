using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundFactory : MonoBehaviour {

    public GameObject BackGroundPrefab;
    public List<GameObject> MotherObjects = new List<GameObject>();
    private GameObject instance;

    // Use this for initialization
    void Start () {
        foreach(var m in MotherObjects)
        {
            instance = (GameObject)Instantiate(BackGroundPrefab);
            instance.transform.parent = m.transform;
            instance.transform.localPosition = m.transform.localPosition;

            instance.transform.localScale = new Vector3(10, 10, 0);
            instance.transform.localRotation = new Quaternion(0, 0, 0, 0);

            instance.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (instance != null)
        {
            instance.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update () {
        if (!instance.activeSelf)
        {
            instance.SetActive(true);

        }
    }
}
