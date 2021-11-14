using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStageKeeper : MonoBehaviour
{
    public LevelStage[] levelStages;
    [SerializeField] private LevelStageHandler handler;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int stageCountEnemyPreload; //На сколько стадий вперёд заранее прогружать врагов
    [SerializeField] private List<EnemyState> loadedEnemys = new List<EnemyState>();
    private int curLevelStageIndex = 0;

    private void Awake()
    {
        int limit = curLevelStageIndex + stageCountEnemyPreload;
        if(limit >= levelStages.Length) limit = levelStages.Length - 1;
        for(int i = curLevelStageIndex; i <= limit; i++)
            foreach(Transform enemyPoint in levelStages[i].enemyPositions)
                LoadEnemy(enemyPoint, i);
    }

    public void GetLevelStage(out Transform[] wayPoints)
    {
        wayPoints = levelStages[curLevelStageIndex].wayPoints;
        int limit = curLevelStageIndex + stageCountEnemyPreload + 1;
        if(limit < levelStages.Length) 
        {
            Debug.Log("Spawn");
            foreach(Transform enemyPoint in levelStages[limit].enemyPositions)
                LoadEnemy(enemyPoint, limit);
        }
            
        
        foreach(EnemyState enemy in loadedEnemys)
        {
            if(enemy.stageIndex == curLevelStageIndex)
            {
                handler.curEnemys.Add(enemy);
                handler.playerMovement.onPosition.AddListener(enemy.GetComponent<EnemyBehavior>().StartFight);
            }
        }

        curLevelStageIndex++;
        if(curLevelStageIndex >= levelStages.Length)
            handler.isFinalStage = true;
    }

    private void LoadEnemy(Transform enemyPoint, int stageIndex)
    {
        Quaternion rotate = Quaternion.LookRotation(handler.playerMovement.transform.position, handler.playerMovement.transform.position);
        EnemyState enemy = Instantiate(enemyPrefab, enemyPoint.position, rotate).GetComponent<EnemyState>();
        enemy.stageIndex = stageIndex;
        enemy.keeper = this;
        loadedEnemys.Add(enemy);
        enemy.handler = handler;
        enemy.playerTransf = handler.playerMovement.transform;
    }

    public void RemoveEnemy(EnemyState enemy)
    {
        loadedEnemys.Remove(enemy);
    }

    private void OnDrawGizmos()
    {
        for(int i = 0; i<levelStages.Length; i++)
        {
            for(int j = 0; j<levelStages[i].wayPoints.Length-1; j++)
                Gizmos.DrawLine(levelStages[i].wayPoints[j].position, levelStages[i].wayPoints[j+1].position);
            if(i < levelStages.Length - 1)
                Gizmos.DrawLine(levelStages[i].wayPoints[levelStages[i].wayPoints.Length-1].position, levelStages[i+1].wayPoints[0].position);
        }
    }
}

[System.Serializable]
public struct LevelStage
{
    public Transform[] wayPoints;
    public Transform[] enemyPositions;
}
