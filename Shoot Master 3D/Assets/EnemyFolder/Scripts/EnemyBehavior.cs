using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public bool canAttack = false;
    [SerializeField] NavMeshAgent navAgent;
    [SerializeField] private EnemyState state; 
    [SerializeField] private int health;

    private void Start()
    {
        canAttack = false;
    }

    public void StartFight()
    {
        canAttack = true;
        navAgent.SetDestination(state.playerTransf.position);
    }

    private void Update()
    {
        if(canAttack && Vector3.Distance(navAgent.nextPosition, navAgent.destination) <= navAgent.stoppingDistance)
            Attack();
    }

    private void Attack()
    {
        canAttack = false;
        state.playerTransf.GetComponent<PlayerFight>().TakeDamage();
    } 
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
        if(health < 1)
            state.Death();
    }
}
