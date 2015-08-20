
using System;

public class PlayerInfo
{
	public PlayerSpawnerInfo spawner;

	public PlayerWeaponInfo weaponInfo;
	public int scores;
	public int cash;
	public int lifes;

	public PlayerInfo ()
	{
		spawner = new PlayerSpawnerInfo ();
	}
}


