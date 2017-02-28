
using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GalleryCoverFlow : MonoBehaviour
{

    public List<GameObject> PhotosinGallery = new List<GameObject>();

    private GameObject tmpPhoto;

    private GameObject centerPhoto;

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
    private string[] DescriptionText = { "あああ", "いいい", "ううう", "えええ", "おおお", "がはは" };

    // Use this for initialization
    void Start()
    {

        NumberOfObject = PhotosinGallery.Count;
        Debug.Log(NumberOfObject);

        viewConvexPhoto();
        centerPhoto = setPhotoInActive();

        ChangeCenterPhotoSize();
        ChangeDescriptionText(centerPhoto);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            tmpPhoto = PhotosinGallery[0];
            for (int nLoop = 0; nLoop < NumberOfObject - 1; nLoop++)
            {
                PhotosinGallery[nLoop] = PhotosinGallery[nLoop + 1];
            }
            PhotosinGallery[NumberOfObject - 1] = tmpPhoto;

            intensify = 0.1f;

            centerPhoto = setPhotoInActive();
            ChangeDescriptionText(centerPhoto);
            ChangeCenterPhotoSize();

            SetTransitionToPhoto(centerPhoto);

            viewConvexPhoto();

        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            tmpPhoto = PhotosinGallery[NumberOfObject - 1];
            for (int nLoop = NumberOfObject - 1; nLoop > 0; nLoop--)
            {
                PhotosinGallery[nLoop] = PhotosinGallery[nLoop - 1];
            }
            PhotosinGallery[0] = tmpPhoto;

            intensify = 0.1f;

            centerPhoto = setPhotoInActive();
            ChangeDescriptionText(centerPhoto);
            ChangeCenterPhotoSize();

            SetTransitionToPhoto(centerPhoto);

            viewConvexPhoto();

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

    //なんだか汚いのでもっとスマートにかきたい
    private void ChangeDescriptionText(GameObject CenterOfPhoto)
    {
        //int index = Int32.Parse(CenterOfPhoto.tag);
        //TextOfPhotos.GetComponent<UnityEngine.UI.Text>().text = DescriptionText[index]; 
        switch (CenterOfPhoto.name)
        {
            case "Picture1":
                TextOfPhotos.GetComponent<UnityEngine.UI.Text>().text = DescriptionText[0];
                break;
            case "Picture2":
                TextOfPhotos.GetComponent<UnityEngine.UI.Text>().text = DescriptionText[1];
                break;
            case "Picture3":
                TextOfPhotos.GetComponent<UnityEngine.UI.Text>().text = DescriptionText[2];
                break;
            case "Picture4":
                TextOfPhotos.GetComponent<UnityEngine.UI.Text>().text = DescriptionText[3];
                break;
            case "Picture5":
                TextOfPhotos.GetComponent<UnityEngine.UI.Text>().text = DescriptionText[4];
                break;
            case "Picture6":
                TextOfPhotos.GetComponent<UnityEngine.UI.Text>().text = DescriptionText[5];
                break;
            default: break;
        }

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

            //PhotosinGallery[nLoop].transform.localEulerAngles = new Vector3(0, rotY, 0);

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
        //if (centerPhoto.GetComponent<UIMaskTransparent>() != null)
        //{
        //    UnityEngine.Object target = centerPhoto.GetComponent<UIMaskTransparent>();
        //    UnityEngine.Object.Destroy(target);
        //}

        for (int nLoop = 0; nLoop < NumberOfObject; nLoop++)
        {

            if (PhotosinGallery[nLoop].GetComponent<UIMaskTransparent>() != null)
            {
                //UnityEngine.Object target = PhotosinGallery[nLoop].GetComponent<UIMaskTransparent>();
                //UnityEngine.Object.Destroy(target);

                //Debug.Log(PhotosinGallery[nLoop].GetComponent<UIMaskTransparent>());
                //PhotosinGallery[nLoop].GetComponent<UIMaskTransparent>().enabled = true;

            }
            else if (PhotosinGallery[nLoop].GetComponent<UIMaskTransparent>() == null)
            {

                PhotosinGallery[nLoop].GetComponentInChildren<UnityEngine.UI.Image>().material = UI_mat;

                PhotosinGallery[nLoop].AddComponent<UIMaskTransparent>();

                PhotosinGallery[nLoop].GetComponent<UIMaskTransparent>().UI_mask_mat = UI_mat;
                Debug.Log("test" + PhotosinGallery[nLoop].GetComponent<UIMaskTransparent>().UI_mask_mat);
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

}