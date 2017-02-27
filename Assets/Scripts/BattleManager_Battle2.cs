using UnityEngine;
using System.Collections;

using UnityEngine.UI;

using UnityStandardAssets.ImageEffects;

public class BattleManager_Battle2 : MonoBehaviour {

	int battleStatus;

	const int BATTLE_START = 0;
	const int BATTLE_PLAY  = 1;
	const int BATTLE_END   = 2;
	
	float timer;

	public Image messageStart2;
	public Image messageWin2;
	public Image messageLose2;

	public static int score;

	int clearScore;

	public Camera resultCamera2;

	public Camera maincamera2;

	public GameObject resultCameraObject2;

	
	// Use this for initialization
	void Start () {
	
		battleStatus = BATTLE_START;
	
		timer = 0;
	
		messageStart2.enabled = true;
		messageWin2.enabled = false;
		messageLose2.enabled = false;

		score = 0;

		//敵の最大生成数をクリア数にする
		clearScore = EnemyInstantiate.instantiateValue;
		
		//ゲーム開始時はリザルト用カメラはオフにする
		resultCamera2.enabled = false;
		
		//ゲーム開始時は効果をオフにする
		Camera.main.GetComponent<ColorCorrectionCurves> ().enabled = false;
		Camera.main.GetComponent<DepthOfField> ().enabled = false;
		maincamera2.GetComponent<ColorCorrectionCurves> ().enabled = false;
		maincamera2.GetComponent<DepthOfField> ().enabled = false;


		resultCameraObject2.GetComponent<ColorCorrectionCurves> ().enabled = false;
		resultCameraObject2.GetComponent<DepthOfField> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		switch(battleStatus){
		
		case BATTLE_START:
			
			//時間経過でメッセージを消して状態移行
			timer += Time.deltaTime;

			if(timer > 3){


				messageStart2.enabled = false;

				battleStatus = BATTLE_PLAY;

				timer = 0;
			}
			
			break;
			
		case BATTLE_PLAY:
			
			
			//プレイヤー1体力が0以下になったら敗北＆プレイヤー２勝利
			if(PlayerAp1.armorPoint <= 0){
				battleStatus = BATTLE_END;
				messageWin2.enabled = true;
				resultCamera2.enabled = true;
			}
			//プレイヤー2の体力が0以下になったら敗北
			if(PlayerAp2.armorPoint <= 0){
				battleStatus = BATTLE_END;
				messageLose2.enabled = true;

			}

			break;
			
		case BATTLE_END:
			
			//一定時間経過したら遷移可能にする
			timer += Time.deltaTime;
			
			if(timer > 3){

				//動きを止める
				Time.timeScale = 0;	

				if (Input.anyKey){
					Application.LoadLevel("Title");
					
					//動きを再開する
					Time.timeScale = 1;
				}

				//遷移可能状態になったらカメラの効果を有効にする
				if(messageLose2.enabled == true){
					

					maincamera2.GetComponent<ColorCorrectionCurves> ().enabled = true;
					maincamera2.GetComponent<DepthOfField> ().enabled = true;
				}
				if(messageWin2.enabled == true){
					resultCameraObject2.GetComponent<ColorCorrectionCurves> ().enabled = true;
					resultCameraObject2.GetComponent<DepthOfField> ().enabled = true;
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
