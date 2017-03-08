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

    //MainとかのUI中心以外の画面に使うときはGameObjectのModalOptionをScene内にコピーする必要あり
    //for Main
    private GameObject Player;
    private GameObject Enemy;
    private GameObject EventSystemObject;

    //for select
    private GameObject PlayerSelectControllerObject;
    private GameObject CameraObject;

    public bool isModalOption;
    private bool isPause;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //CutInするか否かを取得
        isPause = CutinManager.canCutIn;

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
                GetComponentInParentAndChildren<CircularUI>(RootObject).enabled = !ModalFlag;

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

                BackgroundObject = GameObject.Find("BackgroundCamera");
                GetComponentInParentAndChildren<MoveStage>(BackgroundObject).enabled = !ModalFlag;

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
                break;
            case "stageselect":
                break;
            case "select":
                CameraObject = GameObject.Find("Camera");
                GetComponentInParentAndChildren<CameraController>(CameraObject).enabled = !ModalFlag;

                PlayerSelectControllerObject = GameObject.Find("PlayerSelectController");
                GetComponentInParentAndChildren<PlayerSelectController>(PlayerSelectControllerObject).enabled = !ModalFlag;

                break;
            case "selectsceneDouble":
                break;
            case "Result":
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
