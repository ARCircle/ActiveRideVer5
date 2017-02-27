using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfLockOn1 : MonoBehaviour {
    public bool SubShotFlag1;
    GameObject Mark;
    Vector2 posEnem;
    Vector2 posR;
    Vector2 posR_tmp;
    public float lockMoveSpeed1;
    float EnemDis;
    RectTransform rectTransform = null;
    [SerializeField]
    Transform target = null;
    GameObject player;



    void Awake()

    {

        rectTransform = GetComponent<RectTransform>();


    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player2");
        player = player.transform.parent.gameObject;
        //Debug.LogError(player.name);

    }

    void Update()

    {
        //ここからロックオンを動かすための処理
        posR = rectTransform.anchoredPosition;

        if (Input.GetKey(KeyCode.I) && rectTransform.anchoredPosition.y < 270)
        {
            posR_tmp.y += lockMoveSpeed1;
        }
        if (Input.GetKey(KeyCode.K) && rectTransform.anchoredPosition.y > -270)
        {
            posR_tmp.y -= lockMoveSpeed1;
        }


        if (Input.GetKey(KeyCode.J) && rectTransform.anchoredPosition.x > -480)
        {
            posR_tmp.x -= lockMoveSpeed1;
        }
        if (Input.GetKey(KeyCode.L) && rectTransform.anchoredPosition.x < 480)
        {
            posR_tmp.x += lockMoveSpeed1;
        }

        rectTransform.anchoredPosition = posR + posR_tmp;
        posR_tmp.x = 0;
        posR_tmp.y = 0;
        Debug.Log(Camera.main.pixelHeight);

        //ここまで

        //ここから範囲内に敵を捕らえ続けるための処理
        posEnem = Camera.main.WorldToScreenPoint(target.position);
        posEnem = new Vector2(posEnem.x / Camera.main.pixelWidth, posEnem.y / Camera.main.pixelHeight);
        //Debug.Log (posEnem);

        if (posEnem.x > 0.75f)
        {
            playerRotate(player, new Vector3(0, 1, 0));
        }
        if (posEnem.x < 0.25f)
        {
            playerRotate(player, new Vector3(0, -1, 0));
        }
        if (posEnem.y > 0.75f)
        {
            playerRotate(player, new Vector3(-1, 0, 0));
        }
        if (posEnem.y < 0.25f)
        {
            playerRotate(player, new Vector3(1, 0, 0));
        }

        //ここまで

        //ロックオン先のオブジェクトの取得
        Ray ray = Camera.main.ScreenPointToRay(posR);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //Rayが当たるオブジェクトがあった場合はそのオブジェクト名をログに表示
            Debug.Log(hit.collider.gameObject.name);
        }

    }

    public static void playerRotate(GameObject target, Vector3 x)
    {
        target.transform.Rotate(x);
    }

}
