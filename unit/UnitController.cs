using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour {

	public UnitPoint unitPoint;
	private Transform moveFinalTarget;
	private Rigidbody2D rb;
	private SpriteRenderer unitRenderer;


	void Start () {	
		unitRenderer = GetComponent<SpriteRenderer> ();
		moveFinalTarget = GameContext.inst.teamMoveTarget[unitPoint.team];
		rb = GetComponent<Rigidbody2D> ();
	}

	public void init(int unitId,int team)
	{
		unitPoint = new UnitPoint ();
		unitPoint.id = unitId;
		unitPoint.currentLife = unitPoint.info().life;
	}

	void Update () {
		if (unitPoint == null)return;
		Vector3 delta = (moveFinalTarget.transform.position - transform.position);
		float direction = Mathf.Abs (delta.x) / delta.x;
		rb.velocity = new Vector2 (unitPoint.info().moveSpeed*direction, 0);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		//Debug.Log("UnitController:: OnCollisionEnter2D="+coll.gameObject);
        if (coll.gameObject.tag == "Finish") 
		{
			Debug.Log("UnitController:: moveFinalTarget destination complete");
			GameContext.inst.store.carier.playerInfo.lifes--;
			destroyUnit();
			EventControl.onLifeChanged ();
		}		
	}
	void destroyUnit()
	{
		CancelInvoke ();
		unitPoint.currentLife = 0;
		Destroy(gameObject);
	}



	void OnMouseDown()
	{
		unitPoint.currentLife -= 1;
		unitRenderer.color = new Color (0.9f, 0.3f,0.3f,1f);
		CancelInvoke ("resetColor");
		Invoke ("resetColor",0.4f);
		if (unitPoint.currentLife <= 0) 
		{
			destroyUnit();
			EventControl.onKillUnit (unitPoint);
		}
	}
	private void resetColor ()
	{
		unitRenderer.color=new Color (1f, 1f,1f,1f);
	}
}
