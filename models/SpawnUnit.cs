
using System;

public class SpawnUnit
{
	public int unitId;//ид юнита
	public int count;//количество одновременно заспауненых юнитов
	public float time;//Время спауна юнита от начала волны




	private UnitInfo _unitInfo;
	
	public UnitInfo unitInfo {
		get {
			if(_unitInfo==null || _unitInfo.id!=unitId)
			{
				_unitInfo=GameContext.inst.store.getUnitInfo(unitId);
			}
			return _unitInfo;
		}
	}


}


