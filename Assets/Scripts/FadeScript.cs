using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour {

    public float fade_speed = 1.0f;
    float alpha;
    float red, blue, green;

    private bool isFadein;
    private bool isFadeout;

    public GameObject canvas;
    public GameObject background;
    private GameObject ObjToFadeIn;

    public GameObject BANS_obj;
    public GameObject PHEN_obj;
    public GameObject UNIC_obj;

    // Use this for initialization
    void Start () {
        isFadein = true;
        isFadeout = false;

        canvas.SetActive(false);
        background.SetActive(false);

        canvas.SetActive(false);
        background.SetActive(false);
        BANS_obj.SetActive(false);
        PHEN_obj.SetActive(false);
        UNIC_obj.SetActive(false);

        //乱数に対応した機体を背景に表示
        int Character_Num = new System.Random().Next(3);

        switch (Character_Num)
        {
            case 0:
                ObjToFadeIn = BANS_obj;
                break;
            case 1:
                ObjToFadeIn = PHEN_obj;
                break;
            case 2:
                ObjToFadeIn = UNIC_obj;
                break;
            default: break;
        }

        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
        alpha = GetComponent<Image>().color.a;

        //ObjToFadeIn = ChangePrefab.GetGameObj();

    }

    // Update is called once per frame
    void Update() {
        GetComponent<Image>().color = new Color(red, green, blue, alpha);
        
        //フェードインしてからフェードアウト
        if (isFadein)
        {
            alpha = alpha + 0.01f;
            //Debug.Log(alpha);
            if (alpha >= 2.0)
            {
                isFadein = false;
                isFadeout = true;
            }
        }
        if (isFadeout)
        {
            alpha = alpha - 0.01f;
            //Debug.Log(alpha);
            if (alpha <= 0)
            {
                wait();
                canvas.SetActive(true);
                background.SetActive(true);
                ObjToFadeIn.SetActive(true);

                isFadein = false;
                isFadeout = false;
            }
        }

    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
    }
}
