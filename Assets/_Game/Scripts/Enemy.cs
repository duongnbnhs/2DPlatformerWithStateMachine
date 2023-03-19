using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] protected float attackRange;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected float timeLoopAttack;

    [SerializeField] protected GameObject attackArea;

    [Header("Game sounds Effect: ")]
    public AudioClip slashEnemySound;

    protected IState currentState;

    protected bool isRight = true;

    protected Character target;
    public Character Target => target;


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
        gameObject.SetActive(false);
    }


    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public void SetTarget(Character character)
    {
        this.target = character;

        if (IsTargetInRange())
        {
            ChangeState(new AttackState());
        }
        else
        if (Target != null)
        {
            ChangeState(new PatrolState());
        }
        else
        {
            ChangeState(new IdleState());
        }
    }

    public void Moving()
    {
        ChangeAnim(StringHelper.ANIM_RUN);
        rb.velocity = transform.right * moveSpeed;
    }

    public void StopMoving()
    {
        ChangeAnim(StringHelper.ANIM_IDLE);
        rb.velocity = Vector2.zero;
    }

    public virtual void Attack()
    {
        ChangeAnim(StringHelper.ANIM_ATTACK);
        ActiveAttack();
        AudioController.Ins.PlaySound(slashEnemySound);
        Invoke(nameof(DeActiveAttack), timeLoopAttack);
    }

    public virtual bool IsTargetInRange()
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

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyWall")
        {
            ChangeDirection(!isRight);
        }
    }

    public virtual void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;
        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up  * 180);
    }

    protected virtual void ActiveAttack()
    {
        attackArea.SetActive(true);
    }

    protected virtual void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }

}
