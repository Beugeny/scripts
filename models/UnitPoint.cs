using UnityEngine;
using System.Collections;

public class UnitPoint
{

	public int id;
	public int currentLife;//жизнь юнита
	public int team;

	private UnitInfo _info;

	public UnitInfo info()
	{
		if (_info == null) 
		{
			_info=GameContext.inst.store.getUnitInfo(id);
		}
		return _info; 
	}

	public bool isDie()
	{
		return currentLife <= 0;
	}

}
