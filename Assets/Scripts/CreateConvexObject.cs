using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateConvexObject : MonoBehaviour {

    public GameObject PhotoObject1;
    public GameObject PhotoObject2;
    public GameObject PhotoObject3;

    public GameObject MotherObject;
    public List<GameObject> PhotosinGallery = new List<GameObject>();

    private GameObject tmpPhoto;

    private int NumberOfObject;

    private int X_MAX = 15;
    private int Y_MAX = 15;

	private float X,Y,Z;
	private float rotX, rotY, rotZ;

    private void Awake()
    {
        NumberOfObject = PhotosinGallery.Count;
    }

	// Use this for initialization
	void Start () {

        viewConvexPhoto();
        setPhotoInActive();

        /*
        float X1 = X_MAX * Mathf.Cos(1 * (360 / NumberOfObject) * (Mathf.PI / 180));
        float Z1 = Y_MAX * Mathf.Sin(1 * (360 / NumberOfObject) * (Mathf.PI / 180));
        float rotY1 = 1 * 360 / NumberOfObject;
        PhotoObject1.transform.localPosition = new Vector3(X1, 0 ,Z1);
        PhotoObject1.transform.localEulerAngles = new Vector3(0,rotY1,0);

        float X2 = X_MAX * Mathf.Cos(0 * (360 / NumberOfObject) * (Mathf.PI / 180));
        float Z2 = Y_MAX * Mathf.Sin(0 * (360 / NumberOfObject) * (Mathf.PI / 180));
        float rotY2 = 0 * 360 / NumberOfObject;
        PhotoObject2.transform.localPosition = new Vector3(X2, 0, Z2);
        PhotoObject2.transform.localEulerAngles = new Vector3(0, 90, 0);

        float X3 = X_MAX * Mathf.Cos(-1 * (360 / NumberOfObject) * (Mathf.PI / 180));
        float Z3 = Y_MAX * Mathf.Sin(-1 * (360 / NumberOfObject) * (Mathf.PI / 180));
        float rotY3 = -1 * 360 / NumberOfObject;
        PhotoObject3.transform.localPosition = new Vector3(X3, 0, Z3);
        PhotoObject3.transform.localEulerAngles = new Vector3(0,rotY3, 0);
        */
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.W))
        {
            tmpPhoto = PhotosinGallery[0];
            for (int nLoop = 0; nLoop < NumberOfObject - 1 ; nLoop++)
            {
                PhotosinGallery[nLoop] = PhotosinGallery[nLoop + 1];
            }
            PhotosinGallery[NumberOfObject - 1] = tmpPhoto;
            viewConvexPhoto();
            setPhotoInActive();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            tmpPhoto = PhotosinGallery[NumberOfObject - 1];
            for (int nLoop = NumberOfObject - 1; nLoop > 0; nLoop--)
            {
                PhotosinGallery[nLoop] = PhotosinGallery[nLoop - 1];
            }
            PhotosinGallery[0] = tmpPhoto;
            viewConvexPhoto();
            setPhotoInActive();
        }
    }

    static void Swap<T>(ref T lhs, ref T rhs)
    {
        T temp;
        temp = lhs;
        lhs = rhs;
        rhs = temp;
    }
    private void viewConvexPhoto()
    {
        for (int nLoop = 0; nLoop < NumberOfObject; nLoop++)
        {
            X = X_MAX * Mathf.Cos((nLoop - 1) * (360 / NumberOfObject) * (Mathf.PI / 180));
            Z = Y_MAX * Mathf.Sin((nLoop - 1) * (360 / NumberOfObject) * (Mathf.PI / 180));
            rotY = (nLoop - 1) * 360 / NumberOfObject;

            if (rotY == 0)
            {
                rotY += 90;
                X = 0;
            }
            PhotosinGallery[nLoop].transform.localPosition = new Vector3(X, 0, Z);
            PhotosinGallery[nLoop].transform.localEulerAngles = new Vector3(0, rotY, 0);

        }
    }
	private void setPhotoInActive()
    {
        for (int nLoop = 0; nLoop < NumberOfObject ; nLoop++)
        {
            if (nLoop >= NumberOfObject / 2)
            {
                PhotosinGallery[nLoop].SetActive(false);
            }else
            {
                PhotosinGallery[nLoop].SetActive(true);
            }

        }
    }
}
