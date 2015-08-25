using UnityEngine;
using System.Collections;

public class TeamUnitSpawner : MonoBehaviour
{

    public TeamSpawner spawnerModel;
    public Transform[] spawnPositions;//позиции для спауна .юниотв
    private Transform moveFinalTarget;


    public void init(TeamSpawner sp, Transform[] positions, Transform moveFinalTarget)
    {
        this.moveFinalTarget = moveFinalTarget;
        spawnerModel = sp;
        spawnPositions = positions;
        spawnNext();
    }

    private IEnumerator spawnReload()
    {
        yield return new WaitForSeconds(spawnerModel.time);
        spawnNext();
    }

    private void spawnNext()
    {
        spawnUnit(spawnerModel.unitId);
        StartCoroutine(spawnReload());
    }

    private void spawnUnit(int id)
    {
        UnitInfo info = GameContext.inst.store.getUnitInfo(id);
        Transform pos = getRandomSpawnPosition();
        GameObject instant = GameContext.inst.getPrefabByName(info.prefabName);

        Vector3 spawpPos = new Vector3(pos.position.x + UnityEngine.Random.Range(-0.1f, 0.1f), pos.position.y + UnityEngine.Random.Range(-0.1f, 0.1f), pos.position.z);
        GameObject newUnit = (GameObject)Instantiate(instant, spawpPos, Quaternion.identity);
        UnitController unitControl = newUnit.GetComponent<UnitController>();
        unitControl.init(info.id, Teams.GOOD, moveFinalTarget);
    }
    public Transform getRandomSpawnPosition()
    {
        int index = Random.Range(0, spawnPositions.Length);
        return spawnPositions[index];
    }
}
