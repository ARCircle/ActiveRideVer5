using UnityEngine;
using System.Collections;

using UnityEngine.UI;

using UnityStandardAssets.ImageEffects;

public class BattleManager : MonoBehaviour {

	int battleStatus;

	const int BATTLE_START = 0;
	const int BATTLE_PLAY  = 1;
	const int BATTLE_END   = 2;
	
	float timer;

	public Image messageStart;
	public Image messageWin;
	public Image messageLose;
	
	public static int score;

	int clearScore;
	
	public Camera resultCamera;
	
	public GameObject resultCameraObject;
	
	// Use this for initialization
	void Start () {
	
		battleStatus = BATTLE_START;
	
		timer = 0;

		messageStart.enabled = true;
		messageWin.enabled = false;
		messageLose.enabled = false;
		
		score = 0;

		//敵の最大生成数をクリア数にする
		clearScore = EnemyInstantiate.instantiateValue;
		
		//ゲーム開始時はリザルト用カメラはオフにする
		resultCamera.enabled = false;
		
		//ゲーム開始時は効果をオフにする
		Camera.main.GetComponent<ColorCorrectionCurves> ().enabled = false;
		Camera.main.GetComponent<DepthOfField> ().enabled = false;
		
		resultCameraObject.GetComponent<ColorCorrectionCurves> ().enabled = false;
		resultCameraObject.GetComponent<DepthOfField> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		switch(battleStatus){
		
		case BATTLE_START:
			
			//時間経過でメッセージを消して状態移行
			timer += Time.deltaTime;

			if(timer > 3){

				messageStart.enabled = false;

				battleStatus = BATTLE_PLAY;

				timer = 0;
			}
			
			break;
			
		case BATTLE_PLAY:
			
			//スコアが出現数に到達したら勝利	
			if(score >= clearScore){
				battleStatus = BATTLE_END;
				messageWin.enabled = true;
				
				//勝利時にリザルト用カメラに切り替える
				resultCamera.enabled = true;
			}
			
			//プレイヤーの体力が0以下になったら敗北
			if(PlayerAp.armorPoint <= 0){
				battleStatus = BATTLE_END;
				messageLose.enabled = true;
			}
			break;
			
		case BATTLE_END:
			
			//一定時間経過したら遷移可能にする
			timer += Time.deltaTime;
			
			if(timer > 3){

				//動きを止める
				Time.timeScale = 0;	

				if (Input.GetButtonDown ("Fire1")){
					Application.LoadLevel("Result");
					
					//動きを再開する
					Time.timeScale = 1;
				}

				//遷移可能状態になったらカメラの効果を有効にする
				if(messageWin.enabled == true){
					resultCameraObject.GetComponent<ColorCorrectionCurves> ().enabled = true;
					resultCameraObject.GetComponent<DepthOfField> ().enabled = true;
				}else{
					Camera.main.GetComponent<ColorCorrectionCurves> ().enabled = true;
					Camera.main.GetComponent<DepthOfField> ().enabled = true;
				}
			}
			break;
			
		default:
			break;
		}
	}
}
