using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class UnitController : MonoBehaviour
{

    public UnitPoint unitPoint;
    private Transform moveFinalTarget;
    private Rigidbody2D rb;
    private SpriteRenderer unitRenderer;

    private UnitController _target;
    private bool readyToAttack = true;


    void Start()
    {
        unitRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine("checkTarget");
    }

    private IEnumerator checkTarget()
    {   
        while(true)
        {
            if (_target == null)
            {
                _target = findTargetUnit();
            }
            yield return new WaitForSeconds(0.5f);
        }    
    }
    /*
    Поиск вражеского юнита
    */
    private UnitController findTargetUnit()
    {
        GameObject[] all = GameObject.FindGameObjectsWithTag("Enemy");
        List<UnitController> tmpTargets = new List<UnitController>();
        foreach (GameObject tmp in all)
        {
            UnitController unit = tmp.GetComponent<UnitController>();
            if (unit && !unit.unitPoint.isDie() && unit.unitPoint.team != unitPoint.team)
            {
                tmpTargets.Add(unit);
            }
        }


        for (int i = 0; i < tmpTargets.Count; i++)
        {
            if (inMoveRange(tmpTargets[i]) == true)
            {
                return tmpTargets[i];
            }
        }
        return null;
    }

    public void init(int unitId, int team, Transform moveFinalTarget)
    {
        this.moveFinalTarget = moveFinalTarget;
        unitPoint = new UnitPoint();
        unitPoint.team = team;
        unitPoint.id = unitId;
        unitPoint.currentLife = unitPoint.info().life;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.1f);
        Gizmos.DrawSphere(transform.position, unitPoint.info().viewRadius);

        Gizmos.color = new Color(1, 0, 0, 0.1f);
        Gizmos.DrawSphere(transform.position, unitPoint.info().weapon.radius);
    }
    void Update()
    {
        if(_target && _target.unitPoint.isDie())
        {
            _target = null;
        }
        if (_target)
        {
            rb.velocity = new Vector2();
            if (inAttackRange(_target) == true)
            {
                if (readyToAttack) StartCoroutine("attack");
            }
            else
            {
                moveTo(_target.transform);
            }
        }
        else
        {
            moveTo(moveFinalTarget.transform);
        }
    }
    private void moveTo(Transform target)
    {
        if (unitPoint == null) return;
        Vector3 delta = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(delta.y, delta.x);        
        float cos = Mathf.Cos(angle);
        float sin = Mathf.Sin(angle);
        //Debug.Log("cos=" + cos+"  sin=" + sin);
        rb.velocity = new Vector2(unitPoint.info().moveSpeed * cos, unitPoint.info().moveSpeed  * sin);
    }

    private IEnumerator attack()
    {
        _target.applyDamage(unitPoint.info().weapon.force);
        readyToAttack = false;
        yield return new WaitForSeconds(unitPoint.info().weapon.reloadTime);
        readyToAttack = true;
    }

    private void applyDamage(int damage)
    {
        unitPoint.currentLife -= damage;
        unitRenderer.color = new Color(0.9f, 0.3f, 0.3f, 1f);
        CancelInvoke("resetColor");
        Invoke("resetColor", 0.4f);
        if (unitPoint.currentLife <= 0)
        {
            destroyUnit();
            EventControl.onKillUnit(unitPoint);
        }
    }

    private bool inAttackRange(UnitController _target)
    {
        float dist = Vector2.Distance(_target.transform.position, transform.position);
        return dist <= unitPoint.info().weapon.radius;
    }
    private bool inMoveRange(UnitController _target)
    {
        float dist = Vector2.Distance(_target.transform.position, transform.position);
        return dist <= unitPoint.info().viewRadius;
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        //Debug.Log("UnitController:: OnCollisionEnter2D="+coll.gameObject);
        if (coll.gameObject.transform == moveFinalTarget)
        {
            Debug.Log("UnitController:: moveFinalTarget destination complete");
            GameContext.inst.store.carier.playerInfo.lifes--;
            destroyUnit();
            EventControl.onLifeChanged();
        }
    }
    void destroyUnit()
    {
        CancelInvoke();
        unitPoint.currentLife = 0;
        Destroy(gameObject);
        StopCoroutine("checkTarget");
    }



    void OnMouseDown()
    {
        applyDamage(GameContext.inst.store.playerInfo.weaponInfo.force);
    }
    private void resetColor()
    {
        unitRenderer.color = new Color(1f, 1f, 1f, 1f);
    }
}
