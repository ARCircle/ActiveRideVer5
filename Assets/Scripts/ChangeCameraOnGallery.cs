using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCameraOnGallery : MonoBehaviour
{
    public GameObject MainCam;
    public GameObject[] AirFrame_Cam;

    private Vector3[] AirFrame_Pos = new Vector3[3];

    private Vector3 Dpos;

    public List<GameObject> Descriptions = new List<GameObject>();
    public List<GameObject> Characters = new List<GameObject>();
	public List<GameObject> Operators = new List<GameObject> ();

    private float CameraPosFrom = -20.0f;
    private float CameraPosTo = -10.0f;

    private Vector3 DescriptionPosFrom = new Vector3(100f, 0f, 0f);
    private Vector3 DescriptionPosTo = new Vector3(30f, 0f, -275f);

    private bool DescriptionMoveFlag;
    private bool AirFrameMoveFlag;
	private bool isAxisInUse = false;

	private AudioSource audioSource1;

    private int CameraIndex;

    // Use this for initialization
    void Start()
    {
		AudioSource[] audioSources = GetComponents<AudioSource>();
		audioSource1 = audioSources[0];

        foreach(var Camera in AirFrame_Cam)
        {
            Camera.transform.localPosition = new Vector3(0, 0, CameraPosFrom);
        }

        //SetCameraInActive();
        //UIsActiveControl(0);

        CameraIndex = 0;
        AirFrameMoveFlag = false;
    }


    private void OnEnable()
    {
        //Descriptions[0].SetActive(true);
        //AirFrame_Cam[0].SetActive(false);

        //Pauseから復帰時にfalseになるため
        //カメラ位置が飛ばされるのを防止
        MainCam.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow)
            || Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Horizontal2") < 0
            || Input.GetAxisRaw("Horizontal3") < 0 || Input.GetAxisRaw("Horizontal4") < 0)
        {
			if (!isAxisInUse) {
				SetCameraInActive();
				MainCam.SetActive(true);
			
				isAxisInUse = true;
			}
        }

        if (GalleryCanvas.canStoryBegin)
        {
            //AirFrame_Pos[CameraIndex].z = CameraPosFrom;
            //AirFrame_Cam[CameraIndex].transform.localPosition = AirFrame_Pos[CameraIndex];

            SetCameraInActive();
            UIsActiveControl(0);
        }
       
		if (AirFrameMoveFlag)
		{
			AirFrame_Cam[CameraIndex].gameObject.transform.parent.gameObject.transform.FindChild("AirFrame").transform.Rotate(new Vector3(0.1f, 0.2f, 0));
		}

        if( (Input.GetKeyUp(KeyCode.S) || Input.GetAxisRaw("Vertical") < 0 || Input.GetAxisRaw("Vertical2") < 0) 
            && GalleryCanvas.OnStoryFlag)
        {

			if (!isAxisInUse) {
				audioSource1.PlayOneShot(audioSource1.clip);

				if (AirFrameMoveFlag == false)
				{
					AirFrameMoveFlag = true;
				}
				else
				{
					AirFrame_Cam[CameraIndex].gameObject.transform.parent.gameObject.transform.FindChild("AirFrame").transform.localRotation = new Quaternion(0, 0, 0, 0);
					AirFrameMoveFlag = false;
				}

				isAxisInUse = true;
			}

        }

        if ( (Input.GetKeyUp(KeyCode.W) || Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Vertical2") > 0)
            && GalleryCanvas.OnStoryFlag)
        {
			if (!isAxisInUse) {
				audioSource1.PlayOneShot(audioSource1.clip);

				CameraIndex++;

				if (CameraIndex >= 3) { CameraIndex = 0; }

				AirFrame_Pos[CameraIndex].z = CameraPosFrom;
				AirFrame_Cam[CameraIndex].transform.localPosition = AirFrame_Pos[CameraIndex];

				SetCameraInActive();
				UIsActiveControl(CameraIndex);

				isAxisInUse = true;
			}

        }

		if (Input.GetAxisRaw ("Horizontal2") == 0 && Input.GetAxisRaw ("Horizontal") == 0
			&& Input.GetAxisRaw ("Horizontal3") == 0 && Input.GetAxisRaw ("Horizontal4") == 0
			&& Input.GetAxisRaw ("Vertical") == 0 && Input.GetAxisRaw ("Vertical2") == 0) {

			isAxisInUse = false;
		}

        AirFrame_Pos[CameraIndex] = AirFrame_Cam[CameraIndex].transform.localPosition;
        AirFrame_Pos[CameraIndex] = MoveCamera(AirFrame_Cam[CameraIndex], AirFrame_Pos[CameraIndex]);
        AirFrame_Cam[CameraIndex].transform.localPosition = AirFrame_Pos[CameraIndex];
        Dpos = MoveDescription(Descriptions[CameraIndex].transform.localPosition);
        Descriptions[CameraIndex].transform.localPosition = Dpos;
      
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

		foreach (GameObject o in Operators)
		{
			o.SetActive(false);
			o.gameObject.GetComponentInChildren<UIMaskTransparent>().enabled = false;
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

		Operators[index].SetActive(true);
		Operators[index].gameObject.GetComponentInChildren<UIMaskTransparent>().enabled = true;

        AirFrame_Cam[index].SetActive(true);
        AirFrame_Cam[index].gameObject.transform.parent.gameObject.SetActive(true);
 
    }

}