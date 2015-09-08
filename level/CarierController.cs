using UnityEngine;
using System.Collections;

public class CarierController : MonoBehaviour {

	private Carier carier;
	void Start () 
	{		
		EventControl.killUnit += onKillUnit;
		carier = GameContext.inst.store.carier;
	}
	
	
	void onKillUnit (UnitPoint unitInfo)
	{
        if(unitInfo.team== Teams.BAD)
        {
            changeCash(unitInfo.info().killCash);
            changeScore(unitInfo.info().killScore);
        }		
	}

	void changeCash(int changeValue)
	{
		carier.playerInfo.cash += changeValue;
		EventControl.onCashChanged ();
	}
	void changeScore(int changeValue)
	{
		carier.playerInfo.scores += changeValue;
		EventControl.onScoresChanged ();
	}

}
