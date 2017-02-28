using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HumanController : MonoBehaviour {

    private int Y_Offset;
    private int Y_Delta = 1;
    private const int OFFSET_MAX = 3;
    private const int OFFSET_MIN = -3;
    private float TimeLeft;

    private Vector3 Position;
	// Use this for initialization
	void Start () {
        Position = this.transform.localPosition;
        Y_Offset = 0;
    }
	
	// Update is called once per frame
	void Update () {

        if (Y_Offset >= OFFSET_MAX || Y_Offset <= OFFSET_MIN) {
            Y_Delta *= -1;
        }

        TimeLeft -= Time.deltaTime;
        if (TimeLeft <= 0.0)
        {
            TimeLeft = 0.8f;
            Y_Offset += Y_Delta;
        }

        //Debug.Log(Y_Offset);
        this.transform.localPosition = Position + new Vector3(0, Y_Offset, 0);
    }
}
