using System;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 100;
    

    void Update()
    {
        transform.Rotate( new Vector3(0, Time.deltaTime * rotateSpeed, 0));
    }

}
