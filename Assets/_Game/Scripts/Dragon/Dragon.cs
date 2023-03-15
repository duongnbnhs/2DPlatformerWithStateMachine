using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Character
{
    [SerializeField] private float strikeRange;
    [SerializeField] private float blastRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Kunai kunaiPrefab;
    [SerializeField] private Transform throwPoint;
    //[SerializeField] private GameObject strikeArea;

    private IState<Dragon> currentState;

    private bool isRight = true;
    public Character target;
    private bool isAttack = false;
    public Character Target => target;

    public bool isHurt = false;

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
            ChangeState(new DragonStrikeState());
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
    public void Strike()
    {
        rb.AddForce(transform.forward * 10f, ForceMode2D.Impulse);
        //change anim strike
        anim.ResetTrigger("strike");
        anim.SetTrigger("strike");        

    }
    public void Blast()
    {
        ChangeAnim(StringHelper.ANIM_DRAGON_BLAST);
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
        Instantiate(kunaiPrefab, throwPoint.position, throwPoint.rotation);
    }
    private void ResetAttack()
    {
        isAttack = false;
        anim.ResetTrigger(StringHelper.ANIM_IDLE);
        anim.SetTrigger(StringHelper.ANIM_IDLE);
    }
    public bool IsTargetInRange()
    {
        if ((target != null && Vector2.Distance(target.transform.position, transform.position) <= strikeRange ))
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
    
    float timer = 0f;
    public void Hurt()
    {
        timer+=Time.deltaTime;
        
        ChangeAnim(StringHelper.ANIM_DRAGON_HURT);
        if (timer > 1f)
        {
            timer = 0f;
            ChangeState(new DragonPatrolState());
        }
    }
}
