using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundFactory : MonoBehaviour {

    public GameObject BackGroundPrefab;
    public List<BackgroundMetaData> ParentObjects = new List<BackgroundMetaData>();
    private GameObject instance;

    // Use this for initialization
    void Start () {
        foreach(var p in ParentObjects)
        {

            instance = (GameObject)Instantiate(BackGroundPrefab);
            instance.transform.parent = p.ParentObject.transform;
            //instance.transform.localPosition = p.ParentObject.transform.localPosition;
            instance.transform.localPosition = new Vector3(0, 0, 0);
            instance.transform.localRotation = p.ParentObject.transform.localRotation;

            instance.transform.localScale = p.Scale;
            instance.transform.localRotation = p.localRotate;

            instance.SetActive(true);
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
        if (instance != null)
        {
            instance.SetActive(true);

        }
    }

    //個々のBackground情報を格納するリスト
    [System.Serializable]
    public class BackgroundMetaData
    {
        public GameObject ParentObject;
        public Vector3 Scale;
        public Quaternion localRotate;
    }
}
