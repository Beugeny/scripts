using UnityEngine;
using System.Collections;
using System;

public class Store {
	public Carier carier;
	public LeveInfo[] levels;
	public PlayerInfo playerInfo;
	public UnitInfo[] units;


	public Store()
	{
		carier = new Carier ();

		//<<TEST DATA

		

		//UNITS
		units=new UnitInfo[3];
		WeaponInfo weapon = new WeaponInfo ();
		weapon.force = 3;
		weapon.radius = 1;
		weapon.reloadTime = 1;

		UnitInfo unit = new UnitInfo ();
		unit.life = 10;
		unit.prefabName = "SimpleUnit";
		unit.id = 0;
		unit.armor = 1;
		unit.moveSpeed = 1;
		unit.weapon = weapon;
		unit.killCash = 1;
		unit.killScore = 2;

		units.SetValue (unit,0);

		//SPAWNER
		SpawnerInfo spawner = new SpawnerInfo ();
		spawner.waves=new SpawnWave[1];
		
		SpawnWave wave1 = new SpawnWave ();
		wave1.time = 0f;
		wave1.units=new SpawnUnit[1];

		SpawnUnit spawnUnit = new SpawnUnit ();
		spawnUnit.unitId = 0;
		spawnUnit.time = 1f;
		spawnUnit.count = 3;

		/*SpawnUnit spawnUnit2 = new SpawnUnit ();
		spawnUnit2.unitId = 0;
		spawnUnit2.time = 3;
		spawnUnit2.count = 1;*/

		wave1.units.SetValue (spawnUnit,0);
		//wave1.units.SetValue (spawnUnit,1);

		spawner.waves.SetValue (wave1, 0);
		//spawner.waves.SetValue (wave1, 1);

		//LEVELS
		levels=new LeveInfo[2];
		
		LeveInfo levelInfo=new LeveInfo();
		levelInfo.id=1;
		levelInfo.info="TestLevel1";
		levelInfo.spawn = spawner;
		levels.SetValue(levelInfo,0);
		
		levelInfo=new LeveInfo();
		levelInfo.id=2;
		levelInfo.info="TestLevel2";
		levelInfo.spawn = spawner;
		levels.SetValue(levelInfo,1);

		//LEVEL PROGRESS
		LevelProgress level = new LevelProgress ();
		level.id = 1;
		carier.levelsProgress.SetValue (level, 1);

        //PLAYER
        playerInfo = new PlayerInfo();
        playerInfo.lifes = 3;
        playerInfo.spawner.spawners = new TeamSpawner[1];
        TeamSpawner teamSpawner = new TeamSpawner();
        teamSpawner.id = 0;
        teamSpawner.unitId = 0;
        teamSpawner.time = 2;
        playerInfo.spawner.spawners.SetValue(teamSpawner, 0);
        carier.playerInfo = playerInfo;

		//TEST DATA>>

	}

	public UnitInfo getUnitInfo(int id)
	{
		for (int i=0; i<units.Length; i++) 
		{
			if(units[i].id==id)
			{
				return units[i];
			}
		}
		return null;
	}
	public LeveInfo getLevelInfo(int id)
	{
		for (int i=0; i<levels.Length; i++) 
		{
			if(levels[i].id==id)
			{
				return levels[i];
			}
		}
		return null;
	}
    
}
