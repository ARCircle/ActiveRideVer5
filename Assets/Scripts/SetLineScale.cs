using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLineScale : MonoBehaviour {

    private float LineScale;
    private float DeltaScale_X = 0.1f;
    private float MaxScale_X = 5.0f;
    private float DefaultLineScale = 0f;

	// Use this for initialization
	void Start () {
        LineScale = 1 * DefaultLineScale;
        this.transform.localScale = new Vector3(LineScale, 1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
        LineScale = ChangeLineScale(LineScale, this.gameObject);
    }

    public float ChangeLineScale(float LineScale, GameObject TargetObject)
    {
        if (LineScale <= MaxScale_X)
        {
            LineScale += DeltaScale_X;
            this.transform.localScale = new Vector3(LineScale, 1f, 1f);
        }

        //TODO: fix no good coding
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            LineScale = 1 * DefaultLineScale;
            this.transform.localScale = new Vector3(LineScale, 1f, 1f);
        }

        return LineScale;
    }
}
