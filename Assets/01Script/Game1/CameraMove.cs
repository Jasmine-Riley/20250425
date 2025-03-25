using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Transform target;
    [SerializeField] private Vector3 offset;
    private GameObject obj;

    private void Awake()
    {
        obj = GameObject.Find("Player");

        if (obj != null)
            target = obj.transform;
    }

    private void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}