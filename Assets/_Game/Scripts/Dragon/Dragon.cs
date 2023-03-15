using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Character
{
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;

    //[SerializeField] private GameObject attackArea;

    private IState<Dragon> currentState;

    public bool isRight = true;

    private Character target;
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

        ChangeState(new DragonIdleState());
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


    public void ChangeState(IState<Dragon> newState)
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

    internal void SetTarget(Character character)
    {
        this.target = character;

        if (IsTargetInRange())
        {
            ChangeState(new DragonBlastState());
        }
        else
        if (Target != null)
        {
            ChangeState(new DragonPatrolState());
        }
        else
        {
            ChangeState(new DragonIdleState());
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

    public void Blast()
    {
        /*ChangeAnim(StringHelper.ANIM_ATTACK);
        ActiveAttack();
        AudioController.Ins.PlaySound(slashEnemySound);
        Invoke(nameof(DeActiveAttack), 0.5f);*/
    }

    public bool IsTargetInRange()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyWall")
        {
            ChangeDirection(!isRight);
        }
    }

    public void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;
        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }
}
