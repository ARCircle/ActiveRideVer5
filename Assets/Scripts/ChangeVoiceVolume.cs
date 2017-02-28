using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVoiceVolume : MonoBehaviour {

    //DIMval 0-1
    public float VoiceVal;
    public float getVoiceVal
    {
        private set { VoiceVal = value; }
        get { return VoiceVal; }
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
    void Start()
    {
        VoiceVal = 0.5f;
        gaugeCtrl = this.GetComponent<UnityEngine.UI.Image>();
        gaugeCtrl.fillAmount = VoiceVal;

        isBGM = true;

        OnColor = OnText.GetComponent<UnityEngine.UI.Text>().color;
        OffColor = OffText.GetComponent<UnityEngine.UI.Text>().color;

    }

    // Update is called once per frame
    void Update()
    {
        mixer.SetFloat("VoiceVolume", 80 * (VoiceVal - 1));

        if (Input.GetKeyUp(KeyCode.L))
        {
            if (isBGM)
            {
                tmpVolume = VoiceVal;

                Swap(ref OnColor, ref OffColor);
                OnText.GetComponent<UnityEngine.UI.Text>().color = OnColor;
                OffText.GetComponent<UnityEngine.UI.Text>().color = OffColor;

                isBGM = false;
            }
            else
            {
                VoiceVal = tmpVolume;

                Swap(ref OnColor, ref OffColor);
                OnText.GetComponent<UnityEngine.UI.Text>().color = OnColor;
                OffText.GetComponent<UnityEngine.UI.Text>().color = OffColor;

                isBGM = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            if (VoiceVal <= 1.0f)
            {
                VoiceVal += 0.05f;
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (VoiceVal >= 0f)
            {
                VoiceVal -= 0.05f;
            }
        }

        if (!isBGM)
        {
            gaugeCtrl.GetComponent<UnityEngine.UI.Image>().color = new Color(0.7f, 0.7f, 0.7f);
            VoiceVal = 0f;
        }
        else
        {
            gaugeCtrl.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f);
            gaugeCtrl.fillAmount = VoiceVal;
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
