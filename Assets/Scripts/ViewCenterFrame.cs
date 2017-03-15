using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ViewCenterFrame : MonoBehaviour {

    private UnityEngine.UI.Image gaugeCtrl;

    private float fill;
    private bool canViewOwn;

    // Use this for initialization
    void Start () {
        fill = 0.0f;
        this.GetComponent<ViewCenterFrame>().enabled = true;

        gaugeCtrl = this.GetComponent<UnityEngine.UI.Image>();
        gaugeCtrl.fillAmount = fill;
        Debug.Log(GetComponent<ViewCenterFrame>().enabled);

        canViewOwn = true;
    }

    void OnEnable()
    {
        fill = 0.0f;
        gaugeCtrl.fillAmount = fill;

        canViewOwn = true;
    }
	
	// Update is called once per frame
	void Update () {

       
        if (canViewOwn && fill <= 1.0f)
        {
            fill += 0.02f;

            gaugeCtrl.fillAmount = fill;
        }
        else
        {
            canViewOwn = false;
            this.GetComponent<ViewCenterFrame>().enabled = false;
        }
	}
}
