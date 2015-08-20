using UnityEngine;
using System.Collections;
using System;

public class LeveInfo
{
	public int id;
	public String info;
	public SpawnerInfo spawn;//спавнер для текущего уровня


	public LeveInfo ()
	{

	}



	public void printLevel()
	{
		Debug.Log ("LevelInfo id="+id+" info="+info);
	}
}


