using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingController : MonoBehaviour {

    public GameObject TargetCircularObject;
    private int color;
    private Vector2 SizeDelta;
    private bool canTransformRing;

	// Use this for initialization
	void Start () {
        canTransformRing = true;

        color = 255;
        //this.transform.localScale = TargetCircularObject.GetComponent<RectTransform>().sizeDelta;
        SizeDelta = TargetCircularObject.GetComponent<RectTransform>().sizeDelta;
    }
	
	// Update is called once per frame
	void Update () {

        //TargetCircularObject.GetComponent<UnityEngine.UI.Button>().IsActive = true;

	    if(this.GetComponent<UnityEngine.UI.Image>().color.a >= 0 && canTransformRing)
        {
            SizeDelta.x *= 1.01f;
            SizeDelta.y *= 1.01f;
            color -= 5;
            this.GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f, color/255f);
            this.GetComponent<RectTransform>().sizeDelta = SizeDelta;
        }else
        {
            canTransformRing = false;
        }
    	
	}
}
