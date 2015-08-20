using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public Text txt_level_complete;  
	public Text scoreText;
	public Text cashText;
	public Text leftLifesText;



	void Start () {
		txt_level_complete.enabled = false;
		EventControl.cashChanged += onCashChanged;
		EventControl.scoresChanged += onScoreChanged;
		EventControl.lifeChanged += onLifeChanged;
		EventControl.levelComplete += onLevelComplete;
		EventControl.levelFail += onLevelFail;

		onScoreChanged ();
		onCashChanged ();
		onLifeChanged ();
	}

	void onScoreChanged ()
	{
		scoreText.text = "Scores: "+GameContext.inst.store.carier.playerInfo.scores;

	}

	void onCashChanged ()
	{
		cashText.text = "Cash: " + GameContext.inst.store.carier.playerInfo.cash;
	}

	void onLifeChanged ()
	{
		int lifes = GameContext.inst.store.carier.playerInfo.lifes;
		if (lifes >= 0)
		{
			leftLifesText.text = "Left Lifes: "+lifes;
		}
		else 
		{
			leftLifesText.text = "Left Lifes: 0";
		}		
	}
	void onLevelComplete()
	{
		txt_level_complete.enabled = true;
		txt_level_complete.text="LEVEL COMPLETE";
	}
	void onLevelFail()
	{
		txt_level_complete.enabled = true;
		txt_level_complete.text="LEVEL FAIL";
	}
}
