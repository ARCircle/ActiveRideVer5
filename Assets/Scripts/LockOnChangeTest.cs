using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LockOnChangeTest : MonoBehaviour {

	bool SubShotFlag;
	GameObject Mark;
	Vector2 posEnem;
	Vector2 posR;
	Vector2 posR_tmp;
    Vector2 posR_trans;
	public float lockMoveSpeed;
	float EnemDis;
	RectTransform rectTransform = null;
	[SerializeField] Transform target = null;
	GameObject player;
    Camera plcamera;
    Transform cameraParent;



    void Awake()

	{

		rectTransform = GetComponent<RectTransform> ();

	}

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player = player.transform.parent.gameObject;
        //Debug.LogError(player.name);
        cameraParent = Camera.main.transform.parent;
        GameObject plcamera_tmp = GameObject.Find("CameraForRay");
        plcamera = plcamera_tmp.GetComponent<Camera>();
    }

	void Update()

	{
		//ここからロックオンを動かすための処理
		posR = rectTransform.anchoredPosition;

        if (rectTransform.anchoredPosition.y > -270 && rectTransform.anchoredPosition.y < 270)
        {
            posR_tmp.y += Input.GetAxis("Horizontal3");
        }

        if (rectTransform.anchoredPosition.x > -270 && rectTransform.anchoredPosition.x < 270)
        {
            posR_tmp.x += Input.GetAxis("Vertical3");
        }




        /*	if (Input.GetKey (KeyCode.I)&&rectTransform.anchoredPosition.y < 270) {
                posR_tmp.y += lockMoveSpeed;
                }
            if (Input.GetKey (KeyCode.K)&&rectTransform.anchoredPosition.y > -270) {
                posR_tmp.y -= lockMoveSpeed;
                }


            if (Input.GetKey (KeyCode.J)&&rectTransform.anchoredPosition.x > -480) {
                posR_tmp.x -= lockMoveSpeed;
                }
            if (Input.GetKey (KeyCode.L)&&rectTransform.anchoredPosition.x < 480) {
                posR_tmp.x += lockMoveSpeed;
                }
        */
        rectTransform.anchoredPosition = posR + posR_tmp;
		posR_tmp.x = 0;
		posR_tmp.y = 0;
		

		//ここまで

		//ここから範囲内に敵を捕らえ続けるための処理
		posEnem = Camera.main.WorldToScreenPoint (target.position);
		posEnem = new Vector2 (posEnem.x / Camera.main.pixelWidth, posEnem.y / Camera.main.pixelHeight);
		//Debug.Log (posEnem);

		if (posEnem.x > 0.75f) {
            playerRotate(player,new Vector3(0, 1, 0));
        }
		if (posEnem.x < 0.25f) {
            playerRotate(player, new Vector3(0, -1, 0));
        }
		if (posEnem.y > 0.75f) {
            playerRotate(cameraParent, new Vector3(-1, 0, 0));
        }
		if (posEnem.y < 0.25f) {
            playerRotate(cameraParent, new Vector3(1,0, 0));
        }

        //ここまで

        //ロックオン先のオブジェクトの取得
        //posRを正規化してposR_transに代入
        posR_trans = new Vector2(posR.x/1920 + 0.5f, posR.y/1080 + 0.5f);

        //posR_transをカメラ用に変形
        posR_trans.x *= Camera.main.pixelWidth;
        posR_trans.y *= Camera.main.pixelHeight;
        //Debug.Log(posR_trans);
        Ray ray = plcamera.ScreenPointToRay(posR_trans);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
        RaycastHit hit;
        
        if (Physics.SphereCast(ray,2, out hit))
        {
            //Rayが当たるオブジェクトがあった場合はそのオブジェクト名をログに表示
            Debug.Log(hit.collider.gameObject.name);
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, 0, false);
        }
        

    }

    public static void playerRotate(GameObject target, Vector3 x)
    {
        target.transform.Rotate(x);
    }

    public static void playerRotate(Transform target, Vector3 x)
    {
        target.Rotate(x);
    }
}
