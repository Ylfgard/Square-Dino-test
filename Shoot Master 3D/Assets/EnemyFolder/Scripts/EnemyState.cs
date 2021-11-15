using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public LevelStageKeeper keeper;
    public LevelStageHandler handler;
    public Transform playerTransf;
    public int stageIndex;
    [SerializeField] private EnemyBehavior behavior; 
    [SerializeField] private Collider hitCollider; 
    [SerializeField] private Rigidbody[] ragdollRB;
    private bool isDead = false;

    public void RagdollChangeState(bool state)
    {
        state = !state;
        behavior.animator.enabled = state;
        hitCollider.enabled = state;
        foreach(Rigidbody rb in ragdollRB)
            rb.isKinematic = state;
    }

    private void OnBecameInvisible() 
    {
        if(isDead)
            Destroy(behavior.gameObject, 2);
    }

    public void Death()
    {
        RagdollChangeState(true);
        behavior.canAttack = false;
        behavior.navAgent.isStopped = true;
        isDead = true;
        handler.RemoveEnemy(this);
        keeper.RemoveEnemy(this);
    }
}
