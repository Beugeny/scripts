using UnityEngine;
using System.Collections;

public class LevelEnemyWorker : MonoBehaviour {

    public Transform[] spawnPositions;//позиции для спауна .юниотв
    private LeveInfo level;
    private LevelWorker _levelWorker;
    private WaveController[] waves;


    public void init(LevelWorker levelController,int id)
    {
        _levelWorker = levelController;
        EventControl.killUnit += onUnitKilled;
        EventControl.lifeChanged += onLifeChanged;
        
        level = GameContext.inst.store.getLevelInfo(id);
        if (level == null)
        {
            throw new System.NullReferenceException("level nust be not null");
        }

        if (waves == null)
        {
            waves = new WaveController[level.spawn.waves.Length];
        }
        for (int i = 0; i < level.spawn.waves.Length; i++)
        {
            WaveController wave = gameObject.AddComponent<WaveController>();
            waves.SetValue(wave, i);
            wave.init(this, level.spawn.waves[i]);
        }
    }


   public Transform getRandomSpawnPosition()
    {
        int index = Random.Range(0, spawnPositions.Length);
        //Debug.Log ("spawn index="+index+" len="+spawnPositions.Length);
        return spawnPositions[index];
    }

    void onUnitKilled(UnitPoint unitInfo)
    {
        if (!hasActiveWaves())
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            int badEnemies = 0;
            UnitController control;
            for (int i = 0; i < enemies.Length; i++)
            {
                control = enemies[i].GetComponent<UnitController>();
                if (control != null && !control.unitPoint.isDie() && control.unitPoint.team == Teams.BAD)
                {
                    badEnemies++;
                }
            }

            if (badEnemies == 0)
            {
                EventControl.onLevelComplete();
            }
        }
    }

    void onLifeChanged()
    {
        if (GameContext.inst.store.playerInfo.lifes <= 0)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++)
            {
                Destroy(enemies[i].gameObject);
            }
            Debug.Log("on level fail");
            EventControl.onlevelFail();
        }
        onUnitKilled(null);
    }

    bool hasActiveWaves()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            //Debug.Log("waves");
            if (waves[i].isActive()) return true;
        }
        return false;
    }
	
}
