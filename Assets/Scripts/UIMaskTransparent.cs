using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMaskTransparent : MonoBehaviour {
    [SerializeField]
    public Material UI_mask_mat;

    //[Range(0, 1)]
    private float mask_range;
    private float maskRangeDelta = 0.02f;

    private bool canUpdateMaskRange;

    // Use this for initialization
    void Start () {
        Debug.Log(UI_mask_mat);

        mask_range = 1.0f;
        this.UI_mask_mat.SetFloat("_Range", 1.0f);

        canUpdateMaskRange = true;
    }

    void OnEnable()
    {
        canUpdateMaskRange = true;
        mask_range = 1.0f;

    }

    // Update is called once per frame
    void Update () {

        if(mask_range >= 0.0f && canUpdateMaskRange)
        {
            mask_range -= maskRangeDelta;
        }else
        {
            canUpdateMaskRange = false;
        }

        this.UI_mask_mat.SetFloat("_Range", mask_range);
	}
}
