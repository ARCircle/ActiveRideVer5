using UnityEngine;
using System.Collections;

public class PlayerMotion1 : MonoBehaviour {

	private Animator animator;

	// Use this for initialization
	void Start () {

		animator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {

		//モーションを切り替える
		if(Input.GetAxis ("Horizontal") > 0){

			animator.SetInteger("Horizontal",1);

		}else if(Input.GetAxis ("Horizontal") < 0){

			animator.SetInteger("Horizontal",-1);

		}else{

			animator.SetInteger("Horizontal",0);
		}

		if(Input.GetAxis ("Vertical") > 0){

			animator.SetInteger("Vertical",1);

		}else if(Input.GetAxis ("Vertical") < 0){

			animator.SetInteger("Vertical",-1);

		}else{

			animator.SetInteger("Vertical",0);
		}

		//ジャンプモーションに切り替える
		animator.SetBool("Jump", Input.GetButton ("Jump"));

		//ブーストキーが押されたらにパラメータを切り替える
		animator.SetBool("Boost",Input.GetButton ("Boost"));

		//攻撃動作のアニメーション管理
		if (Input.GetKey (KeyCode.G)) {
			animator.SetInteger ("Vertical", 1);
		} else if (Input.GetKeyUp (KeyCode.G)) {
			animator.SetInteger ("Vertical", 0);
		}


	}

}
