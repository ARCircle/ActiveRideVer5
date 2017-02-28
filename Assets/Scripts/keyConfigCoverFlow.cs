using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class keyConfigCoverFlow : MonoBehaviour {

    private static KeyConfigBehaviour keyConfig_Instance = new KeyConfigBehaviour();

    public List<GameObject> eachConfigsInKeyConfig = new List<GameObject>();

    private GameObject tmpConfig;

    private GameObject centerConfig;
    public GameObject getCenterConfig
    {
        private set { centerConfig = value; }
        get { return centerConfig; }
    }

    public int NumberOfObject;
    public int getNumberOfOject
    {
        set { NumberOfObject = value; }
        get { return NumberOfObject; }
    }

    private int Z_MAX = 30;
    private int Y_MAX = 120;
    private int X_OFFSET = -10;
    private int Y_OFFSET = -70;
    private int CenterConfigIndex;

    private int NumberOfActiveConfig = 3;

    private float X, Y, Z;
    private float Rotate_X, Rotate_Y, Rotate_Z;

    //Yamlファイルのインスタンス作成, inputManagerFileを作成
    private DesirializationYaml desirializationYaml = new DesirializationYaml();
    DesirializationYaml.InputManagerFile inputManageFile;

    // Use this for initialization
    void Start()
    {
        NumberOfObject = eachConfigsInKeyConfig.Count;
        getNumberOfOject = NumberOfObject;

        viewConvexConfig();
        setConfigInActive();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A) && !keyConfig_Instance.getCanInputConfigKey)
        {
            tmpConfig = eachConfigsInKeyConfig[0];
            for (int nLoop = 0; nLoop < NumberOfObject - 1; nLoop++)
            {
                eachConfigsInKeyConfig[nLoop] = eachConfigsInKeyConfig[nLoop + 1];
            }
            eachConfigsInKeyConfig[NumberOfObject - 1] = tmpConfig;
            viewConvexConfig();
            setConfigInActive();

        }
        KeyConfigBehaviour KeyConfigBehaviour_Instance = new KeyConfigBehaviour();

        if (Input.GetKeyDown(KeyCode.D) && !keyConfig_Instance.getCanInputConfigKey)
        {
            tmpConfig = eachConfigsInKeyConfig[NumberOfObject - 1];
            for (int nLoop = NumberOfObject - 1; nLoop > 0; nLoop--)
            {
                eachConfigsInKeyConfig[nLoop] = eachConfigsInKeyConfig[nLoop - 1];
            }
            eachConfigsInKeyConfig[0] = tmpConfig;

            viewConvexConfig();
            setConfigInActive();

        }

    }

    static void Swap<T>(ref T lhs, ref T rhs)
    {
        T temp;
        temp = lhs;
        lhs = rhs;
        rhs = temp;
    }
    private void viewConvexConfig()
    {
        for (int nLoop = 0; nLoop < NumberOfObject; nLoop++)
        {
            X = X_OFFSET;
            Y = Y_OFFSET + 1.2f * Y_MAX * Mathf.Sin((nLoop - 1) * (360 / NumberOfObject) * (Mathf.PI / 180));
            Z = Mathf.Abs(Z_MAX * Mathf.Cos((nLoop - 1) * (360 / NumberOfObject) * (Mathf.PI / 180)));

            Rotate_X = 60 * Mathf.Sin((nLoop - 1) * (360 / NumberOfObject) * (Mathf.PI / 180));

            if (nLoop == 1)
            {
                getCenterConfig = eachConfigsInKeyConfig[nLoop];
            }

            eachConfigsInKeyConfig[nLoop].transform.localPosition = new Vector3(X, Y, -Z);
            eachConfigsInKeyConfig[nLoop].transform.localEulerAngles = new Vector3(Rotate_X, 0, 0);

        }
    }
    public static GameObject SelectConfig;

    public GameObject getSelectedConfigObject
    {
        private set{
            SelectConfig = value;
        }
        get
        {
            return SelectConfig;
        }
    }

    private void setConfigInActive()
    {

        //inputManagerFile作成
        inputManageFile = desirializationYaml.DeserializeDefaultYaml();

        for (int nLoop = 0; nLoop < NumberOfObject; nLoop++)
        {

            eachConfigsInKeyConfig[nLoop].transform.FindChild("Val").gameObject.GetComponent<UnityEngine.UI.Text>().text
                = getValueOfConfig(eachConfigsInKeyConfig[nLoop], nLoop);

            eachConfigsInKeyConfig[nLoop].transform.FindChild("Conf").gameObject.GetComponent<UnityEngine.UI.Text>().fontSize = 28;
            eachConfigsInKeyConfig[nLoop].transform.FindChild("Val").gameObject.GetComponent<UnityEngine.UI.Text>().fontSize = 28;

            //出現するConfigの数を制限
            if (nLoop < NumberOfActiveConfig)
            {
                eachConfigsInKeyConfig[nLoop].SetActive(true);
            }
            else
            {
                eachConfigsInKeyConfig[nLoop].SetActive(false);
            }

            if(nLoop == Mathf.Floor(NumberOfActiveConfig / 2)) {
                SelectConfig = eachConfigsInKeyConfig[nLoop];
                eachConfigsInKeyConfig[nLoop].transform.FindChild("Conf").gameObject.GetComponent<UnityEngine.UI.Text>().color = Color.white;
                eachConfigsInKeyConfig[nLoop].transform.FindChild("Val").gameObject.GetComponent<UnityEngine.UI.Text>().color = Color.white;

            }
            else
            {
                eachConfigsInKeyConfig[nLoop].transform.FindChild("Conf").gameObject.GetComponent<UnityEngine.UI.Text>().color = Color.gray;
                eachConfigsInKeyConfig[nLoop].transform.FindChild("Val").gameObject.GetComponent<UnityEngine.UI.Text>().color = Color.gray;

            }
        }

    }

    private int SearchAxesFromInputManager(string Query){

        int ResultAxesIndex = 0;

        for (int i = 0; i < inputManageFile.inputManager.Axes.Count; i++)
        {
            if(inputManageFile.inputManager.Axes[i].m_Name == Query)
            {
                ResultAxesIndex = i;
            }
        }
        return ResultAxesIndex;
    }

    private string getValueOfConfig(GameObject Config, int nLoop)
    {
        string ValueOfConfig = "not set";
        string NameOfConfig;
        DesirializationYaml.m_Axes Axes;

        NameOfConfig = Config.name;

        //functionalize
        //Horizontal, Verticalの処理に基づく
        /*
        if (NameOfConfig.IsAny("Horizontal2(n)", "Horizontal(n)", "Vertical2(n)", "Vertical(n)"))
        {
            Axes = inputManageFile.inputManager.Axes[SearchAxesFromInputManager(NameOfConfig)];
            ValueOfConfig = Axes.NegativeButton;
        }
        else if (NameOfConfig.IsAny("Horizontal2(p)", "Horizontal(p)", "Vertical2(p)", "Vertical(p)",
            "Jump", "Jump2", "Fire1", "Fire2"))
        {
            Axes = inputManageFile.inputManager.Axes[SearchAxesFromInputManager(NameOfConfig)];
            ValueOfConfig = Axes.PositiveButton;
        }
        else if (NameOfConfig.IsAny("Boost", "Boost2", "Lock", "Lock2"))
        {
            Axes = inputManageFile.inputManager.Axes[SearchAxesFromInputManager(NameOfConfig)];
            ValueOfConfig = Axes.AltPositiveButton;
        }
        */
        
        switch (NameOfConfig)
        {
            case "Horizontal2(p)":
                Axes = inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Horizontal2")];
                ValueOfConfig = Axes.PositiveButton;
                break;
            case "Horizontal2(n)":
                Axes = inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Horizontal2")];
                ValueOfConfig = Axes.NegativeButton;
                break;
            case "Horizontal(p)":
                Axes = inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Horizontal")];
                ValueOfConfig = Axes.PositiveButton;
                break;
            case "Horizontal(n)":
                Axes = inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Horizontal")];
                ValueOfConfig = Axes.NegativeButton;
                break;
            case "Vertical2(p)":
                Axes = inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Vertical2")];
                ValueOfConfig = Axes.PositiveButton;
                break;
            case "Vertical2(n)":
                Axes = inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Vertical2")];
                ValueOfConfig = Axes.NegativeButton;
                break;
            case "Vertical(p)":
                Axes = inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Vertical")];
                ValueOfConfig = Axes.PositiveButton;
                break;
            case "Vertical(n)":
                Axes = inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Vertical")];
                ValueOfConfig = Axes.NegativeButton;
                break;
            case "Jump":
                Axes = inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Jump")];
                ValueOfConfig = Axes.PositiveButton;
                break;
            case "Jump2":
                Axes = inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Jump2")];
                ValueOfConfig = Axes.PositiveButton;
                break;
            case "Fire1":
                Axes = inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Fire1")];
                ValueOfConfig = Axes.PositiveButton;
                break;
            case "Fire2":
                Axes = inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Fire2")];
                ValueOfConfig = Axes.PositiveButton;
                break;
            case "Lock":
                Axes = inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Lock")];
                ValueOfConfig = Axes.AltPositiveButton;
                break;
            case "Lock2":
                Axes = inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Lock2")];
                ValueOfConfig = Axes.AltPositiveButton;
                break;
            case "Boost":
                Axes = inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Boost")];
                ValueOfConfig = Axes.AltPositiveButton;
                break;
            case "Boost2":
                Axes = inputManageFile.inputManager.Axes[SearchAxesFromInputManager("Boost2")];
                ValueOfConfig = Axes.AltPositiveButton;
                break;
            default:
                break;
        }
        

        return ValueOfConfig;
    }

}

public static partial class StringExtensions
{
    public static bool IsAny(this string self, params string[] values)
    {
        return values.Any(c => c == self);
    }
}
