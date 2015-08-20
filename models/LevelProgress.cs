using UnityEngine;
using System.Collections;
using System;

public class LevelProgress
{
	public int id;
	public int state;

	private LeveInfo _levelInfo;

	public LeveInfo levelInfo {
		get {
			if(_levelInfo==null)
			{
				_levelInfo=GameContext.inst.store.getLevelInfo(id);
			}
			return _levelInfo;
		}
	}
}


