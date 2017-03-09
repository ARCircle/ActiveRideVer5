using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutinManager : MonoBehaviour {
    //カットインするオブジェクト群を格納
    public List<GameObject> CutInObjects = new List<GameObject>();

    //カットインの起点と終点
    private Vector3 CutInPosFrom = new Vector3(1000f,0,0);
    private Vector3 CutInPosTo = new Vector3(0f,0,0);
    //カットインするオブジェクトの位置
    private Vector3 CutInPos;

    public static bool canCutIn;

    private float TimeLeft;
    private float DeltaTime;

    //カットインを行う時間
    public float TimeLeft_MAX = 0.5f;
    private float CutInDelta;

    //Canvasとか低い階層にアタッチ
    //canCutInをPauserScript.csに渡して止めてるので, PauserScriptもIsModalOptionがfalseになるようにアタッチ

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
            //カットイン起こしたいトリガ処理に応じてCutInChecker(GameObject CutInTarget)に
            //カットイン対象のGameObjectを投げる
            //(この場合にはCutInObjects[0]に格納したものの1つ, Start関数ですべてSetActive(false)にしてる)
            CutInChecker(CutInObjects[0]);
        }

        if (canCutIn)
        {
            //CutInObjects内でSetActiveがCutInChecker(GameObject CutInTarget)にてtrueに指定, 
            //かつカットインを起こせるか否かのフラグがtrueのときのみ実行される
            foreach (GameObject targetObject in CutInObjects)
            {
                if (targetObject.activeSelf)
                {
                    DoCutIn(targetObject);
                }
            }

        }
	}

    public void CutInChecker(GameObject CutInTarget)
    {
        if (canCutIn)
        {
            CutInTarget.SetActive(false);
            canCutIn = false;
        }
        else
        {
            CutInTarget.SetActive(true);
            canCutIn = true;
        }
    }

    public void DoCutIn(GameObject CutInTarget)
    {
            if (TimeLeft >= 0f)
            {
                CutInPos.x = CutInPos.x - CutInPosFrom.x / CutInDelta;
                CutInTarget.transform.localPosition = CutInPos;
                TimeLeft -= DeltaTime;
            }
            else
            {
                TimeLeft = TimeLeft_MAX;
                CutInPos.x = CutInPosFrom.x;
                CutInTarget.transform.localPosition = CutInPosFrom;
                CutInTarget.SetActive(false);
                canCutIn = false;
            }
    }
}
