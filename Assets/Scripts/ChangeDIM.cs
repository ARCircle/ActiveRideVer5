using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDIM : MonoBehaviour {

    //DIMval 0-1
    public float DIMVal;
    public float getDIMVal
    {
        private set { DIMVal = value; }
        get { return DIMVal; }
    }

    private UnityEngine.UI.Image gaugeCtrl;

    private bool isBGM;

    // Use this for initialization
    void Start()
    {
        DIMVal = 0.5f;
        gaugeCtrl = this.GetComponent<UnityEngine.UI.Image>();
        gaugeCtrl.fillAmount = DIMVal;

        isBGM = true;

    }

    // Update is called once per frame
    void Update()
    {
		Debug.Log ("AmbientSkyColor: " + RenderSettings.ambientLight);
		Color AmbientLight = RenderSettings.ambientLight;
		AmbientLight = new UnityEngine.Color (255f, 255f, 255f, DIMVal);
		RenderSettings.ambientLight = AmbientLight;

        if (Input.GetKeyUp(KeyCode.L))
        {
            if (isBGM)
            {
                //isBGM = false;
            }
            else
            {
                //isBGM = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            if (DIMVal <= 1.0f)
            {
                DIMVal += 0.1f;
            }

            RenderSettings.ambientSkyColor =
                new UnityEngine.Color((1 - DIMVal) * 255f, (1 - DIMVal) * 255f, (1 - DIMVal) * 255f, 1 - DIMVal);
            RenderSettings.ambientGroundColor =
                new UnityEngine.Color((1 - DIMVal) * 255f, (1 - DIMVal) * 255f, (1 - DIMVal) * 255f, 1 - DIMVal);

        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (DIMVal >= 0f)
            {
                DIMVal -= 0.1f;
            }
			RenderSettings.ambientSkyColor = 
                new UnityEngine.Color((1 - DIMVal) * 255f, (1 - DIMVal) * 255f, (1 - DIMVal) *255f, 1 - DIMVal);
            RenderSettings.ambientGroundColor = 
                new UnityEngine.Color((1 - DIMVal) * 255f, (1 - DIMVal) * 255f, (1 - DIMVal) * 255f, 1 - DIMVal);

        }

        RenderSettings.ambientIntensity = DIMVal;

        if (!isBGM)
        {
            //gaugeCtrl.GetComponent<UnityEngine.UI.Image>().color = new Color(0.7f, 0.7f, 0.7f);
            DIMVal = 0f;
        }
        else
        {
            //gaugeCtrl.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f);
            gaugeCtrl.fillAmount = DIMVal;
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
