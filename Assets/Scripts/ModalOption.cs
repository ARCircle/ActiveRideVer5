using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ModalOption : MonoBehaviour {
    public GameObject ModalCanvas;
	public List<Button> ModalOptionButtons = new List<Button>();
	public List<GameObject> ModalOptionDescriptions = new List<GameObject>();

	public static bool isModalSetActive;
    public static bool isPause;
	private bool isAxisInUse = false;

	private int bCenterIndex;
	private int MaxCountOfModalOptionButtons;

	private int Verify_bIndex(int bCenterIndex)
	{
		if(bCenterIndex >= MaxCountOfModalOptionButtons)
		{
			bCenterIndex = 0;
		}
		if (bCenterIndex < 0)
		{
			bCenterIndex = MaxCountOfModalOptionButtons - 1;
		}
		return bCenterIndex;
	}

	private void SetActiveDescriptions(int index){
		foreach (GameObject d in ModalOptionDescriptions) {
			d.SetActive (false);
		}
		ModalOptionDescriptions [index].SetActive (true);
	}

    private void QuitGame()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #elif UNITY_WEBPLAYER
	        Application.OpenURL("https://www.google.co.jp/");
    #else
	        Application.Quit();
    #endif
    }

    private void SceneManager(int index)
	{
        Debug.Log("SceneManager:" + index);

        switch (index)
		{
		case 0:
                UnityEngine.SceneManagement.SceneManager.LoadScene("title");

                /*
			    CameraFade.StartAlphaFade(Color.black, false, 0.6f, 0.6f, () =>
				{
					UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
					//MainSoundObject.SetActive (false);
				});
                */
                break;
		case 1:
                UnityEngine.SceneManagement.SceneManager.LoadScene("SelectMenu");
                /*
                CameraFade.StartAlphaFade(Color.black, false, 0.6f, 0.6f, () =>
				{
					UnityEngine.SceneManagement.SceneManager.LoadScene("SelectMenu");
					//MainSoundObject.SetActive (false);
				});
                */
			    break;
		case 2:
                //ゲーム終了処理
                //ModalCanvas.SetActive(false);
                QuitGame();

                break;
            default: break;

		}
	}

    // Use this for initialization
    void Start () {
		
	    ModalCanvas.SetActive(false);
		isModalSetActive = false;
        isPause = false;

		bCenterIndex = 1;
		MaxCountOfModalOptionButtons = ModalOptionButtons.Count;

		//初期のボタンの選択処理
		SetActiveDescriptions (bCenterIndex);

    }
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp(KeyCode.O) || Input.GetButtonUp("Option"))
		{
            if (isModalSetActive == false) {
                isPause = true;
                ModalCanvas.SetActive(true);
                isModalSetActive = true;
                ModalOptionButtons[bCenterIndex].Select();
            }
            else
            {
                isPause = false;
                ModalCanvas.SetActive(false);
                isModalSetActive = false;
            }

        }

		if (isModalSetActive == true) {

            if (Input.GetKeyUp(KeyCode.A) || Input.GetAxisRaw("Horizontal3") > 0 || Input.GetAxisRaw("Horizontal4") > 0)
			{

				if (isAxisInUse) {
					bCenterIndex--;
					bCenterIndex = Verify_bIndex (bCenterIndex);
					ModalOptionButtons [bCenterIndex].Select ();
					SetActiveDescriptions (bCenterIndex);
					
					isAxisInUse = true;
				}
			}

			if (Input.GetKeyUp(KeyCode.D) || Input.GetAxisRaw("Horizontal3") < 0 || Input.GetAxisRaw("Horizontal4") < 0)
			{
				if (isAxisInUse) {

					bCenterIndex++;
					bCenterIndex = Verify_bIndex (bCenterIndex);
					ModalOptionButtons [bCenterIndex].Select ();
					SetActiveDescriptions (bCenterIndex);

					isAxisInUse = true;
				}
			}


			//ボタンクリック時の動作
			if (Input.GetKeyUp (KeyCode.W) || Input.GetButtonUp("Submit")) {
                Debug.Log(bCenterIndex);
                ModalOptionButtons[bCenterIndex].onClick.Invoke();
				SceneManager(bCenterIndex);
                ModalCanvas.SetActive(false);
                isModalSetActive = false;
            }

			if (Input.GetAxisRaw ("Horizontal3") == 0 && Input.GetAxisRaw ("Horizontal4") == 0) {
				isAxisInUse = false;
			}
				
		}

	}
	void OnButtonClick() {
		UIRipple:Update ();
	}
}
