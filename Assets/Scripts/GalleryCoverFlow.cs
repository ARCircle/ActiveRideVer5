using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class GalleryCoverFlow : MonoBehaviour
{
    public List<GameObject> PhotosinGallery = new List<GameObject>();

    private GameObject tmpPhoto;
    private GameObject centerPhoto;
    public GameObject centerPhotoFrame;

    private SpriteRenderer spriteRenderer;

    protected int NumberOfObject;

    private float X, Y, Z;
    private float Rotate_X, Rotate_Y, Rotate_Z;

    private int Z_MAX = 60;
    private int Y_MAX = 250;
    private int X_MAX = 100;

    private int CenterButtonIndex;

    private float intensify = 0.1f;

    public Material UI_mat;

    public GameObject TextOfPhotos;
    public GameObject TitleOfPhotos;

    private string[,] CSVdata;
    private const string path = "/CSV/Picture1.csv";

    private string[] DescriptionText = { "あああ", "いいい", "ううう", "えええ", "おおお", "がはは" };

    public Dictionary<string, string> MappingDescription = new Dictionary<string, string>();
    public Dictionary<string, string> MappingTitle = new Dictionary<string, string>();

    // Use this for initialization
    void Start()
    {
        //CSVファイルよりTitleとDesctriptionを読み込み
        ShowUIText showUIText = new ShowUIText();
        showUIText.readCSVData(Application.dataPath + path, ref CSVdata);
        MappingCSV(ref CSVdata);

        NumberOfObject = PhotosinGallery.Count;

        viewConvexPhoto();
        centerPhoto = setPhotoInActive();

        ChangeCenterPhotoSize();
        ChangeDescriptionText(centerPhoto);
    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log(centerPhotoFrame.gameObject.GetComponent<ViewCenterFrame>().enabled);
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)
            || Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Vertical2") > 0
            || Input.GetAxisRaw("Vertical") < 0 || Input.GetAxisRaw("Vertical2") < 0
            )
        {
            intensify = 0.1f;

            centerPhoto = setPhotoInActive();
            ChangeDescriptionText(centerPhoto);
            ChangeCenterPhotoSize();
            
            SetTransitionToPhoto(centerPhoto);

            //in ViewCenterFrame.cs
            //中央のフレームを再表示
            centerPhotoFrame.gameObject.GetComponent<ViewCenterFrame>().enabled = true;

            viewConvexPhoto();
        }

        if (Input.GetKeyDown(KeyCode.W) 
            || Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Vertical2") > 0)
        {
            tmpPhoto = PhotosinGallery[0];
            for (int nLoop = 0; nLoop < NumberOfObject - 1; nLoop++)
            {
                PhotosinGallery[nLoop] = PhotosinGallery[nLoop + 1];
            }
            PhotosinGallery[NumberOfObject - 1] = tmpPhoto;

        }

        if (Input.GetKeyDown(KeyCode.S)
            || Input.GetAxisRaw("Vertical") < 0 || Input.GetAxisRaw("Vertical2") < 0)
        {
            tmpPhoto = PhotosinGallery[NumberOfObject - 1];
            for (int nLoop = NumberOfObject - 1; nLoop > 0; nLoop--)
            {
                PhotosinGallery[nLoop] = PhotosinGallery[nLoop - 1];
            }
            PhotosinGallery[0] = tmpPhoto;

        }
        intensify = FlashSprite(intensify);
    }

    static void Swap<T>(ref T lhs, ref T rhs)
    {
        T temp;
        temp = lhs;
        lhs = rhs;
        rhs = temp;
    }

    private void ChangeDescriptionText(GameObject CenterOfPhoto)
    {
        TitleOfPhotos.GetComponent<UnityEngine.UI.Text>().text = MappingTitle[CenterOfPhoto.name].ToString();
        TextOfPhotos.GetComponent<UnityEngine.UI.Text>().text = MappingDescription[CenterOfPhoto.name].ToString();    
    }

    private void viewConvexPhoto()
    {
        for (int nLoop = 0; nLoop < NumberOfObject; nLoop++)
        {
            Z = Mathf.Abs(Z_MAX * Mathf.Cos((nLoop - 1) * (360 / NumberOfObject) * (Mathf.PI / 180)));
            Y = 1.2f * Y_MAX * Mathf.Sin((nLoop - 1) * (360 / NumberOfObject) * (Mathf.PI / 180));
            X = 1.2f * X_MAX * Mathf.Sin((nLoop - 1) * (360 / NumberOfObject) * (Mathf.PI / 180));

            Rotate_X = 60 * Mathf.Sin((nLoop - 1) * (360 / NumberOfObject) * (Mathf.PI / 180));

            PhotosinGallery[nLoop].transform.localPosition = new Vector3(X, Y, -Z);
            PhotosinGallery[nLoop].transform.localEulerAngles = new Vector3(Rotate_X, 0, 0);
        }
    }

    private void ChangeCenterPhotoSize()
    {
        for (int nLoop = 0; nLoop < NumberOfObject; nLoop++)
        {
            if (nLoop == (NumberOfObject / 2) - 2)
            {
                PhotosinGallery[nLoop].transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            }
            else
            {
                PhotosinGallery[nLoop].transform.localScale = new Vector3(1f, 1f, 1f);
            }

        }
    }

    private void SetTransitionToPhoto(GameObject centerPhoto)
    { 
        //トランジション用のコンポーネント割り当て
        for (int nLoop = 0; nLoop < NumberOfObject; nLoop++)
        {

            if (PhotosinGallery[nLoop].GetComponent<UIMaskTransparent>() == null)
            {
                PhotosinGallery[nLoop].GetComponentInChildren<UnityEngine.UI.Image>().material = UI_mat;

                PhotosinGallery[nLoop].AddComponent<UIMaskTransparent>();

                PhotosinGallery[nLoop].GetComponent<UIMaskTransparent>().UI_mask_mat = UI_mat;
                PhotosinGallery[nLoop].GetComponent<UIMaskTransparent>().enabled = false;
            }

            if (PhotosinGallery[nLoop] != centerPhoto)
            {
                PhotosinGallery[nLoop].GetComponentInChildren<UnityEngine.UI.Image>().material = null;

                UnityEngine.Object target = PhotosinGallery[nLoop].GetComponent<UIMaskTransparent>();
                UnityEngine.Object.Destroy(target);
            }
            else
            {
                PhotosinGallery[nLoop].GetComponent<UIMaskTransparent>().enabled = true;
                PhotosinGallery[nLoop].GetComponentInChildren<UnityEngine.UI.Image>().material = UI_mat;
            }
        }

    }

    private GameObject setPhotoInActive()
    {
        for (int nLoop = 0; nLoop < NumberOfObject; nLoop++)
        {
            if (nLoop >= NumberOfObject / 2)
            {
                PhotosinGallery[nLoop].SetActive(false);
            }
            else
            {
                PhotosinGallery[nLoop].SetActive(true);
            }
        }
        return PhotosinGallery[(NumberOfObject / 2) - 2];
    }

    float FlashSprite(float intensify)
    {
        if (intensify >= 1f)
        {
            intensify = 1f;
            centerPhoto.GetComponentInChildren<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f);
        }
        else
        {
            centerPhoto.GetComponentInChildren<UnityEngine.UI.Image>().color = new Color(intensify, intensify, intensify);

            intensify = intensify * 1.1f;
        }
        return intensify;
    }

    //string[,]配列に読み込んだCSVデータを(Title, Descriptionの)Dictionaryにマッピング
    private void MappingCSV(ref string[,] data)
    {
        for (int i = 0; i < data.GetLength(0); i++)
        {
            data[i, 2] = data[i, 2].Replace("*" , Environment.NewLine);
            MappingTitle.Add(data[i, 0], data[i, 1]);
            MappingDescription.Add(data[i, 0], data[i, 2]);
        }
    }

}