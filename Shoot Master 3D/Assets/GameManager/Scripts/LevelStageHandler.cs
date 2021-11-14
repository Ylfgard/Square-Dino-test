using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStageHandler : MonoBehaviour
{
    
    public bool isFinalStage = false;
    public PlayerMovement playerMovement;
    public List<EnemyState> curEnemys = new List<EnemyState>();
    [SerializeField] private LevelStageKeeper levelStageKeeper;

    private void Start() 
    {
        isFinalStage = false;
        RunNextLevelStage();
    }

    private void RunNextLevelStage() 
    {
        playerMovement.onPosition.RemoveAllListeners();
        if(isFinalStage == false)
        {
            Transform[] wayPoints;
            levelStageKeeper.GetLevelStage(out wayPoints);
            playerMovement.GetWayPoints(wayPoints);
            if(curEnemys.Count == 0)
                playerMovement.onPosition.AddListener(RunNextLevelStage);
        }
        else
        {
            Debug.Log("End");
        }
    }

    public void RemoveEnemy(EnemyState enemy)
    {
        curEnemys.Remove(enemy);
        if(curEnemys.Count == 0)
            RunNextLevelStage();
    }
}
