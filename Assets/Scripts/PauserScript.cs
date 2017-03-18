using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauserScript : MonoBehaviour
{
    //TODO: OnEnableが止まらない問題の解消

    private static ModalOption modalOption = new ModalOption();

    private GameObject RootObject;

    //for Option
    private GameObject BackgroundObject;

    //for Gallery
    private GameObject StoryCanvas;

    //MainとかのUI中心以外の画面に使うときはGameObjectのModalOption(Canvas)をScene内にコピーする必要あり
    //UIのみの画面ではModalOption.unityを加算ロードすればよい

    //for Main
    private GameObject Player;
    private GameObject Enemy;
    private GameObject EventSystemObject;
	private GameObject Script;

    //for 2PlayerMode
    private GameObject Player1;
    private GameObject Player2;

    //for stageselect
    private GameObject StageSelectControllerObject;

    //for select
    private GameObject PlayerSelectControllerObject;
    private GameObject CameraObject;
    //for select2player
    private GameObject CameraObject2;

    public bool isModalOption;
    private bool isPause;
	public bool isHissatsu;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {

		Debug.Log (CutinManager_hissatu.canCutIn);

        //CutInするか否かを取得
		if (isHissatsu) {
			isPause = CutinManager_hissatu.canCutIn;
		} else {
			isPause = CutinManager.canCutIn;
		}

        Debug.Log(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + "is Active, " + isModalOption);

        if (isModalOption)
        {
            SelectObjectDependOnSceneName(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, ModalOption.isModalSetActive);
        }
        else
        {
            // カットイン実装用
            // isPauseを外部スクリプトからコントロール, ポーズの呼び出し
            SelectObjectDependOnSceneName(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, isPause);
        }

    }


    //TODO: 例外処理
    public void SelectObjectDependOnSceneName(string SceneName, bool ModalFlag)
    {
        if (ModalFlag)
        {
            //TimeScaleによる停止
            Time.timeScale = 0;
        }
        if (!ModalFlag)
        {
            Time.timeScale = 1;
        }

        switch (SceneName)
        {
            case "Title":
                RootObject = GameObject.Find("TitleCanvas");

                GetComponentInParentAndChildren<FadeScript>(RootObject).enabled = !ModalFlag;
                GetComponentInParentAndChildren<TitleScene>(RootObject).enabled = !ModalFlag;
                break;
            case "SelectMenu":
                RootObject = GameObject.Find("SelectCanvas");

                GetComponentInParentAndChildren<CanvasRotate>(RootObject).enabled = !ModalFlag;
                //GetComponentInParentAndChildren<CircularUI>(RootObject).enabled = !ModalFlag;

                break;
            case "Gallery":
                RootObject = GameObject.Find("GalleryCanvas");

                //どのUIgroupをSetActiveにするかを保存する
                GetComponentInParentAndChildren<ChangeCameraOnGallery>(RootObject).enabled = !ModalFlag;
                GetComponentInParentAndChildren<GalleryCanvas>(RootObject).enabled = !ModalFlag;

                foreach (var components in GetComponentsInParentAndChildren<BackGroundController>(RootObject))
                {
                    components.enabled = !ModalFlag;
                }

                if (GetComponentInParentAndChildren<GalleryCoverFlow>(RootObject) != null)
                    GetComponentInParentAndChildren<GalleryCoverFlow>(RootObject).enabled = !ModalFlag;

                foreach (var components in GetComponentsInParentAndChildren<ShowUIText>(RootObject))
                {
                    components.enabled = !ModalFlag;
                }
                foreach (var components in GetComponentsInParentAndChildren<UIMaskTransparent>(RootObject))
                {
                    components.enabled = !ModalFlag;
                }

                StoryCanvas = GameObject.Find("StoryCanvases");

                GetComponentInParentAndChildren<BackGroundFactory>(StoryCanvas).enabled = !ModalFlag;

                foreach (var components in GetComponentsInParentAndChildren<ShowUIText>(StoryCanvas))
                {
                    components.enabled = !ModalFlag;
                }
                foreach (var components in GetComponentsInParentAndChildren<UIMaskTransparent>(StoryCanvas))
                {
                    components.enabled = !ModalFlag;
                }
                foreach (var components in GetComponentsInParentAndChildren<HumanController>(StoryCanvas))
                {
                    components.enabled = !ModalFlag;
                }
                foreach (var components in GetComponentsInParentAndChildren<BackGroundController>(StoryCanvas))
                {
                    components.enabled = !ModalFlag;
                }

                break;
            case "Option":
                BackgroundObject = GameObject.Find("BackgroundPrefab");
                GetComponentInParentAndChildren<BackGroundController>(BackgroundObject).enabled = !ModalFlag;

                //BackgroundObject = GameObject.Find("BackgroundCamera");
                //GetComponentInParentAndChildren<MoveStage>(BackgroundObject).enabled = !ModalFlag;

                RootObject = GameObject.Find("OptionCanvas");
                //GetComponent<OptionRotate>().enabled = !ModalOption.isPause;

                GetComponentInParentAndChildren<OptionRotate>(RootObject).enabled = !ModalFlag;
                GetComponentInParentAndChildren<CircularOption>(RootObject).enabled = !ModalFlag;
                GetComponentInParentAndChildren<SetLineScale>(RootObject).enabled = !ModalFlag;

                if (GetComponentInParentAndChildren<keyConfigCoverFlow>(RootObject) != null)
                    GetComponentInParentAndChildren<keyConfigCoverFlow>(RootObject).enabled = !ModalFlag;
                if (GetComponentInParentAndChildren<KeyConfigBehaviour>(RootObject) != null)
                    GetComponentInParentAndChildren<KeyConfigBehaviour>(RootObject).enabled = !ModalFlag;

                if (GetComponentInParentAndChildren<ChangeBGMVal>(RootObject) != null)
                    GetComponentInParentAndChildren<ChangeBGMVal>(RootObject).enabled = !ModalFlag;
                if (GetComponentInParentAndChildren<ChangeDifficulty>(RootObject) != null)
                    GetComponentInParentAndChildren<ChangeDifficulty>(RootObject).enabled = !ModalFlag;
                if (GetComponentInParentAndChildren<ChangeDIM>(RootObject) != null)
                    GetComponentInParentAndChildren<ChangeDIM>(RootObject).enabled = !ModalFlag;
                if (GetComponentInParentAndChildren<ChangeVoiceVolume>(RootObject) != null)
                    GetComponentInParentAndChildren<ChangeVoiceVolume>(RootObject).enabled = !ModalFlag;
                if (GetComponentInParentAndChildren<ChangeSEVolume>(RootObject) != null)
                    GetComponentInParentAndChildren<ChangeSEVolume>(RootObject).enabled = !ModalFlag;
                break;
            case "Main":
			case "Main1":
                //validate Null Exception
                Player = GameObject.Find("UNICORN1playMode");

                RootObject = GameObject.Find("Canvas");

                //GetComponentInParentAndChildren<LockOnChangeTest>(RootObject).enabled = !ModalFlag;
				

                Script = GameObject.Find("Scripts");

			if (GetComponentInParentAndChildren<Time_ScoreScript>(Script) != null)
				GetComponentInParentAndChildren<Time_ScoreScript>(Script).enabled = !ModalFlag;

			if (GetComponentInParentAndChildren<EnemyInstantiate>(Script) != null)
				GetComponentInParentAndChildren<EnemyInstantiate>(Script).enabled = !ModalFlag;
			
                int SelectNumber = PlayerSelectController.Selectnumber();

                switch (SelectNumber)
                {
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

                if (Player != null)
                {
				
					GetComponentInParentAndChildren <Animator>(Player).enabled = !ModalFlag;

                    GetComponentInParentAndChildren<PlayerAp>(Player).enabled = !ModalFlag;
                    GetComponentInParentAndChildren<PlayerMove>(Player).enabled = !ModalFlag;
                    GetComponentInParentAndChildren<PlayerRotate>(Player).enabled = !ModalFlag;
                    GetComponentInParentAndChildren<PlayerMotion>(Player).enabled = !ModalFlag;

					if (GetComponentInParentAndChildren<PlayerShoot_U>(Player) != null)					
                   		GetComponentInParentAndChildren<PlayerShoot_U>(Player).enabled = !ModalFlag;
					if (GetComponentInParentAndChildren<PlayerShoot_P>(Player) != null)					
						GetComponentInParentAndChildren<PlayerShoot_P>(Player).enabled = !ModalFlag;
					if (GetComponentInParentAndChildren<PlayerShoot_B>(Player) != null)					
						GetComponentInParentAndChildren<PlayerShoot_B>(Player).enabled = !ModalFlag;

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
            case "2PlayerModeStage1":
			case "2PlayerModeStage2":
			
                RootObject = GameObject.Find("Canvas");

				if (GetComponentInParentAndChildren<LockOnChangeTest>(RootObject) != null)
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
				if (GetComponentInParentAndChildren<MainCameraController>(CameraObject) != null)					
                	GetComponentInParentAndChildren<MainCameraController>(CameraObject).enabled = !ModalFlag;

                CameraObject2 = GameObject.Find("Camera2");
				if (GetComponentInParentAndChildren<MainCameraController2>(CameraObject2) != null)									
                	GetComponentInParentAndChildren<MainCameraController2>(CameraObject2).enabled = !ModalFlag;

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

    /// <summary>
    /// 親や子オブジェクトも含めた範囲から指定のコンポーネントを全て取得する
    /// </summary>
    public static List<T> GetComponentsInParentAndChildren<T>(GameObject gameObject)
    {
        List<T> _list = new List<T>(gameObject.GetComponents<T>());

        _list.AddRange(new List<T>(gameObject.GetComponentsInChildren<T>()));
        _list.AddRange(new List<T>(gameObject.GetComponentsInParent<T>()));

        return _list;
    }
}
