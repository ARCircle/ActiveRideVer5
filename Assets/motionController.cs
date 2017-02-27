using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class motionController : MonoBehaviour {

	static int idle = 0;
	static int moveforward = 0;
	static int boost = 0;
	static int move = 0;

	float time;

	void Start () {
	
		time = 0.0f;
		idleflag ();
		moveforwardflag ();
		boostflag ();
		movflag ();

	}
	

	void Update () {

		time += Time.deltaTime;

		if (time >= 21.5f) {
			idle = 1;
			idleflag ();
		}

		if (time >= 27.0f) {
			moveforward = 1;
			moveforwardflag ();
		}

		if (time >= 31.5f) {
			boost = 1;
			boostflag ();
		}

		if(time >= 32.5f){
			move = 1;
			movflag ();
		}


	}


	public static int idleflag(){
		return idle;
	}

	public static int moveforwardflag(){
		return moveforward;
	}

	public static int boostflag(){
		return boost;
	}

	public static int movflag(){
		return move;
	}
		

		
}
