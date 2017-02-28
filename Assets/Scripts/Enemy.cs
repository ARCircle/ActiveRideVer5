using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	GameObject target;
	
	public GameObject shot;

	float shotInterval = 0;
	float shotIntervalMax = 1.0F;
	
	public GameObject exprosion;
	
	public int armorPoint;
	public int armorPointMax = 1000; 
	int damage = 100;
	
	float timer = 0;
	
	int enemyLevel = 0;
	
	// Use this for initialization
	void Start () {
	
		//ターゲットを取得
		target = GameObject.Find("PlayerTarget");
		
		armorPoint = armorPointMax;
	}
	
	// Update is called once per frame
	void Update () {
	
		/*
		//敵の攻撃範囲を設定する
		if( Vector3.Distance (target.transform.position, transform.position) <= 30 ){
			
			//ターゲットの方向を向く
			//transform.LookAt(target.transform);
			
			//スムーズにターゲットの方向を向く
			Quaternion targetRotation = Quaternion.LookRotation (target.transform.position - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * 10);
			
			//弾を発射する
			shotInterval += Time.deltaTime;
			
			if(shotInterval > shotIntervalMax){

				Instantiate(shot, transform.position, transform.rotation);
				shotInterval = 0;
			}
		}
		*/
		
		timer += Time.deltaTime;

		//経過時間に応じてレベルを上げる
		if (timer < 5)
			enemyLevel = 0;
		else if (timer < 10)
			enemyLevel = 2;
		else if (timer < 15)
			enemyLevel = 3;
		else if (timer < 20) {
			enemyLevel = 4;
			shotIntervalMax = 0.5F;
		}
		else if(timer < 25){
			enemyLevel = 5;
			//レベル４：攻撃間隔が短くなる
			shotIntervalMax = 0.5F;
		}
		
		//レベル２：プレイヤーが一定範囲に近づいたら攻撃
		if(enemyLevel >= 2) {
			
			if (Vector3.Distance (target.transform.position, transform.position) <= 50) {
				
				//ターゲットの方を徐々に向く
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (target.transform.position - transform.position), Time.deltaTime * 5);

				//一定間隔でショット
				shotInterval += Time.deltaTime;
				
				if (shotInterval > shotIntervalMax) {

					Instantiate (shot, transform.position, transform.rotation);
					shotInterval = 0;
				}
			}else{
					
				//レベル３：プレイヤーに自分から近づく
				if(enemyLevel >= 3){
					transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (target.transform.position - transform.position), Time.deltaTime * 5);
					transform.position += transform.forward * Time.deltaTime * 20;
				}
			}
		}
	}
	
	private void OnCollisionEnter(Collision collider) {

		
		if (collider.gameObject.tag == "Shot") {

			//プレイヤーの弾と衝突したら消滅する
			//Destroy (gameObject);
			//Instantiate(exprosion, transform.position, transform.rotation);
			
			//ダメージをランダムで変える
			//damage = Random.Range(50, 150);
			
			//プレイヤーの弾からダメージを取得する
			damage = collider.gameObject.GetComponent<ShotPlayer>().damage;
			
			//プレイヤーの弾と衝突したらダメージ
			armorPoint -= damage;

			//体力が0以下になったら消滅する
			if (armorPoint <= 0){
				Time_ScoreScript.destroyScore += 200;
				BattleManager.score++;
				Destroy (gameObject);
				Instantiate(exprosion, transform.position, transform.rotation);
				
				//リザルト用のスコアを加算する

			}
		}
		

	}
}
