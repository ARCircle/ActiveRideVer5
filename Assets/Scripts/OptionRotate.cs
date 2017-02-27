using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionRotate : MonoBehaviour
{
    public List<GameObject> BtnList = new List<GameObject>();
	public List<GameObject> Settings = new List<GameObject>();
    private List<GameObject> buttons;

    public GameObject Axis;
    public GameObject Buttons;
    //public GameObject Descriptions;

    GameObject MainSoundObject;

    private int Circle_val = 360;
    public int AngleParUpdate = 6;
	private static int AngleParButton;

    private AudioSource audioSource1;
    private AudioSource audioSource2;

	public int bIndex;
    private int bCenterIndex;
    private int CenterIndex;

    private int defaultButtonOffset;

	public float smooth_move = 1.5f;

	private bool RotateFlag_plus;
	private bool RotateFlag_minus;
	private int RotatedAngle_z = 0;

	private static float angle_z = 0;

    // Use this for initialization
    void Start()
    {

		//ModalOptionの加算ロード
		UnityEngine.SceneManagement.SceneManager.LoadScene("ModalOption", LoadSceneMode.Additive);

        /*
        CenterIndex = (int)Mathf.Ceil(buttons.Count / 2);
        buttons[CenterIndex].transform.localScale = new Vector3(1.5f, 1.5f, 0);
        */
        
        AudioSource[] audioSources = GetComponents<AudioSource>();
        audioSource1 = audioSources[0];
        audioSource2 = audioSources[1];
        
        MainSoundObject = GameObject.Find("MainSoundObject");
        
        bIndex = 0;
        SetActiveSettings(bIndex);

        //Buttonの初期回転の設定(TODO)
        defaultButtonOffset = 72 / BtnList.Count;
        for (int i = 0; i < BtnList.Count; i++)
        {
            BtnList[i].transform.Rotate(new Vector3(0, 0 , defaultButtonOffset - i * Circle_val / BtnList.Count)); 
        }
    }
		

    private int Verify_bIndex(int bCenterIndex)
    {
        if(bCenterIndex > Settings.Count - 1)
        {
            bCenterIndex = 0;
        }
        if (bCenterIndex == Settings.Count - 1)
        {
            bCenterIndex = Settings.Count - 1;
        }
        if (bCenterIndex < 0)
        {
            bCenterIndex = Settings.Count - 1;
        }
        if (bCenterIndex == 0)
        {
            bCenterIndex = 0;
        }

        return bCenterIndex;

    }
    //Menuの選択されたindexに基づいてシーン遷移
    //シーンはフェード表示
    private void SceneManager(int index)
    {
 		//MainへのExit
    }

    //回転角と軸の対応付け
    private int CheckViewDescriptions(float angle)
    {
		AngleParButton = Circle_val / BtnList.Count;
		int index = 0;

		if ((angle_z < AngleParButton/2  ) || (angle_z >= Circle_val - AngleParButton/2))
        {
            index = 0;
        }
		if ((angle_z >= AngleParButton/2 ) && (angle_z < AngleParButton + AngleParButton/2))
        {
            index = 4;
        }
		if ((angle_z >= AngleParButton + AngleParButton/2) && (angle_z < AngleParButton * 2 + AngleParButton/2))
        {
            index = 3;
        }
		if ((angle_z >= AngleParButton * 2 + AngleParButton/2) && (angle_z < AngleParButton * 3 + AngleParButton/2))
        {
            index = 2;
        }
		if ((angle_z >= AngleParButton * 3 + AngleParButton/2) && (angle_z < Circle_val - AngleParButton/2))
        {
            index = 1;
        }

        return index;
    }

    // Update is called once per frame
    void Update()
    {

        //キー入力を受け付け, フラグ値を指定
        //TODO: マウスホイールにも対応する?
        if ( (Input.GetKeyUp(KeyCode.S) && !ModalOption.isModalSetActive) ||
            (Input.GetAxis("Mouse ScrollWheel") < 0f))
        {
            
            bCenterIndex--;
            bCenterIndex = Verify_bIndex(bCenterIndex);

            //Debug.Log("Center: " + bCenterIndex + "bLeft" + bLeftIndex + "bRIght" +bRightIndex );

            audioSource2.PlayOneShot(audioSource2.clip);

			SetActiveSettings (bCenterIndex);

        	if (!RotateFlag_minus) RotateFlag_plus = true;
            
    	}

        if ( (Input.GetKeyUp(KeyCode.W) && !ModalOption.isModalSetActive) ||
            (Input.GetAxis("Mouse ScrollWheel") > 0f))
        {
			
            //CenterButton.transform.Rotate(new Vector3(0, 0, -72));

            bCenterIndex++;
			bCenterIndex = Verify_bIndex(bCenterIndex);

			audioSource2.PlayOneShot(audioSource2.clip);

			SetActiveSettings (bCenterIndex);

            if (!RotateFlag_plus) RotateFlag_minus = true;

        }

		if (Input.GetKeyUp (KeyCode.Q)) {
		
			CameraFade.StartAlphaFade(Color.black, false, 0.6f, 0.6f, () =>
				{
					UnityEngine.SceneManagement.SceneManager.LoadScene("SelectMenu");
					//MainSoundObject.SetActive (false);
			});
		}

        angle_z = Buttons.transform.localEulerAngles.z;
        
        //軸とボタンを回転
        if (RotateFlag_plus)
        {
            Buttons.transform.Rotate(new Vector3(0, 0, -AngleParUpdate));
            //Axis.transform.Rotate(new Vector3(0, 0, -AngleParUpdate));
            RotatedAngle_z++;

            if (Input.GetKeyUp(KeyCode.W))
            {
                RotateFlag_plus = true;
                RotateFlag_minus = false;
            }
        }
        if (RotateFlag_minus)
        {
            Buttons.transform.Rotate(new Vector3(0, 0, AngleParUpdate));
            //Axis.transform.Rotate(new Vector3(0, 0, AngleParUpdate));
            RotatedAngle_z++;

            if (Input.GetKeyUp(KeyCode.S))
            {
                RotateFlag_plus = false;
                RotateFlag_minus = true;
            }
        }
        //終了条件
        
        if (RotatedAngle_z >= Circle_val / (BtnList.Count * AngleParUpdate))
        {
            RotatedAngle_z = 0;
            RotateFlag_minus = false;
            RotateFlag_plus = false;
        }
        
       
        //シーン遷移
        if (Input.GetKeyUp(KeyCode.Q) || Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.P) || Input.GetKeyUp(KeyCode.Return))
        {
            audioSource1.PlayOneShot(audioSource1.clip);
            //SceneManager(bIndex);
        }

    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.01f);
    }

	private void SetActiveSettings(int sCenterIndex){
		foreach(GameObject s in Settings){
			s.SetActive (false);
		}
        sCenterIndex = Verify_bIndex(sCenterIndex);
		Settings[sCenterIndex].SetActive(true);
	}
}

//循環リスト
public class CycleSequence<T> : IEnumerable<T>
{
    protected List<T> list;
    public CycleSequence(List<T> reel)
    {
        list = reel;
    }
    public IEnumerator<T> GetEnumerator()
    {
        while (true)
        {
            foreach(T rl in list)
            {
                yield return rl;
            }
        }
    }
    IEnumerator IEnumerable.GetEnumerator( )
    {
        return this.GetEnumerator();
    }
}

//個々の説明を格納するリスト
[System.Serializable]
public class DescriptOption
{
    public GameObject description;
    public int index;
}

//個々のボタンを格納するリスト
[System.Serializable]
public class ButtonOption
{
    public GameObject button;
}