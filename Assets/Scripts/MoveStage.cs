using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStage : MonoBehaviour {
    public GameObject Stage;

    private Vector3 Center = new Vector3(0, 700, 0);
    private Vector3 StagePos = new Vector3();
    private Vector3 StageRotate = new Vector3();

    private float AngleDelta = 0.005f;
    private float Angle;

    private const int radius = 500;
    private int MajorAxis = 200;
    private int MinorAxis = 150;

    private System.Random Offset_seed;
    private int Offset_rand;

    //一定時間ごとに重心ずらすため
    private float TimeLeft;

    // Use this for initialization
    void Start () {
        StagePos = this.transform.localPosition;
        StageRotate = this.transform.localRotation.eulerAngles;
        Angle = 0;
	}
	
	// Update is called once per frame
	void Update () {

        TimeLeft -= Time.deltaTime;
        if (TimeLeft <= 0.0)
        {
            TimeLeft = 10.0f;

            Offset_seed = new System.Random();
            Offset_rand = Offset_seed.Next(-100, 100);
        }else
        {
            Offset_rand = 0;
        }

        Angle += AngleDelta;

        MajorAxis += Offset_rand;
        MinorAxis += Offset_rand;

        StagePos.x = MajorAxis * Mathf.Sin(Angle);
        StagePos.z = MinorAxis * Mathf.Cos(Angle);

        StageRotate.y = Angle;

        this.gameObject.transform.localPosition = StagePos;
        this.transform.rotation = Quaternion.Euler(StageRotate);
    }
}
