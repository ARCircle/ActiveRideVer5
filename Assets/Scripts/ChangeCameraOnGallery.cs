using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCameraOnGallery : MonoBehaviour
{
    public GameObject MainCam;
    public GameObject[] AirFrame_Cam;

    private Vector3 posB;
    private Vector3 posU;
    private Vector3 posP;

    private Vector3 Dpos;

    public List<GameObject> Descriptions = new List<GameObject>();
    public List<GameObject> Characters = new List<GameObject>();

    private float CameraPosFrom = -20.0f;
    private float CameraPosTo = -10.0f;

    private Vector3 DescriptionPosFrom = new Vector3(100f, 0f, 0f);
    private Vector3 DescriptionPosTo = new Vector3(30f, 0f, -275f);

    private bool DescriptionMoveFlag;
    private bool AirFrameMoveFlag;

    private int CameraIndex;

    // Use this for initialization
    void Start()
    {

        foreach(var Camera in AirFrame_Cam)
        {
            Camera.transform.localPosition = new Vector3(0, 0, CameraPosFrom);
        }

        SetCameraInActive();
        //UIsActiveControl(0);

        CameraIndex = 0;
        AirFrameMoveFlag = false;
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
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetCameraInActive();
            MainCam.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) && GalleryCanvas.OnStoryFlag)
        {
            UIsActiveControl(0);
        }

        if(Input.GetKeyUp(KeyCode.S) && GalleryCanvas.OnStoryFlag)
        {
            if (AirFrameMoveFlag == false)
            {
                AirFrameMoveFlag = true;
            }
            else
            {
                AirFrame_Cam[CameraIndex].gameObject.transform.parent.gameObject.transform.FindChild("AirFrame").transform.localRotation = new Quaternion(-10, 0, 0, 0);
                AirFrameMoveFlag = false;
            }
        }

        if (AirFrameMoveFlag)
        {
            AirFrame_Cam[CameraIndex].gameObject.transform.parent.gameObject.transform.FindChild("AirFrame").transform.Rotate(new Vector3(0.1f, 0.2f, 0));
        }

        if (Input.GetKeyUp(KeyCode.W) && GalleryCanvas.OnStoryFlag)
        {
            CameraIndex++;

            if (CameraIndex >= 3) { CameraIndex = 0; }

            switch (CameraIndex)
            {
                case 0:
                    posB.z = CameraPosFrom;
                    AirFrame_Cam[0].transform.localPosition = posB;
                    break;
                case 1:
                    posU.z = CameraPosFrom;
                    AirFrame_Cam[1].transform.localPosition = posU;
                    break;
                case 2:
                    posP.z = CameraPosFrom;
                    AirFrame_Cam[2].transform.localPosition = posP;
                    break;
                default:
                    CameraIndex = 0;
                    break;
            }

            SetCameraInActive();
            UIsActiveControl(CameraIndex);
        }

        switch (CameraIndex)
        {
            //TODO: 位置の初期化
            case 0:
                posB = AirFrame_Cam[0].transform.localPosition;
                posB = MoveCamera(AirFrame_Cam[0], posB);
                AirFrame_Cam[0].transform.localPosition = posB;
                Dpos = MoveDescription(Descriptions[0].transform.localPosition);
                Descriptions[0].transform.localPosition = Dpos;
                break;
            case 1:
                posU = AirFrame_Cam[1].transform.localPosition;
                posU = MoveCamera(AirFrame_Cam[1], posU);
                AirFrame_Cam[1].transform.localPosition = posU;
                Dpos = MoveDescription(Descriptions[1].transform.localPosition);
                Descriptions[1].transform.localPosition = Dpos;
                break;
            case 2:
                posP = AirFrame_Cam[2].transform.localPosition;
                posP = MoveCamera(AirFrame_Cam[2], posP);
                AirFrame_Cam[2].transform.localPosition = posP;
                Dpos = MoveDescription(Descriptions[2].transform.localPosition);
                Descriptions[2].transform.localPosition = Dpos;
                break;
            default: break;
        }

    }

    Vector3 MoveDescription(Vector3 pos)
    {
        if (DescriptionMoveFlag)
        {
            if (pos.x >= DescriptionPosTo.x)
            {
                pos.z -= 10.0f;
                pos.x -= 0.5f;
            }
            else
            {
                DescriptionMoveFlag = false;
            }
        }
        return pos;
    }

    //カメラ移動
    Vector3 MoveCamera(GameObject Camera, Vector3 pos)
    {
        if (CameraIndex >= 0 && CameraIndex <= 3)
        {
            if (pos.z <= CameraPosTo)
            {
                pos.z = pos.z + 0.3f;
                Camera.transform.localPosition = pos;
                //wait();
                //終端まで達したら元の箇所に戻す
            }
        }
        return pos;
    }
    private void SetCameraInActive()
    {
        //MainCam.SetActive(false);
        foreach(GameObject Camera in AirFrame_Cam)
        {
            Camera.SetActive(false);
            Camera.gameObject.transform.parent.gameObject.SetActive(false);
        }

        foreach (GameObject d in Descriptions)
        {
            d.SetActive(false);
            d.gameObject.GetComponent<ShowUIText>().enabled = false;
        }

        foreach (GameObject c in Characters)
        {
            c.SetActive(false);
            c.gameObject.GetComponent<UIMaskTransparent>().enabled = false;
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.01f);
    }

    private void UIsActiveControl(int index)
    {
        DescriptionMoveFlag = true;
        Descriptions[index].transform.localPosition = DescriptionPosTo;

        Descriptions[index].SetActive(true);
        Descriptions[index].gameObject.GetComponent<ShowUIText>().enabled = true;

        Characters[index].SetActive(true);
        Characters[index].gameObject.GetComponent<UIMaskTransparent>().enabled = true;

        AirFrame_Cam[index].SetActive(true);
        AirFrame_Cam[index].gameObject.transform.parent.gameObject.SetActive(true);
 
    }

}