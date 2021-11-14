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
    private bool isDead = false;

    public void Death()
    {
        isDead = true;
        handler.RemoveEnemy(this);
        keeper.RemoveEnemy(this);
        Destroy(gameObject);
    }
}
