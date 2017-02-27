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
    public List<GameObject> Characters = new List<GameObject>();

    private float CameraPosFrom = -20.0f;
    private float CameraPosTo = -10.0f;

    private Vector3 DescriptionPosFrom = new Vector3(100f, 0f, 0f);
    private Vector3 DescriptionPosTo = new Vector3(30f, 0f, -195f);

    private int CameraMoveFlag;
    private bool DescriptionMoveFlag;

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
            UIsActiveControl(0);
        }

        if (Input.GetKeyDown(KeyCode.W) && GalleryCanvas.OnStoryFlag )
        {
            CameraIndex++;

            if (CameraIndex >= 3) { CameraIndex = 0; }

            switch (CameraIndex) {
                case 0:
                    posB.z = CameraPosFrom;
                    BANSHEE_Cam.transform.localPosition = posB;
                    SetCameraInActive();

                    UIsActiveControl(0);
                    break;
                case 1:
                    posU.z = CameraPosFrom;
                    UNICORN_Cam.transform.localPosition = posU;

                    SetCameraInActive();
                    UIsActiveControl(1);
                    break;
                case 2:
                    posP.z = CameraPosFrom;
                    PHOENIX_Cam.transform.localPosition = posP;

                    SetCameraInActive();
                    UIsActiveControl(2);
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
        if(DescriptionMoveFlag)
        {

            if (pos.x >= DescriptionPosTo.x)
            {
                pos.z -= 10.0f;
                pos.x -= 0.5f;
            }else
            {
                DescriptionMoveFlag = false;
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

        BANSHEE_Cam.gameObject.transform.parent.gameObject.SetActive(false);
        UNICORN_Cam.gameObject.transform.parent.gameObject.SetActive(false);
        PHOENIX_Cam.gameObject.transform.parent.gameObject.SetActive(false);

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
        CameraMoveFlag = index + 1;
        DescriptionMoveFlag = true;
        Descriptions[index].transform.localPosition = DescriptionPosTo;

        Descriptions[index].SetActive(true);
        Descriptions[index].gameObject.GetComponent<ShowUIText>().enabled = true;

        Characters[index].SetActive(true);
        Characters[index].gameObject.GetComponent<UIMaskTransparent>().enabled = true;

        switch (index)
        {
            case 0:
                BANSHEE_Cam.SetActive(true);
                BANSHEE_Cam.gameObject.transform.parent.gameObject.SetActive(true);
                break;
            case 1:
                UNICORN_Cam.SetActive(true);
                UNICORN_Cam.gameObject.transform.parent.gameObject.SetActive(true);
                break;
            case 2:
                PHOENIX_Cam.SetActive(true);
                PHOENIX_Cam.gameObject.transform.parent.gameObject.SetActive(true);
                break;
            default:
                break;
        }

    }

}
