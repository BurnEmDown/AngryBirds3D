using System;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 100;
    [SerializeField] private float fallSpeed = 100;

    [SerializeField] private bool rotate = true;
    private bool isOnGround = false;

    private void Start()
    {
        RotateToGround();
    }

    void Update()
    {
        if(rotate)
            transform.Rotate( new Vector3(0, Time.deltaTime * rotateSpeed, 0) );

        if(!isOnGround)
            RotateToGround();
    }

    public void StopRotation()
    {
        rotate = false;
    }

    public void RotateToGround()
    {
        transform.Rotate(new Vector3(-fallSpeed * Time.deltaTime, 0, 0));

        // mark object rotation to be "flat" so it looks like it's "fallen on the ground"
        if (Math.Abs(transform.rotation.eulerAngles.x - 270) <= 1f)
            isOnGround = true;
    }
}
