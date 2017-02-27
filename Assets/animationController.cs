using UnityEngine;
using System.Collections;

public class animationController : MonoBehaviour {

	Animator animator;

	void Start () {
	
		animator = GetComponent<Animator> ();
	}

	public void startAnimation(){
		if (animator) {
			animator.Play ("wait");
		}
	}

	void Update () {
	
		int idle = motionController.idleflag ();
		int moveforward = motionController.moveforwardflag ();
		int boost = motionController.boostflag ();

		if (idle == 1) {
			animator.SetBool ("idle", true);
		}

		if (moveforward == 1) {
			animator.SetBool ("anime", true);
		}

		if (boost == 1) {
			animator.SetBool ("Boost", true);
		}
			
	}
}
