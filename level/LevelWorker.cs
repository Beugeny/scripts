using UnityEngine;
using System.Collections;

public class LevelWorker : MonoBehaviour {


    private LevelTeamWorker teamWorker;
    private LevelEnemyWorker enemyWorker;


    public void Start()
    {
        init(1);
    }
    public void init(int levelId)
    {
        enemyWorker= GetComponent<LevelEnemyWorker>();
        teamWorker = GetComponent<LevelTeamWorker>();
        enemyWorker.init(this,levelId);
        teamWorker.init(this);
    }
}
