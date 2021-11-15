using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public UnityEvent onPosition;
    public Transform lookAtPoint;
    public Animator animator;
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private Transform _transform;
    private Transform[] wayPoints;
    private int curWayPointIndex = 0;
    public void GetWayPoints(Transform[] wayPoints)
    {
        this.wayPoints = wayPoints;
        curWayPointIndex = 0;
        GoToNextWayPoint();
        navAgent.isStopped = false;
        animator.SetBool("Walk", true);
    }

    private void GoToNextWayPoint()
    {
        if(curWayPointIndex >= wayPoints.Length)
        {
            onPosition?.Invoke();
            animator.SetBool("Walk", false);
            navAgent.isStopped = true;
        }
        else
        {
            navAgent.SetDestination(wayPoints[curWayPointIndex++].position);
        }
    }

    private void Update()
    {
        if(navAgent.isStopped == false)
        {
            if(Vector3.Distance(navAgent.nextPosition, navAgent.destination) <= navAgent.stoppingDistance)
            {
                GoToNextWayPoint();
            }
            if(navAgent.isOnOffMeshLink)
                animator.SetTrigger("Jump");
        }
        else
        {
            Quaternion rotate = Quaternion.LookRotation(lookAtPoint.position - _transform.position);
            rotate.x = 0; rotate.z = 0;
            _transform.rotation = rotate;
        }
    }
}
