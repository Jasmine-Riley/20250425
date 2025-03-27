using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Transform target;
    [SerializeField] private Vector3 offset;
    private GameObject obj;
    private Vector3 cameraPos;

    private float angle;

    private void Awake()
    {
        obj = GameObject.Find("Player");

        if (obj != null)
            target = obj.transform;

        cameraPos = target.position;
    }

    private void LateUpdate()
    {
        cameraPos = target.position + offset;
        cameraPos.y = offset.y;

        transform.position = cameraPos;

        //transform.RotateAround(cameraPos, Vector3.up, 5);
    }
}