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
	
		int aniflag1 = camera1Controller.flag1 ();
		int aniflag2 = camera1Controller.flag2 ();
		int aniflag3 = camera1Controller.flag3 ();

		if (aniflag1 == 1) {
			animator.SetBool ("idle", true);
		}

		if (aniflag2 == 1) {
			animator.SetBool ("anime", true);
		}

		if (aniflag3 == 1) {
			animator.SetBool ("Boost", true);
		}
			
	}
}
