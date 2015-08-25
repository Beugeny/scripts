using UnityEngine;
using System.Collections;
using System;

public class GameContext : MonoBehaviour {  

  	public Store store;
	public GameObject[] prefabs;


	private static GameContext _inst;
	void Awake () 
	{
		if (_inst == null) 
		{
			_inst = this;
			DontDestroyOnLoad(this);
			init();
		}
		else if(this!=inst)
		{
			Destroy (this.gameObject);
		}
			
	}
	public static GameContext inst
	{
		get
		{
			if(_inst == null)
			{
				_inst = GameObject.FindObjectOfType<GameContext>();				
				DontDestroyOnLoad(_inst.gameObject);
			}
			
			return _inst;
		}
	}

	public void init()
	{
		//TEST
		store=new Store();
	}
	public GameObject getPrefabByName(String name)
	{
		for (int i=0; i<prefabs.Length; i++) 
		{
			if(prefabs[i].name==name)
			{
				return prefabs[i];
			}
		}
		return null;
	}
}
