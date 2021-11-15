using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public bool canAttack = false;
    public Animator animator;
    public NavMeshAgent navAgent;
    [SerializeField] private EnemyState state; 
    [SerializeField] private int health;

    private void Start()
    {
        canAttack = false;
        state.RagdollChangeState(false);
    }

    public void StartFight()
    {
        StartCoroutine(RandomDelayedWalk());
    }

    private IEnumerator RandomDelayedWalk()
    {
        yield return new WaitForSeconds(Random.Range(0, 1.5f));
        canAttack = true;
        animator.SetBool("Walk", true);
        navAgent.SetDestination(state.playerTransf.position);
    } 

    private void Update()
    {
        if(canAttack && Vector3.Distance(navAgent.nextPosition, navAgent.destination) <= navAgent.stoppingDistance)
        {
            animator.SetBool("Walk", false);
            canAttack = false;
            animator.SetTrigger("Attack");
        }
    }

    public void Attack()
    {
        state.playerTransf.GetComponent<PlayerFight>().TakeDamage();
    } 
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health < 1)
            state.Death();
    }
}
