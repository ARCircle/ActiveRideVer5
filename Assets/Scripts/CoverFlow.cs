using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverFlow : MonoBehaviour
{

    public int numberOfPhotos = 4;
    public ArrayList PhotoObjects = new ArrayList();

    private Vector2 first = Vector2.zero;
    private Vector2 second = Vector2.zero;

    void Start()
    {

        LoadImages();
    }

    void LoadImages()
    {
        for (int nLoop = 0; nLoop < numberOfPhotos; nLoop++)
        {
            GameObject PhotoObject = GameObject.CreatePrimitive(PrimitiveType.Plane);

            PhotoObjects.Add(PhotoObject);

            PhotoObject.transform.position = new Vector3((nLoop - numberOfPhotos / 2) * 10f + 15, 0.5f, 0);

            PhotoObject.transform.eulerAngles = new Vector3(-270, (nLoop - numberOfPhotos / 2) * -45, 0);

           //PhotoObject.renderer.material.mainTexture = Resources.Load("photo" + nLoop);
        }
    }

    void MoveObject(int dir)
    {
        for (int nLoop = 0; nLoop < numberOfPhotos; nLoop++)
        {
            GameObject PhotoObject = PhotoObjects[nLoop] as GameObject;;

            Vector3 TmpTransform = PhotoObject.transform.position;
            TmpTransform.x += dir * 0.2f;
            PhotoObject.transform.position = TmpTransform;

            Vector3 TmpRotation = PhotoObject.transform.eulerAngles;
            TmpRotation.y += dir * 2.0f;
            PhotoObject.transform.eulerAngles = TmpRotation;
        }

    }

    void OnGUI()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveObject(-1);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            MoveObject(1);
        }

        if(Event.current.type == EventType.MouseDown)
        {
            first = Event.current.mousePosition;
        }

        if (Event.current.type == EventType.MouseDrag)
        {
            second = Event.current.mousePosition;
        }

        if (second.x < first.x)
        {
            print("Left");
            MoveObject(-1);
        }
        else if (second.x > first.x)
        {
            print("Right");
            MoveObject(1);
        }

        first = second;
    }
}
