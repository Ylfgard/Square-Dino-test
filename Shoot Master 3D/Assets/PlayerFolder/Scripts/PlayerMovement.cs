using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navAgent;
    private Transform[] wayPoints;
    private int curWayPointIndex = 0;
    private bool inMovement = false;
    public UnityEvent onPosition;

    public void GetWayPoints(Transform[] wayPoints)
    {
        this.wayPoints = wayPoints;
        curWayPointIndex = 0;
        GoToNextWayPoint();
        inMovement = true;
    }

    private void GoToNextWayPoint()
    {
        if(curWayPointIndex >= wayPoints.Length)
        {
            onPosition?.Invoke();
            inMovement = false;
        }
        else
        {
            navAgent.SetDestination(wayPoints[curWayPointIndex++].position);
        }
    }

    private void Update()
    {
        if(inMovement)
        {
            if(Vector3.Distance(navAgent.nextPosition, navAgent.destination) <= navAgent.stoppingDistance)
            {
                GoToNextWayPoint();
            }
        }
    }
}
