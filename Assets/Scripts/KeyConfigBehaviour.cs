using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Core;
using YamlDotNet.Helpers;
using YamlDotNet.Samples;
using YamlDotNet.Serialization;

public class KeyConfigBehaviour : MonoBehaviour
{ 
    //KeyCondigの格納 
    //private KeyConfig keyConfig = new KeyConfig(ExternalFilePath.KEYCONFIG_PATH);

    private static keyConfigCoverFlow keyConfigCoverFlow_Instance = new keyConfigCoverFlow();

    //Keyの状態を取得する
    public static Dictionary<string, KeyCode> Config = new Dictionary<string, KeyCode>();

    //対応付け用の辞書
    private Dictionary<KeyCode, string> ConfigTransfar = new Dictionary<KeyCode, string>()
    {
        {KeyCode.A, "a"},
        {KeyCode.B, "b"},
        {KeyCode.C, "c"},
        {KeyCode.D, "d"},
        {KeyCode.E, "e"},
        {KeyCode.F, "f"},
        {KeyCode.G, "g"},
        {KeyCode.H, "h"},
        {KeyCode.I, "i"},
        {KeyCode.J, "j"},
        {KeyCode.K, "k"},
        {KeyCode.L, "l"},
        {KeyCode.M, "m"},
        {KeyCode.N, "n"},
        {KeyCode.O, "o"},
        {KeyCode.P, "p"},
        {KeyCode.Q, "q"},
        {KeyCode.R, "r"},
        {KeyCode.S, "s"},
        {KeyCode.T, "t"},
        {KeyCode.U, "u"},
        {KeyCode.V, "v"},
        {KeyCode.W, "w"},
        {KeyCode.X, "x"},
        {KeyCode.Y, "y"},
        {KeyCode.Z, "z"},
        {KeyCode.LeftShift, "left shift"},
        {KeyCode.RightShift, "right shift"},
        {KeyCode.LeftControl, "left ctrl"},
        {KeyCode.RightControl, "right ctrl"},
        {KeyCode.UpArrow, "up"},
        {KeyCode.DownArrow, "down"},
        {KeyCode.LeftArrow, "left"},
        {KeyCode.RightArrow, "right"},
        {KeyCode.Space, "space"},
    };

    //対応付け用の辞書
    //JoyStickのKeyCodeとInputManager用のstringを対応付け
    //TODO: 1P, 2Pを Input.GetJoystickNamesとかで取得, コメントアウト部の実現
    // -> 1P, 2Pで異なるconfigでのプレイを可能とする.

    private Dictionary<KeyCode, string> JoystickToConfig_1Player = new Dictionary<KeyCode, string>()
    {
        {KeyCode.JoystickButton0, "joystick button 0"},
        {KeyCode.JoystickButton1, "joystick button 1"},
        {KeyCode.JoystickButton2, "joystick button 2"},
        {KeyCode.JoystickButton3, "joystick button 3"},
        {KeyCode.JoystickButton4, "joystick button 4"},
        {KeyCode.JoystickButton5, "joystick button 5"},
        {KeyCode.JoystickButton6, "joystick button 6"},
    };
    private Dictionary<KeyCode, string> JoystickToConfig_2Player_JoyStick1 = new Dictionary<KeyCode, string>()
    {
        {KeyCode.JoystickButton0, "joystick 1 button 0"},
        {KeyCode.JoystickButton1, "joystick 1 button 1"},
        {KeyCode.JoystickButton2, "joystick 1 button 2"},
        {KeyCode.JoystickButton3, "joystick 1 button 3"},
        {KeyCode.JoystickButton4, "joystick 1 button 4"},
        {KeyCode.JoystickButton5, "joystick 1 button 5"},
        {KeyCode.JoystickButton6, "joystick 1 button 6"},
    };

    private Dictionary<KeyCode, string> JoystickToConfig_2Player_JoyStick2 = new Dictionary<KeyCode, string>()
    {
        {KeyCode.JoystickButton0, "joystick 2 button 0"},
        {KeyCode.JoystickButton1, "joystick 2 button 1"},
        {KeyCode.JoystickButton2, "joystick 2 button 2"},
        {KeyCode.JoystickButton3, "joystick 2 button 3"},
        {KeyCode.JoystickButton4, "joystick 2 button 4"},
        {KeyCode.JoystickButton5, "joystick 2 button 5"},
        {KeyCode.JoystickButton6, "joystick 2 button 6"},
    };

    //JoyStickによりKeyConfigを行う(通常)
    //falseのとき, KeyでAltを指定する
    private bool isJoyStickKeyConfig = true;
    public bool getisJoyStickKeyConfig
    {
        private set { isJoyStickKeyConfig = value; }
        get { return isJoyStickKeyConfig; }
    }

    private string configFilePath;

    private bool canInputConfigKey;
    public bool getCanInputConfigKey
    {
        private set { canInputConfigKey = value; }
        get { return canInputConfigKey; }
    }

    private int selectedCurrentConfig;

    private KeyCode inputKey;

    private GameObject SelectConfigObject;

    internal class KeyConfigSetting {

        private Array KeyCodeValues;
        private static KeyConfigBehaviour inputManager;
        private static KeyConfigSetting instance;

    }

    private DesirializationYaml desirializationYaml = new DesirializationYaml();
    DesirializationYaml.InputManagerFile inputManageFile;

    public void Awake()
    {
        
        //DefaultのKeyConfigを格納
        try {          
            //KeyConfig.Instance.keyConfig.LoadConfigFile();
        }catch(IOException e)
        {
            Debug.Log(e.Message);
        }

    }


    // Use this for initialization
	void Start () {

        canInputConfigKey = false;
        selectedCurrentConfig = 0;

        inputManageFile = desirializationYaml.DeserializeDefaultYaml();

    }

    // Update is called once per frame
    void Update () {

        //Config内容UIをw,s遷移 -> 
        //指定キー(Enterとかが押された)がGetKeyされたときのみcanInputConfigKey:true
        getCanInputConfigKey = canInputConfigKey;

        if (Input.GetKeyUp(KeyCode.Return))
        {
            if (canInputConfigKey == false)
            {
                SelectConfigObject = keyConfigCoverFlow_Instance.getSelectedConfigObject;
                SelectConfigObject.transform.FindChild("Conf").gameObject.GetComponent<UnityEngine.UI.Text>().color = Color.red;
                canInputConfigKey = true;
            }
            else
            {
                SelectConfigObject = keyConfigCoverFlow_Instance.getSelectedConfigObject;
                SelectConfigObject.transform.FindChild("Conf").gameObject.GetComponent<UnityEngine.UI.Text>().color = Color.white;
                canInputConfigKey = false;
            }
        }

        //Recovery Default Config
        if (Input.GetKeyUp(KeyCode.R))
        {
            desirializationYaml.RecoveryDefaultInputManager();

            SelectConfigObject = keyConfigCoverFlow_Instance.getSelectedConfigObject;
            inputManageFile = desirializationYaml.DeserializeDefaultYaml();

            //TODO: リカバリ時に回復されたKeyCodeをSelectedConfigObjectの位置に表示

        }

        //SaveKeyConfig
        if (Input.GetKeyUp(KeyCode.H))
        {
            if (canInputConfigKey == false)
            {
                desirializationYaml.SerializeYaml(inputManageFile);
            }
        }
                /*
                if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
                {
                    canInputConfigKey = false;
                }
                */

        if (canInputConfigKey)
        {
            string NameOfConfig;
            //DesirializationYaml.m_Axes Axes;

            NameOfConfig = SelectConfigObject.name;
            inputKey = GetKey(inputKey);

            Debug.Log("JoyStickCount" + Input.GetJoystickNames().Count());

            foreach (var name in Input.GetJoystickNames())
            {
                Debug.Log("JoyStickName" + name);
            }

            if (inputKey != KeyCode.None && inputKey != KeyCode.Return)
            {
                SelectConfigObject.transform.FindChild("Val").gameObject.GetComponent<Text>().text = inputKey.ToString();
                SelectConfigObject.transform.FindChild("Val").gameObject.GetComponent<Text>().color = Color.white;

                //inputManageFile配下の指定ファイルにinputKeyをtransfarして格納
                //inputManageFile.inputManager.Axes[0].SerializedVersion = Input;
                //TODO: NameOfConfig, Axesを投げてswitch文内の処理をifとかで統括するメソッド作成する

                switch (NameOfConfig)
                {
                    case "Horizontal2(p)":
                        inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Horizontal2")].AltPositiveButton
                            = TransfarKeyCode(inputKey);
                        break;
                    case "Horizontal2(n)":
                        inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Horizontal2")].AltNegativeButton
                            = TransfarKeyCode(inputKey);
                        break;
                    case "Horizontal(p)":
                        inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Horizontal")].AltPositiveButton
                            = TransfarKeyCode(inputKey);
                        break;
                    case "Horizontal(n)":
                        inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Horizontal")].AltNegativeButton
                            = TransfarKeyCode(inputKey);
                        break;
                    case "Vertical2(p)":
                        inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Vertical2")].AltPositiveButton
                            = TransfarKeyCode(inputKey);
                        break;
                    case "Vertical2(n)":
                        inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Vertical2")].AltNegativeButton
                            = TransfarKeyCode(inputKey);
                        break;
                    case "Vertical(p)":
                        inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Vertical")].AltPositiveButton
                            = TransfarKeyCode(inputKey);
                        break;
                    case "Vertical(n)":
                        inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Vertical")].AltNegativeButton
                            = TransfarKeyCode(inputKey);
                        break;
                    case "Horizontal4(p)":
                        inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Horizontal4")].AltPositiveButton
                            = TransfarJoystickToString(inputKey);
                        break;
                    case "Horizontal4(n)":
                        inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Horizontal4")].AltNegativeButton
                            = TransfarJoystickToString(inputKey);
                        break;
                    case "Horizontal3(p)":
                        inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Horizontal3")].AltPositiveButton
                            = TransfarJoystickToString(inputKey);
                        break;
                    case "Horizontal3(n)":
                        inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Horizontal3")].AltNegativeButton
                            = TransfarJoystickToString(inputKey);
                        break;
                    case "Vertical4(p)":
                        inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Vertical4")].PositiveButton
                            = SelectDictionaryByJoystickNum(2, inputKey);
                        break;
                    case "Vertical4(n)":
                        inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Vertical4")].NegativeButton
                          = SelectDictionaryByJoystickNum(2, inputKey);
                        break;
                    case "Vertical3(p)":
                        inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Vertical3")].PositiveButton
                          = SelectDictionaryByJoystickNum(1, inputKey);
                        break;
                    case "Vertical3(n)":
                        inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Vertical3")].NegativeButton
                          = SelectDictionaryByJoystickNum(1, inputKey);
                        break;
                    case "Jump":
                        if (isJoyStickKeyConfig)
                        {
                            inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Jump")].PositiveButton
                                = SelectDictionaryByJoystickNum(1, inputKey);
                        }
                        else
                        {
                            inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Jump")].AltPositiveButton
                                = TransfarKeyCode(inputKey);
                        }
                        break;
                    case "Jump2":
                        if (isJoyStickKeyConfig)
                        {
                            inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Jump2")].PositiveButton
                                = SelectDictionaryByJoystickNum(2, inputKey);
                        }
                        else
                        {
                            inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Jump2")].AltPositiveButton
                                = TransfarKeyCode(inputKey);
                        }
                        break;
                    case "Fire1":
                        if (isJoyStickKeyConfig)
                        {
                            inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Fire1")].PositiveButton
                                = SelectDictionaryByJoystickNum(1, inputKey);
                        }
                        else
                        {
                            inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Fire1")].AltPositiveButton
                                = TransfarKeyCode(inputKey);
                        }
                        break;
                    case "Fire2":
                        //positiveのみ変える
                        if (isJoyStickKeyConfig)
                        {
                            inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Fire2")].PositiveButton
                                = SelectDictionaryByJoystickNum(2, inputKey);
                        }
                        else
                        {
                            inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Fire2")].AltPositiveButton
                                = TransfarKeyCode(inputKey);
                        }
                        break;
                    case "Lock":
                        if (isJoyStickKeyConfig)
                        {
                            inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Lock")].PositiveButton
                                = SelectDictionaryByJoystickNum(1, inputKey);
                        }
                        else
                        {
                            inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Lock")].AltPositiveButton
                                = TransfarKeyCode(inputKey);
                        }
                        break;
                    case "Lock2":
                        if (isJoyStickKeyConfig)
                        {
                            inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Lock2")].PositiveButton
                                = SelectDictionaryByJoystickNum(2, inputKey);
                        }
                        else
                        {
                            inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Lock2")].AltPositiveButton
                                = TransfarKeyCode(inputKey);
                        }
                        break;
                    case "CamReset":
                        //1P, 2Pで共通
                        inputManageFile.inputManager.Axes[SearchAxesFromInputManager("CamReset")].PositiveButton
                            = TransfarJoystickToString(inputKey);
                        break;
                    case "Boost":
                        if (isJoyStickKeyConfig)
                        {
                            inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Boost")].PositiveButton
                                = SelectDictionaryByJoystickNum(1, inputKey);
                        }
                        else
                        {
                            inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Boost")].AltPositiveButton
                                = TransfarKeyCode(inputKey);
                        }
                        break;
                    case "Boost2":
                        if (isJoyStickKeyConfig)
                        {
                            inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Boost2")].PositiveButton
                                = SelectDictionaryByJoystickNum(2, inputKey);
                        }
                        else
                        {
                            inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Boost2")].AltPositiveButton
                                = TransfarKeyCode(inputKey);
                        }
                        break;
                    default:
                        break;
                }
            }

            /*
            switch (selectedCurrentConfig)
            {
                case 0:
                    //キーがあるときとないときのDictionaryへの格納処理
                    //Getkey.Tostringの結果とInputManager, positiveButtonの対応付け 
                    //  -> InputManagerの各要素とforeach比較して対応するとこに代入

                    if (Config.ContainsKey("atk"))
                    {
                        //inputManageFile配下の指定ファイルにinputKeyをtransfarして格納
                        //inputManageFile.inputManager.Axes[0].SerializedVersion = Input;
                    }
                    else
                    {
                        Config.Add("atk", inputKey);
                    }
                    break;
                case 1: break;
                default: break;
            } 
            */   
        }

	}

    private string TransfarKeyCode(KeyCode code){
        return ConfigTransfar[code].ToString();
    }

    private string TransfarJoystickToString(KeyCode code)
    {
        return JoystickToConfig_1Player[code].ToString();
    }

    private string TransfarJoystickToString2P(KeyCode code, int JoystickNum)
    {
        switch (JoystickNum)
        {
            case 1: return JoystickToConfig_2Player_JoyStick1[code].ToString();
            case 2: return JoystickToConfig_2Player_JoyStick1[code].ToString();
            default: return JoystickToConfig_1Player[code].ToString();
        }
    }

    private bool SetKey(string KeyName, KeyCode keyCode)
    {
        if(string.IsNullOrEmpty(KeyName))
        {
            return false;
        }else
        {
            Config[KeyName] = keyCode;
            return true;
        }
    }

    private bool RemoveKey(string KeyName)
    { 
        return Config.Remove(KeyName);
    }

    //Awake時にDefault Key Settingを格納
    private void SetDefaultKeyConfig(KeyConfigBehaviour DefaultKeyConfig)
    {
        /*
        foreach (var key in Key.AllKeyData)
        {
            SetK
        }
        */
    }

    //Input Key取得
    private KeyCode GetKey(KeyCode inputKey)
    {
       // KeyCode inputKey = KeyCode.Space; 
        if(Input.anyKeyDown){
            foreach(KeyCode code in Enum.GetValues(typeof(KeyCode)) ){
                if (Input.GetKeyDown(code)){
                    inputKey = code;
                    Debug.Log("Get:"+ inputKey);
                    return inputKey;           
                }
            }
        }else {
            //return inputKey;
        }
        return inputKey;
    }

    private string SelectDictionaryByJoystickNum(int Player ,KeyCode inputkey)
    {
        if (Input.GetJoystickNames().Count() == 2)
        {
            return TransfarJoystickToString2P(inputKey, 2);
        }
        else
        {
            return TransfarJoystickToString(inputKey);
        }
    }

    //TODO Add or Change, Remove,を関数化 
    
    //KeyConfig管理クラスの作成
    public void KeyConfig(string configFilePath)
    {
        this.configFilePath = configFilePath;
    }

    private int SearchAxesFromInputManager(string Query)
    {

        int ResultAxesIndex = 0;

        for (int i = 0; i < inputManageFile.inputManager.Axes.Count; i++)
        {
            if (inputManageFile.inputManager.Axes[i].m_Name == Query)
            {
                ResultAxesIndex = i;
            }
        }
        return ResultAxesIndex;
    }
}
