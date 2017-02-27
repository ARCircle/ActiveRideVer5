using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LockOnChange1 : MonoBehaviour
{

    bool SubShotFlag1;
    GameObject Mark1;
    Vector2 posEnem1;
    Vector2 posR1;
    Vector2 posR_tmp1;
    public float lockMoveSpeed1;
    float EnemDis1;
    RectTransform rectTransform1 = null;
    [SerializeField]
    Transform target1 = null;
    GameObject player1;



    void Awake()

    {

        rectTransform1 = GetComponent<RectTransform>();


    }

    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player");
        player1 = player1.transform.parent.gameObject;
        //Debug.LogError(player.name);

    }

    void Update()

    {
        //ここからロックオンを動かすための処理
        posR1 = rectTransform1.anchoredPosition;

        if (Input.GetKey(KeyCode.I) && rectTransform1.anchoredPosition.y < 270)
        {
            posR_tmp1.y += lockMoveSpeed1;
        }
        if (Input.GetKey(KeyCode.K) && rectTransform1.anchoredPosition.y > -270)
        {
            posR_tmp1.y -= lockMoveSpeed1;
        }


        if (Input.GetKey(KeyCode.J) && rectTransform1.anchoredPosition.x > -480)
        {
            posR_tmp1.x -= lockMoveSpeed1;
        }
        if (Input.GetKey(KeyCode.L) && rectTransform1.anchoredPosition.x < 480)
        {
            posR_tmp1.x += lockMoveSpeed1;
        }

        rectTransform1.anchoredPosition = posR1 + posR_tmp1;
        posR_tmp1.x = 0;
        posR_tmp1.y = 0;
        Debug.Log(Camera.main.pixelHeight);

        //ここまで

        //ここから範囲内に敵を捕らえ続けるための処理
        posEnem1 = Camera.main.WorldToScreenPoint(target1.position);
        posEnem1 = new Vector2(posEnem1.x / Camera.main.pixelWidth, posEnem1.y / Camera.main.pixelHeight);
        //Debug.Log (posEnem);

        if (posEnem1.x > 0.75f)
        {
            playerRotate(player1, new Vector3(0, 1, 0));
        }
        if (posEnem1.x < 0.25f)
        {
            playerRotate(player1, new Vector3(0, -1, 0));
        }
        if (posEnem1.y > 0.75f)
        {
            playerRotate(player1, new Vector3(-1, 0, 0));
        }
        if (posEnem1.y < 0.25f)
        {
            playerRotate(player1, new Vector3(1, 0, 0));
        }

        //ここまで

        //ロックオン先のオブジェクトの取得
        Ray ray = Camera.main.ScreenPointToRay(posR1);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //Rayが当たるオブジェクトがあった場合はそのオブジェクト名をログに表示
            Debug.Log(hit.collider.gameObject.name);
        }

    }

    public static void playerRotate(GameObject target1, Vector3 x)
    {
        target1.transform.Rotate(x);
    }

}
