using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// offset 0.05 / 6.8 / -5.44
public class CameraMove : MonoBehaviour
{
    //private Transform target;

    //public float Yaxis;
    //public float Xaxis;
    //private float rotSensitive = 3f;
    //private float dis = 8.708393f;
    ////private float dis = 5.44f;

    //private float RotationMin = -10f;
    //private float RotationMax = 80f;
    //private float smoothTime = 0.12f;

    //private Vector3 targetRotation;
    //private Vector3 currentVel;


    //private GameObject obj;

    //private void Awake()
    //{
    //    Debug.Log(Vector3.Distance(Vector3.zero, new Vector3(0.05f, 6.8f, -5.44f)));

    //    obj = GameObject.Find("Player");

    //    if (obj != null)
    //        target = obj.transform;
    //}

    //private void LateUpdate()
    //{
    //    Yaxis = Yaxis + Input.GetAxis("Mouse X") * rotSensitive;
    //    //Xaxis = Xaxis + Input.GetAxis("Mouse Y") * rotSensitive;

    //    Xaxis = Mathf.Clamp(Xaxis, RotationMin, RotationMax);

    //    targetRotation = Vector3.SmoothDamp(targetRotation, new Vector3(Xaxis, Yaxis, -0.255f), ref currentVel, smoothTime);
    //    this.transform.eulerAngles = targetRotation;

    //    transform.position = target.position - transform.forward * dis;
    //}

    private Transform target;
    [SerializeField] private Vector3 offset;
    private GameObject obj;
    private Vector3 cameraPos;
    private float removeY;
    private float angle;

    private void Awake()
    {
        obj = GameObject.Find("Player");

        if (obj != null)
            target = obj.transform;

        cameraPos = target.position;
        removeY = target.transform.position.y;
    }

    private void LateUpdate()
    {
        cameraPos = target.position + offset;
        cameraPos.y -= removeY;

        transform.position = cameraPos;
    }
}