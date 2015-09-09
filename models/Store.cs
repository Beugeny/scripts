using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Store {
	public Carier carier;
	public List<LeveInfo> levels=new List<LeveInfo>();
	public PlayerInfo playerInfo;
    public List<UnitInfo> units = new List<UnitInfo>();


	public Store()
	{
		carier = new Carier ();

		//<<TEST DATA
        
		WeaponInfo weapon1 = new WeaponInfo ();
		weapon1.force = 3;
		weapon1.radius =1f;
		weapon1.reloadTime = 1f;

        WeaponInfo weapon2 = new WeaponInfo();
        weapon2.force = 2;
        weapon2.radius = 1f;
        weapon2.reloadTime = 4f;

        UnitInfo unit1 = new UnitInfo ();
		unit1.life = 10;
		unit1.prefabName = "SimpleUnit";
		unit1.id = 0;
		unit1.armor = 1;
		unit1.moveSpeed = 2f;
		unit1.weapon = weapon1;
		unit1.killCash = 1;
		unit1.killScore = 2;
        unit1.viewRadius = 4;

        UnitInfo unit2 = new UnitInfo();
        unit2.life = 5;
        unit2.prefabName = "FriendlyUnit";
        unit2.id = 1;
        unit2.armor = 1;
        unit2.moveSpeed = 1.1f;
        unit2.weapon = weapon2;
        unit2.killCash = 1;
        unit2.killScore = 2;
        unit2.viewRadius = 4;

        units.Add(unit1);
        units.Add(unit2);

        //SPAWNER
        SpawnerInfo spawner = new SpawnerInfo ();
		spawner.waves=new SpawnWave[1];
		
		SpawnWave wave1 = new SpawnWave ();
		wave1.time = 0f;
		wave1.units=new SpawnUnit[1];

		SpawnUnit spawnUnit = new SpawnUnit ();
		spawnUnit.unitId = 0;
		spawnUnit.time = 1f;
		spawnUnit.count = 5;

		/*SpawnUnit spawnUnit2 = new SpawnUnit ();
		spawnUnit2.unitId = 0;
		spawnUnit2.time = 3;
		spawnUnit2.count = 1;*/

		wave1.units.SetValue (spawnUnit,0);
		//wave1.units.SetValue (spawnUnit,1);

		spawner.waves.SetValue (wave1, 0);
        //spawner.waves.SetValue (wave1, 1);

        //LEVELS
		
		LeveInfo levelInfo=new LeveInfo();
		levelInfo.id=1;
		levelInfo.info="TestLevel1";
		levelInfo.spawn = spawner;
		levels.Add(levelInfo);
		
		levelInfo=new LeveInfo();
		levelInfo.id=2;
		levelInfo.info="TestLevel2";
		levelInfo.spawn = spawner;
        levels.Add(levelInfo);

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
        teamSpawner.unitId = 1;
        teamSpawner.time = 0.5f;
        playerInfo.spawner.spawners.SetValue(teamSpawner, 0);


        playerInfo.weaponInfo = new PlayerWeaponInfo();
        playerInfo.weaponInfo.force = 4;

        carier.playerInfo = playerInfo;

		//TEST DATA>>

	}



	public UnitInfo getUnitInfo(int id)
	{
		for (int i=0; i<units.Count; i++) 
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
		for (int i=0; i<levels.Count; i++) 
		{
			if(levels[i].id==id)
			{
				return levels[i];
			}
		}
		return null;
	}
    
}
