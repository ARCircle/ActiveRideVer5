using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionCoverFlow : MonoBehaviour {

	public GameObject MotherObject;
	public List<GameObject> PhotosinGallery = new List<GameObject>();

	private GameObject tmpPhoto;

	protected int NumberOfObject = 5;

    private int X_MAX = 15;
	private int Y_MAX = 15;

    private int CenterButtonIndex;

	// Use this for initialization
	void Start () {

		viewConvexPhoto();
		setPhotoInActive();

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
			float X = X_MAX * Mathf.Cos((nLoop - 1) * (360 / NumberOfObject) * (Mathf.PI / 180));
			float Z = Y_MAX * Mathf.Sin((nLoop - 1) * (360 / NumberOfObject) * (Mathf.PI / 180));
			/*
			float rotY = (nLoop - 1) * 360 / NumberOfObject;
			if (rotY == 0)
			{
				rotY += 90;
				X = 0;
			}
			*/
			PhotosinGallery[nLoop].transform.localPosition = new Vector3(X, 0, Z);
			//PhotosinGallery[nLoop].transform.localEulerAngles = new Vector3(0, rotY, 0);

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
