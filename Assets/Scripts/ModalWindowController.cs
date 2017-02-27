using UnityEngine;
using System.Collections;

using UnityEngine.UI;
//public 

public class ModalWindowController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void OnClick(){
        //1Pモード開始
        Application.LoadLevel("Main");

    }

}
