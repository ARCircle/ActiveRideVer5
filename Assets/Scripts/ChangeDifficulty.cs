using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDifficulty : MonoBehaviour {
    public GameObject ModalCanvas;
    public List<UnityEngine.UI.Button> DiffiultyButtons = new List<UnityEngine.UI.Button>();
    private List<GameObject> ModalOptionDescRiptions = new List<GameObject>();

    //public bool isModalSetActive;

    //Difficulty
    //0: easy 1:normal 2:hard
    public int Difficulty;
    public int getDifficulty
    {
        private set { Difficulty = value; }
        get { return Difficulty; }
    }

    private int bCenterIndex;
    private int MaxCountOfModalOptionButtons;

    private int Verify_bIndex(int bCenterIndex)
    {
        if (bCenterIndex >= MaxCountOfModalOptionButtons)
        {
            bCenterIndex = 0;
        }
        if (bCenterIndex < 0)
        {
            bCenterIndex = MaxCountOfModalOptionButtons - 1;
        }
        return bCenterIndex;
    }

    private void SetActiveDescriptions(int index)
    {
        foreach (GameObject d in ModalOptionDescRiptions)
        {
            d.SetActive(false);
        }
        ModalOptionDescRiptions[index].SetActive(true);
    }

    private void SceneManager(int index)
    {
        switch (index)
        {
            case 0:
                CameraFade.StartAlphaFade(Color.black, false, 0.6f, 0.6f, () =>
                {
                    //MainSoundObject.SetActive (false);
                });
                break;
            case 1:
                CameraFade.StartAlphaFade(Color.black, false, 0.6f, 0.6f, () =>
                {
                    //MainSoundObject.SetActive (false);
                });
                break;
            case 2:
                //ポーズ処理
                break;
            default: break;

        }
    }

    // Use this for initialization
    void Start()
    {     
        bCenterIndex = 1;
        MaxCountOfModalOptionButtons = DiffiultyButtons.Count;
        DiffiultyButtons[bCenterIndex].Select();

        Difficulty = bCenterIndex;

        //初期のボタンの選択処理
        //SetActiveDescriptions(bCenterIndex);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.A) || Input.GetAxisRaw("Horizontal3") > 0 || Input.GetAxisRaw("Horizontal4") > 0)
        {
            bCenterIndex--;
            bCenterIndex = Verify_bIndex(bCenterIndex);

            DiffiultyButtons[bCenterIndex].Select();

        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetAxisRaw("Horizontal3") < 0 || Input.GetAxisRaw("Horizontal4") < 0)
        {
            //ModalCanvas.SetActive(true);
            //isModalSetActive = true;
            bCenterIndex++;
            bCenterIndex = Verify_bIndex(bCenterIndex);

            DiffiultyButtons[bCenterIndex].Select();
                //SetActiveDescriptions(bCenterIndex);
        }

        //ボタンクリック時の動作
        if (Input.GetKeyUp(KeyCode.W) || Input.GetAxisRaw("Vertical3") < 0 || Input.GetAxisRaw("Vertical4") < 0)
        {
            DiffiultyButtons[bCenterIndex].onClick.Invoke();
                //SceneManager(bCenterIndex);
        }

        Difficulty = bCenterIndex;
    }
    void OnButtonClick()
    {
        //UIRipple: Update();
    }
}
