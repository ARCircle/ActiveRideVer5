using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GalleryCanvas : MonoBehaviour {

    public GameObject canvas;

    public LinkedList<GameObject> GalleryList = new LinkedList<GameObject>();
    public List<GameObject> Descriptions = new List<GameObject>();

    private int AngleUpdate = 90;
    private Vector3 GalleryPanelSize = new Vector3(500, 500, 0);

    public static bool OnPictureFlag;
    public static bool OnStoryFlag;

    private bool UIGroupSetActiveOnce;
    public static bool canStoryBegin;

    public GameObject Ring_Prefab;
    private GameObject Ring_Instance;

    private Vector3 pos;

    private float TimeLeft;

    public List<GameObject> EachUIGroup = new List<GameObject>();

    //循環連結リストの作成
    void InitializeCicrularlyLinkedList(LinkedList<GameObject> ll)
    {
        ll.AddLast(ll.First);
    }

    private Vector3 DefaultCubePos;

    void InitializePos(Vector3 pos)
    {
        OnPictureFlag = false;
        OnStoryFlag = false;
        pos.x = DefaultCubePos.x;
        pos.y = DefaultCubePos.y;
        canvas.transform.position = pos;
    }

    void EachUIGroupSetActive(int flag)
    {
        foreach(var UI in EachUIGroup)
        {
            UI.SetActive(false);
        }
        EachUIGroup[flag].SetActive(true);
    }

    // Use this for initialization
    void Start () {

        //ModalOptionの加算ロード
        UnityEngine.SceneManagement.SceneManager.LoadScene("ModalOption", LoadSceneMode.Additive);

        //Inactivate Descriptions for Story
        foreach (GameObject d in Descriptions)
        {
            d.SetActive(false);
        }

        pos = canvas.transform.position;
        DefaultCubePos = pos;
        GetComponent<ChangeCameraOnGallery>().enabled = false;

        EachUIGroupSetActive(0);

        TimeLeft = 1.0f;

        OnPictureFlag = false;
        OnStoryFlag = false;
        canStoryBegin = false;
    }

    private void OnEnable()
    {
        //Pause時にenabled=falseになるため
        if(OnStoryFlag)
            GetComponent<ChangeCameraOnGallery>().enabled = false;
    }

    // Update is called once OnStoryFlag frame
    void Update()
    {
        canStoryBegin = false;
        pos = canvas.transform.position;
        //GetComponent<ChangeCameraOnGallery>().enabled = false;
        if (OnStoryFlag)
        {
            if (UIGroupSetActiveOnce)
            {
                if (TimeLeft >= 0.0)
                {
                    TimeLeft -= Time.deltaTime;
                }
                else
                {
                    TimeLeft = 1.0f;
                    Destroy(Ring_Instance);
                    UIGroupSetActiveOnce = false;
                    EachUIGroupSetActive(2);

                    if (!canStoryBegin) { canStoryBegin = true; } else { canStoryBegin = false; }
                    GetComponent<ChangeCameraOnGallery>().enabled = true;
                }
            }
        }
        else
        {
            GetComponent<ChangeCameraOnGallery>().enabled = false;
        }

        if (OnPictureFlag)
        {
            if (UIGroupSetActiveOnce)
            {
                //Debug.Log(TimeLeft);
                if (TimeLeft >= 0.0)
                {
                    TimeLeft -= Time.deltaTime;
                }
                else
                {
                    Destroy(Ring_Instance);
                    EachUIGroupSetActive(1);
                    TimeLeft = 1.0f;
                    UIGroupSetActiveOnce = false;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) 
            || Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Horizontal2") < 0)
        {
            if (!OnStoryFlag)
            {
                OnPictureFlag = true;
                UIGroupSetActiveOnce = true;
                Ring_Instance = CreateInstance(EachUIGroup[0], "Arrow1");
                //EachUIGroupSetActive(0);
            }
            else
            {
                OnPictureFlag = false;
                InitializePos(pos);
                EachUIGroupSetActive(0);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)
            || Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal2") > 0)
        {
            if (!OnPictureFlag)
            {
                if (canStoryBegin) canStoryBegin = false;
                OnStoryFlag = true;
                UIGroupSetActiveOnce = true;
                Ring_Instance = CreateInstance(EachUIGroup[0], "Arrow2");
                GetComponent<ChangeCameraOnGallery>().enabled = false;
            }
            else
            {
                OnStoryFlag = false;
                InitializePos(pos);
                EachUIGroupSetActive(0);
            }
        }

        if (OnPictureFlag && !OnStoryFlag)
        {

            if (pos.x >= DefaultCubePos.x - GalleryPanelSize.x)
            {
                pos.x = pos.x - 10;
                canvas.transform.position = pos;
            }
        }
        if (!OnPictureFlag && OnStoryFlag)
        {

            if (pos.x <= DefaultCubePos.x + GalleryPanelSize.x)
            {
                pos.x = pos.x + 10;
                canvas.transform.position = pos;
                //wait();
            }
        }

		if ( (OnPictureFlag && (Input.GetKeyDown(KeyCode.LeftArrow)
            || Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal2") > 0)) 
            || (OnStoryFlag && (Input.GetKeyDown(KeyCode.RightArrow)
            || Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Horizontal2") < 0)
            ) )
        {
            OnPictureFlag = false;
            OnStoryFlag = false;
 
            pos.x = DefaultCubePos.x;
            pos.y = DefaultCubePos.y;
            canvas.transform.position = pos;
        }

		if (Input.GetKeyUp (KeyCode.Q) || Input.GetButtonUp("Cancel")) {

			CameraFade.StartAlphaFade(Color.black, false, 0.6f, 0.6f, () =>
				{
					UnityEngine.SceneManagement.SceneManager.LoadScene("SelectMenu");
				});
		}
    }

    GameObject CreateInstance(GameObject EachUIGroup, string targetName)
    {
        GameObject instance;
        GameObject target = EachUIGroup.transform.FindChild(targetName).gameObject;
        instance = (GameObject)Instantiate(Ring_Prefab);
        instance.transform.parent = EachUIGroup.transform;

        instance.transform.localPosition = target.transform.localPosition;
        instance.transform.localScale = target.transform.localScale;
        instance.GetComponent<RingController>().TargetCircularObject = target;

        instance.SetActive(true);

        return instance;
        //yield return new WaitForSeconds(2f);
    }
}
