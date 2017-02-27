using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasRotate : MonoBehaviour {

	public List<DescriptMenu> Descript = new List<DescriptMenu>();
	public List<ButtonMenu> BtnList = new List<ButtonMenu>();

	public GameObject Axis;
	public GameObject Buttons;
	public GameObject Descriptions;

	GameObject MainSoundObject; 

	private int Circle_val = 360;
	public int AngleParUpdate = 6;
	private static int AngleParButton;

	public int dIndex;
	public int bIndex;

	private AudioSource audioSource1;
	private AudioSource audioSource2;

	private Vector3 pos;

	public float smooth_move = 1.5f;

	private static float angle_z = 0;

    private bool RotateFlag_plus;
    private bool RotateFlag_minus;
    private int RotatedAngle_z = 0;

    // Use this for initialization
    void Start () {

        //ModalOptionの加算ロード
        UnityEngine.SceneManagement.SceneManager.LoadScene("ModalOption", LoadSceneMode.Additive);

        AudioSource[] audioSources = GetComponents<AudioSource>();
		audioSource1 = audioSources[0];
		audioSource2 = audioSources[1];

		MainSoundObject = GameObject.Find ("MainSoundObject");

		dIndex = 0;
		bIndex = 0;

        //規定位置へDescriptの各要素を移動
		//Patch Work (need to fix)
		foreach (var d in Descript)
		{
			pos = d.description.transform.position;
			pos.x = 2;
			d.description.transform.position = pos;

		}

    }

    //Menuの選択されたindexに基づいてシーン遷移
    //シーンはフェード表示
	private void SceneManager(int index)
	{
		switch (index)
		{
		case 0:
			CameraFade.StartAlphaFade(Color.black, false, 0.6f, 0.6f, () =>
				{
					UnityEngine.SceneManagement.SceneManager.LoadScene("select");
					//MainSoundObject.SetActive (false);
				});
			break;
		case 1:
			CameraFade.StartAlphaFade(Color.black, false, 0.6f, 0.6f, () =>
				{
					UnityEngine.SceneManagement.SceneManager.LoadScene("selectsceneDoble");
					//MainSoundObject.SetActive (false);
				});
			break;
		case 2:
			CameraFade.StartAlphaFade(Color.black, false, 0.6f, 0.6f, () =>
				{
					UnityEngine.SceneManagement.SceneManager.LoadScene("Option");
					//MainSoundObject.SetActive (false);				
				});
			break;
		case 3:
			CameraFade.StartAlphaFade(Color.black, false, 0.6f, 0.6f, () =>
				{
					//Creditは作る?
					UnityEngine.SceneManagement.SceneManager.LoadScene("SelectMenu");
				});
			break;
		case 4:
			CameraFade.StartAlphaFade(Color.black, false, 0.6f, 0.6f, () =>
				{
					UnityEngine.SceneManagement.SceneManager.LoadScene("Gallery");
					//MainSoundObject.SetActive (false);				
				});
			break;
		default: break;

		}
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
		if ((angle_z >= AngleParButton/2) && (angle_z < AngleParButton + AngleParButton/2))
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
    void Update () {

        //キー入力を受け付け, フラグ値を指定
		//TODO: マウスホイールにも対応する?
		if ( (Input.GetKeyUp(KeyCode.S) && !ModalOption.isModalSetActive) ||
			(Input.GetAxis("Mouse ScrollWheel") < 0f ) )
		{
			audioSource2.PlayOneShot(audioSource2.clip);
			if (!RotateFlag_minus) RotateFlag_plus = true;
		}

		if ( (Input.GetKeyUp(KeyCode.W) && !ModalOption.isModalSetActive) ||
			( Input.GetAxis("Mouse ScrollWheel") > 0f ) )
		{
			audioSource2.PlayOneShot(audioSource2.clip);
			if (!RotateFlag_plus) RotateFlag_minus = true;
		}

		angle_z = Buttons.transform.localEulerAngles.z;

        //軸とボタンを回転
        if (RotateFlag_plus)
		{
			Buttons.transform.Rotate(new Vector3(0, 0, AngleParUpdate));
			Axis.transform.Rotate(new Vector3(0, 0, -AngleParUpdate));
			RotatedAngle_z++;

			if (Input.GetKeyUp(KeyCode.W)) 
			{
				RotateFlag_plus = true;
				RotateFlag_minus = false;
			}
		}
		if (RotateFlag_minus)
		{
			Buttons.transform.Rotate(new Vector3(0, 0, -AngleParUpdate));
			Axis.transform.Rotate(new Vector3(0, 0, AngleParUpdate));
			RotatedAngle_z++;

			if (Input.GetKeyUp(KeyCode.S)) 
			{
				RotateFlag_plus = false;
				RotateFlag_minus = true;
			}
		}
        //終了条件
		if (RotatedAngle_z >= Circle_val/(Descript.Count * AngleParUpdate))
		{
			RotatedAngle_z = 0;
			RotateFlag_minus = false;
			RotateFlag_plus = false;
		}


		dIndex = CheckViewDescriptions(angle_z);

        //説明を横からスライドイン
		foreach (var d in Descript) {

            pos = d.description.transform.position;

            //選択された説明をスライドイン
            if (d.index == dIndex)
			{
				if (pos.x >= 0) {
					pos.x -= 1;
				}
                //最前面表示
				d.description.transform.SetAsLastSibling();
			}
            //x=11よりはスライドしない
			else
			{
				pos.x = 2;
			}

            d.description.transform.position = pos;

        }


        //シーン遷移
        if (Input.GetKeyUp(KeyCode.Q)  || Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.P) || Input.GetKeyUp(KeyCode.Return))
		{
			audioSource1.PlayOneShot(audioSource1.clip);
			SceneManager(dIndex);
		}

	}
    
	IEnumerator wait()
	{
		yield return new WaitForSeconds(0.01f);
	}
}

//個々の説明を格納するリスト
[System.Serializable]
public class DescriptMenu
{
	public GameObject description;
	public int index;
}

//個々のボタンを格納するリスト
[System.Serializable]
public class ButtonMenu
{
	public GameObject button;
}