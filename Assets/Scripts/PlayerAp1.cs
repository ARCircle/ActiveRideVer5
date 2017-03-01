using UnityEngine;
using System.Collections;

using UnityEngine.UI;

using UnityStandardAssets.ImageEffects;

public class PlayerAp1 : MonoBehaviour {

	public static int armorPoint;
	public static int armorPointMax = 5000;
    float downPoint = 0;
    bool downFlag = false;
    float downTime = 0;
    bool DmgFlag = true;
    ParticleSystem smoke;

	public Text armorText;

	int displayArmorPoint;

	public Camera MainCamera;

	public Color myWhite;
	public Color myYellow;
	public Color myRed;

	public Image gaugeImage;

	// Use this for initialization
	void Start () {

		armorPoint = armorPointMax;
		displayArmorPoint = armorPoint;

		//ゲーム開始時にはノイズを無効にする
		MainCamera.GetComponent<NoiseAndScratches> ().enabled = false;

        GameObject smoke_tmp = gameObject.transform.FindChild("smoke").gameObject;
        smoke = smoke_tmp.GetComponent<ParticleSystem>();
    }

	// Update is called once per frame
	void Update () {

		//体力をUI Textに表示する
		//armorText.text = armorPoint.ToString();

		//現在の体力と表示用体力が異なっていれば、現在の体力になるまで加減算する
		if (displayArmorPoint != armorPoint) 
			displayArmorPoint = (int)Mathf.Lerp(displayArmorPoint, armorPoint, 0.1F);

		//現在の体力と最大体力をUI Textに表示する
		armorText.text = string.Format("{0:0000} / {1:0000}", displayArmorPoint, armorPointMax);

		//残り体力の割合により文字の色を変える
		float percentageArmorpoint = (float)displayArmorPoint / armorPointMax;

		if( percentageArmorpoint > 0.5F){
			armorText.color = myWhite;
			gaugeImage.color = new Color(0.25F, 0.7F, 0.6F);
		}else if( percentageArmorpoint > 0.3F){
			armorText.color = myYellow;
			gaugeImage.color = myYellow;
		}else{
			armorText.color = myRed;
			gaugeImage.color = myRed;

			//プレイヤーの体力が一定以下になったらノイズを有効にする
			MainCamera.GetComponent<NoiseAndScratches> ().enabled = true;
		}

		//ゲージの長さを体力の割合に合わせて伸縮させる
		gaugeImage.transform.localScale = new Vector3(percentageArmorpoint, 1, 1);

        if (downFlag)
        {
            Debug.LogError("1Pダウン状態移動");
            DmgFlag = false;
        //    GetComponent<CharacterController>().enabled = false;
            downTime += Time.deltaTime;
            if(downTime >= 3)
            {
                downFlag = false;
                DmgFlag = true;
                //  GetComponent<CharacterController>().enabled = true;
                downTime = 0;
                Debug.LogError("1Pダウン状態解除");
                downPoint = 0;
                smoke.Stop();
            }
        }


	}

	private void OnCollisionEnter(Collision collider) {
        //敵の弾と衝突したらダメージ

        if (DmgFlag)
        {
            if (collider.gameObject.tag == "Shot_B")
            {
                armorPoint -= ShotPlayer_B2.damage;
                armorPoint = Mathf.Clamp(armorPoint, 0, armorPointMax);
                downPoint += ShotPlayer_B2.damage / 100;
            }
            else if (collider.gameObject.tag == "Shot_U")
            {
                armorPoint -= ShotPlayer_U2.damage;
                armorPoint = Mathf.Clamp(armorPoint, 0, armorPointMax);
                downPoint += ShotPlayer_U2.damage / 100;
            }
            else if (collider.gameObject.tag == "Shot_P")
            {
                armorPoint -= ShotPlayer_P2.damage;
                armorPoint = Mathf.Clamp(armorPoint, 0, armorPointMax);
                downPoint += ShotPlayer_P2.damage / 100;
            }
        }

        if (downPoint > 5)
        {
            
            downFlag = true;
            smoke.Play();
        }

    }

	//敵とのあたり判定
	/*
	void OnControllerColliderHit(ControllerColliderHit hit){
		if (hit.gameObject.tag == "Enemy") {
			if (PlayerMove.flag == 0) {
				armorPoint -= damage;
				armorPoint = Mathf.Clamp (armorPoint, 0, armorPointMax);
				Destroy (hit.gameObject);
			}
			if(PlayerMove.flag == 1){
				Destroy (hit.gameObject);
			}
		}
	}
	*/
}

