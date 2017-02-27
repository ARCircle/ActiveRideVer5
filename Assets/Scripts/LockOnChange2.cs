using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LockOnChange2 : MonoBehaviour
{

    bool SubShotFlag2;
    GameObject Mark2;
    Vector2 posEnem2;
    Vector2 posR2;
    Vector2 posR_tmp2;
    public float lockMoveSpeed2;
    float EnemDis2;
    RectTransform rectTransform2 = null;
    [SerializeField]
    Transform target2 = null;
    GameObject player2;



    void Awake()

    {

        rectTransform2 = GetComponent<RectTransform>();


    }

    void Start()
    {
        player2 = GameObject.FindGameObjectWithTag("Player");
        player2 = player2.transform.parent.gameObject;
        //Debug.LogError(player.name);

    }

    void Update()

    {
        //ここからロックオンを動かすための処理
        posR2 = rectTransform2.anchoredPosition;

        if (Input.GetKey(KeyCode.I) && rectTransform2.anchoredPosition.y < 270)
        {
            posR_tmp2.y += lockMoveSpeed2;
        }
        if (Input.GetKey(KeyCode.K) && rectTransform2.anchoredPosition.y > -270)
        {
            posR_tmp2.y -= lockMoveSpeed2;
        }


        if (Input.GetKey(KeyCode.J) && rectTransform2.anchoredPosition.x > -480)
        {
            posR_tmp2.x -= lockMoveSpeed2;
        }
        if (Input.GetKey(KeyCode.L) && rectTransform2.anchoredPosition.x < 480)
        {
            posR_tmp2.x += lockMoveSpeed2;
        }

        rectTransform2.anchoredPosition = posR2 + posR_tmp2;
        posR_tmp2.x = 0;
        posR_tmp2.y = 0;
        Debug.Log(Camera.main.pixelHeight);

        //ここまで

        //ここから範囲内に敵を捕らえ続けるための処理
        posEnem2 = Camera.main.WorldToScreenPoint(target2.position);
        posEnem2 = new Vector2(posEnem2.x / Camera.main.pixelWidth, posEnem2.y / Camera.main.pixelHeight);
        //Debug.Log (posEnem);

        if (posEnem2.x > 0.75f)
        {
            playerRotate(player2, new Vector3(0, 1, 0));
        }
        if (posEnem2.x < 0.25f)
        {
            playerRotate(player2, new Vector3(0, -1, 0));
        }
        if (posEnem2.y > 0.75f)
        {
            playerRotate(player2, new Vector3(-1, 0, 0));
        }
        if (posEnem2.y < 0.25f)
        {
            playerRotate(player2, new Vector3(1, 0, 0));
        }

        //ここまで

        //ロックオン先のオブジェクトの取得
        Ray ray = Camera.main.ScreenPointToRay(posR2);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //Rayが当たるオブジェクトがあった場合はそのオブジェクト名をログに表示
            Debug.Log(hit.collider.gameObject.name);
        }

    }

    public static void playerRotate(GameObject target2, Vector3 x)
    {
        target2.transform.Rotate(x);
    }

}

