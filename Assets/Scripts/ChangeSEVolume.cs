using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSEVolume : MonoBehaviour {

    //SEval 0 to 1
    public float SEVal;
    public float getSEVal
    {
        private set { SEVal = value; }
        get { return SEVal; }
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

	public float masterVolume
	{
		set
        {
            mixer.SetFloat("SEVolume", 80 * (SEVal - 1));
        }
    }

    // Use this for initialization
    void Start()
    {
        SEVal = 0.5f;
        gaugeCtrl = this.GetComponent<UnityEngine.UI.Image>();
        gaugeCtrl.fillAmount = SEVal;

        isBGM = true;

        OnColor = OnText.GetComponent<UnityEngine.UI.Text>().color;
        OffColor = OffText.GetComponent<UnityEngine.UI.Text>().color;

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(-40 * Mathf.Log(Mathf.Abs(100 * (SEVal - 1) + 1),10));
		mixer.SetFloat("SEVolume", 80 * (SEVal - 1));

        if (Input.GetKeyUp(KeyCode.L))
        {
            if (isBGM)
            {
                tmpVolume = SEVal;

                Swap(ref OnColor, ref OffColor);
                OnText.GetComponent<UnityEngine.UI.Text>().color = OnColor;
                OffText.GetComponent<UnityEngine.UI.Text>().color = OffColor;

                isBGM = false;
            }
            else
            {
                SEVal = tmpVolume;

                Swap(ref OnColor, ref OffColor);
                OnText.GetComponent<UnityEngine.UI.Text>().color = OnColor;
                OffText.GetComponent<UnityEngine.UI.Text>().color = OffColor;

                isBGM = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetAxisRaw("Horizontal3") > 0 || Input.GetAxisRaw("Horizontal4") > 0)
        {
            if (SEVal <= 1.0f)
            {
                SEVal += 0.05f;
            }
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetAxisRaw("Horizontal3") < 0 || Input.GetAxisRaw("Horizontal4") < 0)
        {
            if (SEVal >= 0f)
            {
                SEVal -= 0.05f;
            }
        }

        if (!isBGM)
        {
            gaugeCtrl.GetComponent<UnityEngine.UI.Image>().color = new Color(0.7f, 0.7f, 0.7f);
            SEVal = 0f;
        }
        else
        {
            gaugeCtrl.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f);
            gaugeCtrl.fillAmount = SEVal;
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
