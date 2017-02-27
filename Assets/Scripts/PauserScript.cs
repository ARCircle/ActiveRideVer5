using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauserScript : MonoBehaviour {
 
    private static ModalOption modalOption = new ModalOption();
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
