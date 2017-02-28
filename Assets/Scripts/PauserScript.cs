using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauserScript : MonoBehaviour {
 
    private static ModalOption modalOption = new ModalOption();

    private GameObject RootObject;

    //for Option
    private GameObject BackgroundObject;

    //MainとかのUI中心以外の画面に使うときはGameObjectのModalOptionをScene内にコピーする必要あり
    //for Main
    private GameObject Player;
    private GameObject Enemy;
    private GameObject EventSystemObject;

    //for 2Player
    private GameObject Player1;
    private GameObject Player2;


    //for select, double select
    private GameObject PlayerSelectControllerObject;
    private GameObject CameraObject;
    private GameObject CameraObject2;

    //for stageselect
    private GameObject StageSelectControllerObject;

    public bool isModalOption;
    private bool isPause;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        
        Debug.Log(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        SelectObjectDependOnSceneName(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

        Debug.Log(!ModalOption.isModalSetActive);

    }


    //TODO: 例外処理
    private void SelectObjectDependOnSceneName(string SceneName)
    {
        GameObject RootObject;
        switch (SceneName) {
            case "Title":
                RootObject = GameObject.Find("TitleCanvas");

                GetComponentInParentAndChildren<FadeScript>(RootObject).enabled = !ModalOption.isModalSetActive;
                GetComponentInParentAndChildren<TitleScene>(RootObject).enabled = !ModalOption.isModalSetActive;

                break;
            case "SelectMenu":
                RootObject = GameObject.Find("SelectCanvas");

                GetComponentInParentAndChildren<CanvasRotate>(RootObject).enabled = !ModalOption.isModalSetActive;
                GetComponentInParentAndChildren<CircularUI>(RootObject).enabled = !ModalOption.isModalSetActive;

                break;
            case "Gallery":
                RootObject = GameObject.Find("GalleryCanvas");

                //どのUIgroupをSetActiveにするかを保存する
                GetComponentInParentAndChildren<ChangeCameraOnGallery>(RootObject).enabled = !ModalOption.isModalSetActive;
                GetComponentInParentAndChildren<GalleryCanvas>(RootObject).enabled = !ModalOption.isModalSetActive;
                //GetComponentInParentAndChildren<GalleryCoverFlow>(RootObject).enabled = !ModalOption.isModalSetActive;

                break;
            case "Option":
                RootObject = GameObject.Find("OptionCanvas");
                //GetComponent<OptionRotate>().enabled = !ModalOption.isPause;

                GetComponentInParentAndChildren<OptionRotate>(RootObject).enabled = !ModalOption.isModalSetActive;
                GetComponentInParentAndChildren<CircularOption>(RootObject).enabled = !ModalOption.isModalSetActive;

                GetComponentInParentAndChildren<SetLineScale>(RootObject).enabled = !ModalFlag;

                if (GetComponentInParentAndChildren<keyConfigCoverFlow>(RootObject) != null)
                    GetComponentInParentAndChildren<keyConfigCoverFlow>(RootObject).enabled = !ModalOption.isModalSetActive;
                if (GetComponentInParentAndChildren<KeyConfigBehaviour>(RootObject) != null)
                    GetComponentInParentAndChildren<KeyConfigBehaviour>(RootObject).enabled = !ModalOption.isModalSetActive;

                if (GetComponentInParentAndChildren<ChangeBGMVal>(RootObject) != null)
                    GetComponentInParentAndChildren<ChangeBGMVal>(RootObject).enabled = !ModalOption.isModalSetActive;
                if (GetComponentInParentAndChildren<ChangeDifficulty>(RootObject) != null)
                    GetComponentInParentAndChildren<ChangeDifficulty>(RootObject).enabled = !ModalOption.isModalSetActive;
                if (GetComponentInParentAndChildren<ChangeDIM>(RootObject) != null)
                    GetComponentInParentAndChildren<ChangeDIM>(RootObject).enabled = !ModalOption.isModalSetActive;
                if (GetComponentInParentAndChildren<ChangeVoiceVolume>(RootObject) != null)
                    GetComponentInParentAndChildren<ChangeVoiceVolume>(RootObject).enabled = !ModalOption.isModalSetActive;
                if (GetComponentInParentAndChildren<ChangeSEVolume>(RootObject) != null)
                    GetComponentInParentAndChildren<ChangeSEVolume>(RootObject).enabled = !ModalOption.isModalSetActive;
                    break;
            case "Main":

                //validate Null Exception
                Player = GameObject.Find("UNICORN1playMode");

                RootObject = GameObject.Find("Canvas");

                GetComponentInParentAndChildren<LockOnChangeTest>(RootObject).enabled = !ModalFlag;

                EventSystemObject = GameObject.Find("EventSystem");

                if (GetComponentInParentAndChildren<Time_ScoreScript>(EventSystemObject) != null)
                    GetComponentInParentAndChildren<Time_ScoreScript>(EventSystemObject).enabled = !ModalFlag;

                if (GetComponentInParentAndChildren<EnemyInstantiate>(EventSystemObject) != null)
                    GetComponentInParentAndChildren<EnemyInstantiate>(EventSystemObject).enabled = !ModalFlag;

                int SelectNumber = PlayerSelectController.Selectnumber();

                switch (SelectNumber) {
                    case 1:
                        //BANSHEE
                        Player = GameObject.Find("BANSHEE1playMode");
                        break;
                    case 2:
                        //UNICORN
                        Player = GameObject.Find("UNICORN1playMode");
                        break;
                    case 3:
                        //PHOENIX
                        Player = GameObject.Find("PHENEX1playMode");
                        break;
                    default: break;

                }
                
                if(Player != null)
                {
                    GetComponentInParentAndChildren<PlayerAp>(Player).enabled = !ModalFlag;
                    GetComponentInParentAndChildren<PlayerMove>(Player).enabled = !ModalFlag;
                    GetComponentInParentAndChildren<PlayerRotate>(Player).enabled = !ModalFlag;
                    GetComponentInParentAndChildren<PlayerMotion>(Player).enabled = !ModalFlag;
                    GetComponentInParentAndChildren<PlayerShoot>(Player).enabled = !ModalFlag;

                    GetComponentInParentAndChildren<LockOn>(Player).enabled = !ModalFlag;
                    GetComponentInParentAndChildren<Compass>(Player).enabled = !ModalFlag;
                    GetComponentInParentAndChildren<BoostEffect>(Player).enabled = !ModalFlag;
                    GetComponentInParentAndChildren<BattleManager>(Player).enabled = !ModalFlag;

                    GetComponentInParentAndChildren<ScreenOverlayManager>(Player).enabled = !ModalFlag;
                    GetComponentInParentAndChildren<CamVibrationManager>(Player).enabled = !ModalFlag;
                }

                Enemy = GameObject.Find("Enemy");

                GetComponentInParentAndChildren<Enemy>(Enemy).enabled = !ModalFlag;
                GetComponentInParentAndChildren<Marker>(Enemy).enabled = !ModalFlag;

                break;
            case "2PlayerMode":
                RootObject = GameObject.Find("Canvas");
                if(GetComponentInParentAndChildren<Time2PMode>(RootObject) != null)
                    GetComponentInParentAndChildren<LockOnChangeTest>(RootObject).enabled = !ModalFlag;

                EventSystemObject = GameObject.Find("EventSystem");

                int SelectNumber1 = DoublePlayerSelectController.Selectnumber1();
                int SelectNumber2 = DoublePlayerSelectController.Selectnumber2();

                switch (SelectNumber1)
                {
                    case 1:
                        //BANSHEE
                        Player = GameObject.Find("BANSHEE1");
                        GetComponentInParentAndChildren<PlayerShoot_B1>(Player).enabled = !ModalFlag;
                        GetComponentInParentAndChildren<PlayerMove_B1>(Player).enabled = !ModalFlag;

                        break;
                    case 2:
                        //UNICORN
                        Player = GameObject.Find("UNICORN1");
                        GetComponentInParentAndChildren<PlayerShoot_U1>(Player).enabled = !ModalFlag;
                        GetComponentInParentAndChildren<PlayerMove_U1>(Player).enabled = !ModalFlag;

                        break;
                    case 3:
                        //PHOENIX
                        Player = GameObject.Find("PHENEX1");
                        GetComponentInParentAndChildren<PlayerShoot_P1>(Player).enabled = !ModalFlag;
                        GetComponentInParentAndChildren<PlayerMove_P1>(Player).enabled = !ModalFlag;

                        break;
                    default: break;

                }

                switch (SelectNumber2)
                {
                    case 1:
                        //BANSHEE
                        Player = GameObject.Find("BANSHEE2");
                        GetComponentInParentAndChildren<PlayerShoot_B2>(Player).enabled = !ModalFlag;
                        GetComponentInParentAndChildren<PlayerMove_B2>(Player).enabled = !ModalFlag;

                        break;
                    case 2:
                        //UNICORN
                        Player = GameObject.Find("UNICORN2");
                        GetComponentInParentAndChildren<PlayerShoot_U2>(Player).enabled = !ModalFlag;
                        GetComponentInParentAndChildren<PlayerMove_U2>(Player).enabled = !ModalFlag;

                        break;
                    case 3:
                        //PHOENIX
                        Player = GameObject.Find("PHENEX2");
                        GetComponentInParentAndChildren<PlayerShoot_P2>(Player).enabled = !ModalFlag;
                        GetComponentInParentAndChildren<PlayerMove_P2>(Player).enabled = !ModalFlag;

                        break;
                    default: break;

                }

                if (Player1 != null)
                {
                    GetComponentInParentAndChildren<PlayerAp1>(Player1).enabled = !ModalFlag;
                    GetComponentInParentAndChildren<PlayerMotion1>(Player1).enabled = !ModalFlag;

                    GetComponentInParentAndChildren<LockOn1>(Player1).enabled = !ModalFlag;
                    GetComponentInParentAndChildren<BoostEffect1>(Player1).enabled = !ModalFlag;
                    GetComponentInParentAndChildren<Compass>(Player1).enabled = !ModalFlag;
                    GetComponentInParentAndChildren<Marker1>(Player1).enabled = !ModalFlag;

                    //子オブジェクト
                    GetComponentInParentAndChildren<BattleManager_Battle1>(Player1).enabled = !ModalFlag;
                    GetComponentInParentAndChildren<lose1>(Player1).enabled = !ModalFlag;

                    //GetComponentInParentAndChildren<ScreenOverlay>(Player1).enabled = !ModalFlag;
                    GetComponentInParentAndChildren<ScreenOverlayManager>(Player1).enabled = !ModalFlag;
                    GetComponentInParentAndChildren<CamVibrationManager>(Player1).enabled = !ModalFlag;
                }

                if (Player2 != null)
                {
                    GetComponentInParentAndChildren<PlayerAp2>(Player2).enabled = !ModalFlag;
                    GetComponentInParentAndChildren<PlayerMotion2>(Player2).enabled = !ModalFlag;

                    GetComponentInParentAndChildren<LockOn2>(Player2).enabled = !ModalFlag;
                    GetComponentInParentAndChildren<BoostEffect2>(Player2).enabled = !ModalFlag;
                    GetComponentInParentAndChildren<Compass>(Player2).enabled = !ModalFlag;
                    GetComponentInParentAndChildren<Marker2>(Player2).enabled = !ModalFlag;

                    //子オブジェクト
                    GetComponentInParentAndChildren<BattleManager_Battle2>(Player2).enabled = !ModalFlag;
                    GetComponentInParentAndChildren<lose2>(Player2).enabled = !ModalFlag;

                    //GetComponentInParentAndChildren<ScreenOverlay>(Player2).enabled = !ModalFlag;
                    GetComponentInParentAndChildren<ScreenOverlayManager>(Player2).enabled = !ModalFlag;
                    GetComponentInParentAndChildren<CamVibrationManager>(Player2).enabled = !ModalFlag;
                }

                break;
            case "stageselect":
                StageSelectControllerObject = GameObject.Find("StageSelectController");
                GetComponentInParentAndChildren<StageSelectController>(StageSelectControllerObject).enabled = !ModalFlag;

                CameraObject = GameObject.Find("Camera");
                GetComponentInParentAndChildren<CameraController>(CameraObject).enabled = !ModalFlag;
                break;
            case "select":
                CameraObject = GameObject.Find("Camera");
                GetComponentInParentAndChildren<CameraController>(CameraObject).enabled = !ModalFlag;

                PlayerSelectControllerObject = GameObject.Find("PlayerSelectController");
                GetComponentInParentAndChildren<PlayerSelectController>(PlayerSelectControllerObject).enabled = !ModalFlag;

                break;
            case "selectsceneDouble":
                CameraObject = GameObject.Find("Camera");
                GetComponentInParentAndChildren<MainCameraController>(CameraObject).enabled = !ModalFlag;

                CameraObject2 = GameObject.Find("Camera2");
                GetComponentInParentAndChildren<MainCameraController2>(CameraObject).enabled = !ModalFlag;

                PlayerSelectControllerObject = GameObject.Find("PlayerSelectController");
                GetComponentInParentAndChildren<DoublePlayerSelectController>(PlayerSelectControllerObject).enabled = !ModalFlag;

                break;
            case "Result":
                RootObject = GameObject.Find("Canvas");
                GetComponentInParentAndChildren<ResultScript>(RootObject).enabled = !ModalFlag;

                break;

            default: break;
                 
        }

    }

    //  GameObjectExtension.cs
    //  http://kan-kikuchi.hatenablog.com/entry/GetComponentInParentAndChildren
    //
    //  Created by kikuchikan on 2015.08.25.

    /// <summary>
    /// 親や子オブジェクトも含めた範囲から指定のコンポーネントを取得する
    /// </summary>
    public static T GetComponentInParentAndChildren<T>(GameObject gameObject) 
    {

        if (gameObject.GetComponentInParent<T>() != null)
        {
            return gameObject.GetComponentInParent<T>();
        }
        if (gameObject.GetComponentInChildren<T>() != null)
        {
            return gameObject.GetComponentInChildren<T>();
        }

        return gameObject.GetComponent<T>();
    }
}
