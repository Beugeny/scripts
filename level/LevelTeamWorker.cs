using UnityEngine;
using System.Collections;


public class LevelTeamWorker : MonoBehaviour
{
    public Transform moveFinalTarget;
    private TeamUnitSpawner[] unitSpawners;
    private PlayerInfo playerInfo;
    public Transform[] spawnPositions;//позиции для спауна .юниотв
    public void init(LevelWorker level)
    {
        playerInfo = GameContext.inst.store.playerInfo;

        unitSpawners=new TeamUnitSpawner[playerInfo.spawner.spawners.Length];
        TeamUnitSpawner spawner;
        for(int i=0;i<playerInfo.spawner.spawners.Length;i++)
        {
            spawner = gameObject.AddComponent<TeamUnitSpawner>();
            spawner.init(playerInfo.spawner.spawners[i], spawnPositions, moveFinalTarget);
        }
    }
}

