using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBGMVal : MonoBehaviour {

    //BGMVal 0 to 1
    public float BGMVal;
    public float getBGMVal
    {
        private set { BGMVal = value; }
        get { return BGMVal; }
    }

    private UnityEngine.UI.Image gaugeCtrl;

    public GameObject OnText;
    public GameObject OffText;

    private Color OnColor;
    private Color OffColor;

    private float tmpVolume;

    private bool isBGM;

    [SerializeField]
    UnityEngine.Audio.AudioMixer mixer;

    // Use this for initialization
    void Start () {
        BGMVal = 0.5f;
        gaugeCtrl = this.GetComponent<UnityEngine.UI.Image>();
        gaugeCtrl.fillAmount = BGMVal;

        isBGM = true;

        OnColor = OnText.GetComponent<UnityEngine.UI.Text>().color;
        OffColor = OffText.GetComponent<UnityEngine.UI.Text>().color;

    }

    // Update is called once per frame
    void Update () {
        mixer.SetFloat("BGMVolume", 80 * (BGMVal - 1));

        if (Input.GetKeyUp(KeyCode.L))
        {
            if (isBGM)
            {
                tmpVolume = BGMVal;

                Swap(ref OnColor, ref OffColor);
                OnText.GetComponent<UnityEngine.UI.Text>().color = OnColor;
                OffText.GetComponent<UnityEngine.UI.Text>().color = OffColor;

                isBGM = false;
            }else
            {
                BGMVal = tmpVolume;

                Swap(ref OnColor, ref OffColor);
                OnText.GetComponent<UnityEngine.UI.Text>().color = OnColor;
                OffText.GetComponent<UnityEngine.UI.Text>().color = OffColor;

                isBGM = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            if (BGMVal <= 1.0f)
            {
                BGMVal += 0.1f;
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (BGMVal >= 0f)
            {
                BGMVal -= 0.1f;
            }
        }

        if (!isBGM)
        {
            gaugeCtrl.color = new Color(0.7f,0.7f,0.7f);
            BGMVal = 0f;
        }else
        {
            gaugeCtrl.color = new Color(1f, 1f, 1f);
            gaugeCtrl.fillAmount = BGMVal;
        }
    }

    static void Swap<T>(ref T lhs, ref T rhs)
    {
        T temp;
        temp = lhs;
        lhs = rhs;
        rhs = temp;
    }
}
