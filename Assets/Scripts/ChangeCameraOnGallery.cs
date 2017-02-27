using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCameraOnGallery : MonoBehaviour {

    public GameObject MainCam;
    public GameObject BANSHEE_Cam;
    public GameObject UNICORN_Cam;
    public GameObject PHOENIX_Cam;

    private Vector3 posB;
    private Vector3 posU;
    private Vector3 posP;

    private Vector3 Dpos;

    public List<GameObject> Descriptions = new List<GameObject>();

    private float CameraPosFrom = -20.0f;
    private float CameraPosTo = -10.0f;

    private Vector3 DescriptionPosFrom = new Vector3(100f, 0f, 0f);
    private Vector3 DescriptionPosTo = new Vector3(-200f,0f,200f/10f);

    private int CameraMoveFlag;
    private int DescriptionMoveFlag;

    private int CameraIndex;

	// Use this for initialization
	void Start () {

        BANSHEE_Cam.transform.localPosition = new Vector3(0, 0, CameraPosFrom);
        UNICORN_Cam.transform.localPosition = new Vector3(0, 0, CameraPosFrom);
        PHOENIX_Cam.transform.localPosition = new Vector3(0, 0, CameraPosFrom);

        SetCameraInActive();
        MainCam.SetActive(true);

        CameraIndex = 1;
    }


    private void OnEnable()
    {
        //Descriptions[0].SetActive(true);
        //BANSHEE_Cam.SetActive(true);
        
        //Pauseから復帰時にfalseになるため
        //カメラ位置が飛ばされるのを防止
        MainCam.SetActive(true);
    }

    // Update is called once per frame

    void Update () {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetCameraInActive();
            MainCam.SetActive(true);
        }
        
        if (Input.GetKeyUp(KeyCode.LeftArrow) && GalleryCanvas.OnStoryFlag)
        {
            posB.z = CameraPosFrom;
            BANSHEE_Cam.transform.localPosition = posB;
            CameraMoveFlag = 1;
            DescriptionMoveFlag = 1;
            Descriptions[0].transform.localPosition = DescriptionPosFrom;
            Descriptions[0].SetActive(true);
            BANSHEE_Cam.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.W) && GalleryCanvas.OnStoryFlag )
        {
            CameraIndex++;
            if (CameraIndex >= 3) CameraIndex = 0;
            switch (CameraIndex) {
                case 0:
                    posB.z = CameraPosFrom;
                    BANSHEE_Cam.transform.localPosition = posB;
                    SetCameraInActive();
                    CameraMoveFlag = 1;
                    DescriptionMoveFlag = 1;
                    Descriptions[0].transform.localPosition = DescriptionPosFrom;
                    Descriptions[0].SetActive(true);
                    BANSHEE_Cam.SetActive(true);
                    break;
                case 1:
                    posU.z = CameraPosFrom;
                    UNICORN_Cam.transform.localPosition = posU;
                    SetCameraInActive();
                    CameraMoveFlag = 2;
                    DescriptionMoveFlag = 1;
                    Descriptions[1].transform.localPosition = DescriptionPosFrom;
                    Descriptions[1].SetActive(true);
                    UNICORN_Cam.SetActive(true);
                    break;
                case 2:
                    posP.z = CameraPosFrom;
                    PHOENIX_Cam.transform.localPosition = posP;
                    SetCameraInActive();
                    CameraMoveFlag = 3;
                    DescriptionMoveFlag = 1;
                    Descriptions[2].transform.localPosition = DescriptionPosFrom;
                    Descriptions[2].SetActive(true);
                    PHOENIX_Cam.SetActive(true);
                    break;
                default:
                    CameraIndex = 0;
                    break;
            }
        }

        switch (CameraMoveFlag)
        {
            //TODO: 位置の初期化
            case 1:
                posB = BANSHEE_Cam.transform.localPosition;
                posB = MoveCamera(UNICORN_Cam, posB);
                BANSHEE_Cam.transform.localPosition = posB;
                Dpos = MoveDescription(Descriptions[0].transform.localPosition);
                Descriptions[0].transform.localPosition = Dpos;
                break;
            case 2:
                posU = UNICORN_Cam.transform.localPosition;
                posU = MoveCamera(UNICORN_Cam, posU);
                UNICORN_Cam.transform.localPosition = posU;
                Dpos = MoveDescription(Descriptions[1].transform.localPosition);
                Descriptions[1].transform.localPosition = Dpos;
                break;
            case 3:
                posP = PHOENIX_Cam.transform.localPosition;
                posP = MoveCamera(PHOENIX_Cam, posP);
                PHOENIX_Cam.transform.localPosition = posP;
                Dpos = MoveDescription(Descriptions[2].transform.localPosition);
                Descriptions[2].transform.localPosition = Dpos;
                break;
            default: break;
        }

    }

    Vector3 MoveDescription(Vector3 pos)
    {
        if(DescriptionMoveFlag!= 0)
        {

            if (pos.x >= DescriptionPosTo.x)
            {
                pos.z -= 5.0f;
                pos.x -= 1.0f;
            }else
            {
                DescriptionMoveFlag = 0;
            }

        }
        return pos;
    }

    Vector3 MoveCamera(GameObject Camera, Vector3 pos)
    {

        if (CameraMoveFlag != 0)
        {

            if (pos.z <= CameraPosTo)
            {
                pos.z = pos.z + 0.3f;
                Camera.transform.localPosition = pos;
                //wait();
                //終端まで達したら元の箇所に戻す
            }else
            {
                CameraMoveFlag = 0;
            }

        }
        return pos;
    } 
    private void SetCameraInActive()
    {
        //MainCam.SetActive(false);
        BANSHEE_Cam.SetActive(false);
        UNICORN_Cam.SetActive(false);
        PHOENIX_Cam.SetActive(false);

        foreach(GameObject d in Descriptions)
        {
            d.SetActive(false);
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.01f);
    }

}
