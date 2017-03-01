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

    public static bool OnPictureFlag = false;
    public static bool OnStoryFlag = false;

    //private bool isUpMaxOnPicture = false;
    //private bool isDownMaxOnPicture = false;

    //private bool isUpMaxOnStory = false;
    //private bool isDownMaxOnStory = false;

	private Vector3 pos;

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

    }

    private void OnEnable()
    {
        //Pause時にenabled=falseになるため
        if(OnStoryFlag)GetComponent<ChangeCameraOnGallery>().enabled = true;
    }

    // Update is called once OnStoryFlag frame
    void Update()
    {
        pos = canvas.transform.position;

        if (OnStoryFlag)
        {
            //TODO: 初期Canvasを非表示, Story用のCanvas表示
            //canvas.SetActive(false);
            GetComponent<ChangeCameraOnGallery>().enabled = true;
        }else
        {
            GetComponent<ChangeCameraOnGallery>().enabled = false;
        }

        if (OnPictureFlag)
        {

        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!OnStoryFlag)
            {
                OnPictureFlag = true;
                EachUIGroupSetActive(1);
            }
            else
            {
                OnPictureFlag = false;
                InitializePos(pos);
                EachUIGroupSetActive(0);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!OnPictureFlag)
            {
                OnStoryFlag = true;
                EachUIGroupSetActive(2);
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
                wait();
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

		if ( (OnPictureFlag && Input.GetKeyDown(KeyCode.LeftArrow)) ||
			(OnStoryFlag && Input.GetKeyDown(KeyCode.RightArrow)) )
        {
            OnPictureFlag = false;
            OnStoryFlag = false;
 
            pos.x = DefaultCubePos.x;
            pos.y = DefaultCubePos.y;
            canvas.transform.position = pos;
        }

		if (Input.GetKeyUp (KeyCode.Q)) {

			CameraFade.StartAlphaFade(Color.black, false, 0.6f, 0.6f, () =>
				{
					UnityEngine.SceneManagement.SceneManager.LoadScene("SelectMenu");
				});
		}
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.01f);
    }
}
