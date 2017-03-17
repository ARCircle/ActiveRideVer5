using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultScript : MonoBehaviour {

	private float time;
	public Text EnemyDestroyS;
	public Text ClearTimeS;
	public Text ClearHPS;
	public Text TotalS;
	public Text EnemyDestroyT;
	public Text ClearTimeT;
	public Text ClearHPT;
	public Text TotalT;
	public Text RankT;


	// Use this for initialization
	void Start () {
		//ModalOptionの加算ロード
		UnityEngine.SceneManagement.SceneManager.LoadScene("ModalOption", LoadSceneMode.Additive);

		time = 0;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > 1) {
			EnemyDestroyS.text = string.Format("{0:0000000}",Time_ScoreScript.destroyScore);
			EnemyDestroyS.color = new Color(255,255,255,255);
			EnemyDestroyT.color = new Color(255,255,255,255);
		}
		if (time > 2) {
			Time_ScoreScript.timeScore = (int)Time_ScoreScript.timeLimit_tmp * 50;
			ClearTimeS.text = string.Format("{0:0000000}", Time_ScoreScript.timeScore);
			ClearTimeS.color = new Color(255,255,255,255);
			ClearTimeT.color = new Color(255,255,255,255);

		}
		if (time > 3) {
			Time_ScoreScript.HPScore = PlayerAp.armorPoint * 5;
			ClearHPS.text = string.Format("{0:0000000}" ,Time_ScoreScript.HPScore);
			ClearHPS.color = new Color(255,255,255,255);
			ClearHPT.color = new Color(255,255,255,255);

		}

		if (time > 4) {
			Time_ScoreScript.allScore = Time_ScoreScript.destroyScore + Time_ScoreScript.HPScore + Time_ScoreScript.timeScore;
			TotalS.text = string.Format("{0:0000000}", Time_ScoreScript.allScore);
			TotalS.color = new Color(255,255,255,255);
			TotalT.color = new Color(255,255,255,255);
		}

		if (time > 5) {
			RankSelect (Time_ScoreScript.allScore, RankT);
			RankT.color = new Color(1,0,0,1);
		
		
		}

		if (time > 6 && Input.GetButtonDown ("Lock")) {
			Time_ScoreScript.timeLimit_tmp = Time_ScoreScript.timeLimit_1P_tmp;
			SceneManager.LoadScene ("Title");
		}
	}
	void RankSelect(int SCR,Text SCRT){
		if (SCR >= 45000){
			SCRT.text = "SSS";
		}else if (SCR >= 40000) {
			SCRT.text = "SS";
		}else if (SCR >= 35000) {
			SCRT.text = "S";
		} else if (SCR >= 30000) {
			SCRT.text = "A";
		} else if (SCR >= 20000) {
			SCRT.text = "B";
		}else
			SCRT.text = "C";
	}

}
