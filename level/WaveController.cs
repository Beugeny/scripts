
using UnityEngine;

using System.Collections;
using System;


public class WaveController:MonoBehaviour
{
	private LevelEnemyWorker _enemyControl;
	private SpawnWave _wave;

	private int leftSpawnUnits=0;//Сколько юнитов еще осталось заспаунить


    public void init(LevelEnemyWorker enemyControl, SpawnWave wave)
	{
		//Debug.Log ("WaveController::init time="+wave.time);
		_wave = wave;
		_enemyControl = enemyControl;

		leftSpawnUnits = 0;
		for (int i=0; i<_wave.units.Length; i++) 
		{
			leftSpawnUnits+=_wave.units[i].count;
		}


		if (_wave.time == 0) {
			startWave (_wave);
		} else 
		{
			StartCoroutine(initSpawnWave(_wave));
		}

	}

	public bool isActive()
	{
		Debug.Log ("WaveController::leftSpawnUnits: "+leftSpawnUnits);
		return leftSpawnUnits > 0;
	}

	private IEnumerator initSpawnWave (SpawnWave wave)
	{
		yield return new WaitForSeconds(wave.time);
		startWave(wave);		
	}
	
	private void startWave (SpawnWave wave)
	{
		//Debug.Log ("WaveController::startWave");

		for (int i=0; i<_wave.units.Length; i++) 
		{
			if (_wave.units[i].time == 0) {
				spawnUnit (_wave.units[i]);
			} else 
			{
				StartCoroutine(initUnitSpawn(_wave.units[i]));
			}
		}
	}

	private IEnumerator initUnitSpawn (SpawnUnit unit)
	{
		yield return new WaitForSeconds(unit.time);
		spawnUnit(unit);
	}

	private void spawnUnit (SpawnUnit unit)
	{
		//Debug.Log("WaveController:spawnUnits count="+unit.count);
		for(int i=0;i<unit.count;i++)
		{
			leftSpawnUnits--;
			//Debug.Log("WaveController:spawnUnit id="+unit.unitId+" prefabName="+unit.unitInfo.prefabName);
			Transform pos=_enemyControl.getRandomSpawnPosition();
			GameObject instant=GameContext.inst.getPrefabByName(unit.unitInfo.prefabName);

			Vector3 spawpPos=new Vector3(pos.position.x+UnityEngine.Random.Range(-0.1f,0.1f),pos.position.y+UnityEngine.Random.Range(-0.1f,0.1f),pos.position.z);
			GameObject newUnit = (GameObject)Instantiate(instant,spawpPos,Quaternion.identity);
			UnitController unitControl= newUnit.GetComponent<UnitController>();
			unitControl.init(unit.unitId,Teams.BAD);
		}
	}
}


