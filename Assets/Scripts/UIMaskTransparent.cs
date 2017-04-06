using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMaskTransparent : MonoBehaviour {
    [SerializeField]
    public Material UI_mask_mat;

    //[Range(0, 1)]
    private float mask_range;
	private float mask_rangeMAX = 0.5f;
    private float maskRangeDelta = 0.02f;

    private bool canUpdateMaskRange;

    // Use this for initialization
    void Start () {
        //Debug.Log("UI_Mask" + UI_mask_mat);

		mask_range = mask_rangeMAX;
        this.UI_mask_mat.SetFloat("_Range", 1.0f);

        canUpdateMaskRange = true;
    }

    void OnEnable()
    {
        //ModalOptionが解除された時は無効
        if (!Input.GetKeyUp(KeyCode.DownArrow))
        {
            canUpdateMaskRange = true;
			mask_range = mask_rangeMAX;
        }

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
