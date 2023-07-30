using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] private List<Transform> points;
    [SerializeField] private float moveSpeed;
    
    private Transform currentDestination;
    private int currentDestinationIndex;

    private void Start()
    {
        currentDestinationIndex = 0;
        currentDestination = points[currentDestinationIndex];
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentDestination.position, moveSpeed * Time.deltaTime);
        if (transform.position == currentDestination.position)
        {
            currentDestinationIndex++;
            if (currentDestinationIndex == points.Count)
                currentDestinationIndex = 0;
            
            currentDestination = points[currentDestinationIndex];
        }
            
    }
}
