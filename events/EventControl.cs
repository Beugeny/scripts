using UnityEngine;
using System.Collections;

public class EventControl {

	public delegate void UnitEvent(UnitPoint unitInfo);
	public delegate void SimpleEvent();

	public static event UnitEvent killUnit;
	public static event SimpleEvent scoresChanged;
	public static event SimpleEvent cashChanged;
	public static event SimpleEvent lifeChanged;
	public static event SimpleEvent levelComplete;
	public static event SimpleEvent levelFail;

	public static void onlevelFail()
	{
		if (levelFail != null) 
		{
			levelFail ();
		}
	}
	public static void onLevelComplete()
	{
		if (levelComplete != null) 
		{
			levelComplete ();
		}
	}
	public static void onKillUnit(UnitPoint info)
	{
		if (EventControl.killUnit != null) 
		{
			EventControl.killUnit (info);
		}
	}
	public static void onScoresChanged()
	{
		if (scoresChanged != null) 
		{
			scoresChanged ();
		}
	}
	public static void onCashChanged()
	{
		if (cashChanged != null) 
		{
			cashChanged ();
		}
	}
	public static void onLifeChanged()
	{
		if (lifeChanged != null) 
		{
			lifeChanged ();
		}
	}

}
