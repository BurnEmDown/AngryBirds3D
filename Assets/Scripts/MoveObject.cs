using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] private Transform movePointsParent;
    [SerializeField] private float moveSpeed;
    
    private List<Transform> movePoints;
    private Transform currentDestination;
    [SerializeField] private int currentDestinationIndex;
    private bool isMoving = true;

    private void Start()
    {
        SetMovePointsFromParent();
        currentDestination = movePoints[currentDestinationIndex];
    }

    private void Update()
    {
        if(isMoving)
            MoveToTarget();
    }

    private void SetMovePointsFromParent()
    {
        movePoints = new List<Transform>();
        
        foreach (Transform movePoint in movePointsParent)
        {
            movePoints.Add(movePoint);            
        }
    }

    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentDestination.position, moveSpeed * Time.deltaTime);
        if (transform.position == currentDestination.position)
        {
            currentDestinationIndex++;
            if (currentDestinationIndex == movePoints.Count)
                currentDestinationIndex = 0;
            
            currentDestination = movePoints[currentDestinationIndex];
        }
    }

    public void StopMoving()
    {
        isMoving = false;
    }

    public void SetMoveIndex(int index)
    {
        currentDestinationIndex = index;
    }
}
