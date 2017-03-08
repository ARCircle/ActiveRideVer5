using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutinManager : MonoBehaviour {

    public List<GameObject> CutInObjects = new List<GameObject>();

    private Vector3 CutInPosFrom = new Vector3(1000f,0,0);
    private Vector3 CutInPosTo = new Vector3(0f,0,0);
    private Vector3 CutInPos;

    public static bool canCutIn;

    private float TimeLeft;
    private float DeltaTime;
    private float TimeLeft_MAX = 0.5f;
    private float CutInDelta;

    // Use this for initialization
    void Start () {
		foreach(GameObject cutin in CutInObjects)
        {
            cutin.SetActive(false);
        }
        canCutIn = false;
        TimeLeft = TimeLeft_MAX;
        DeltaTime = Time.deltaTime;

        CutInPos = CutInPosFrom;

        CutInDelta = TimeLeft_MAX / Time.deltaTime;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(KeyCode.Return))
        {
            if (canCutIn)
            {
                CutInObjects[0].SetActive(false);
                canCutIn = false;
            }
            else
            {
                CutInObjects[0].SetActive(true);
                canCutIn = true;
            }
        }

        if (canCutIn)
        {
            if (TimeLeft >= 0f)
            {
                CutInPos.x = CutInPos.x - CutInPosFrom.x / CutInDelta;
                CutInObjects[0].transform.localPosition = CutInPos;
                TimeLeft -= DeltaTime;
            }
            else
            {
                TimeLeft = TimeLeft_MAX;
                CutInPos.x = CutInPosFrom.x;
                CutInObjects[0].transform.localPosition = CutInPosFrom;
                CutInObjects[0].SetActive(false);
                canCutIn = false;
            }
        }
	}
}
