using UnityEngine;
using System.Collections;
using System;

public class LevelWorker : MonoBehaviour {


    private LevelTeamWorker teamWorker;
    private LevelEnemyWorker enemyWorker;


    public void Start()
    {
        init(1);
    }
    public void init(int levelId)
    {
        EventControl.levelComplete += destroy;
        EventControl.levelFail += destroy;

        enemyWorker = GetComponent<LevelEnemyWorker>();
        teamWorker = GetComponent<LevelTeamWorker>();
        enemyWorker.init(this,levelId);
        teamWorker.init(this);       
    }

    private void destroy()
    {
        teamWorker.destroy();
        enemyWorker.destroy();
        removeAllUnits();       
    }

    private void removeAllUnits()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        UnitController control;
        for (int i=0;i< enemies.Length;i++)
        {
            control = enemies[i].GetComponent<UnitController>();
            if (control != null)
            {
                control.destroyUnit();
            }
        }
        
    }
}
