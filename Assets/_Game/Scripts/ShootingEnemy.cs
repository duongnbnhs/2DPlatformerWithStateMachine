using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    [SerializeField] private Transform throwPoint;
    [SerializeField] private EnemyKunai ekunaiPrefab;

    private void Update()
    {
        if (currentState != null && !IsDead)
        {
            currentState.OnExecute(this);
        }
    }

    public override void OnInit()
    {
        base.OnInit();

        ChangeState(new IdleState());
        DeActiveAttack();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(healthBar.gameObject);
        Destroy(gameObject);
    }

    protected override void OnDeath()
    {
        ChangeState(null);
        base.OnDeath();
    }



    public override void Attack()
    {
        ChangeAnim(StringHelper.ANIM_ATTACK);
        ActiveAttack();
        AudioController.Ins.PlaySound(slashEnemySound);
        Invoke(nameof(DeActiveAttack), 0.5f);
    }

    public override bool IsTargetInRange()
    {
        if (target != null && Vector2.Distance(target.transform.position, transform.position) <= attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public override void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;
        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }
    protected override void ActiveAttack()
    {
        //Debug.Log("Attack");
        ChangeAnim(StringHelper.ANIM_ATTACK);
        Instantiate(ekunaiPrefab, throwPoint.position, throwPoint.rotation);
    }

    protected override void DeActiveAttack()
    {
        //attackArea.SetActive(false);
    }

}
