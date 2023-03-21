using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Character
{
    [SerializeField] protected float tramplingRange;
    [SerializeField] protected float blastRange;
    [SerializeField] protected float earthquakeRange;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] GameObject earthquakeHitBox;
    [SerializeField] GameObject strikeHitBox;
    [SerializeField] GameObject tramplingHitBox;
    [SerializeField] GameObject fireball;
    [SerializeField] GameObject windLeft;
    [SerializeField] GameObject rightW;
    [SerializeField] private Transform throwPoint;
    protected bool isRight = true;
    [HideInInspector] public float maxHP;
    public Character target;
    public Character Target => target;
    protected IState<Dragon> currentState;

    // Update is called once per frame
    private void Update()
    {
        if (currentState != null && !IsDead)
        {
            currentState.OnExecute(this);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Earthquake();
        }
    }
    public override void OnInit()
    {
        base.OnInit();
        //Debug.Log("Dragon init");
        maxHP = hp;
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
    public bool CheckHaflHP()
    {
        if (maxHP / hp >= 2) return true;
        else return false;
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
    public virtual bool IsTargetInRange()
    {
        if (Vector2.Distance(target.transform.position, transform.position) <= 10f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void SetTarget(Character character)
    {
        this.target = character;

        if (IsTargetInRange())
        {
            ChangeState(new DragonAttackState());
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
        ChangeAnim("walk");
        rb.velocity = transform.right * moveSpeed;
    }
    public void StopMoving()
    {
        ChangeAnim("idle");
        rb.velocity = Vector2.zero;
    }
    public virtual void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;
        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }
    public void ChooseSkillByRange()
    {
        float distance = Vector2.Distance(target.transform.position, transform.position);
        if (distance > tramplingRange && IsTargetInRange())
        {
            Trampling();
        }
        if (distance <= tramplingRange && distance > blastRange)
        {
            Blast();
        }
        if (distance <= blastRange && distance > earthquakeRange)
        {
            Earthquake();
        }
        if (distance < earthquakeRange)
        {
            int ran = Random.Range(1, 3);
            if (ran == 1) Strike();
            else Kick();
        }
    }
    public void RandomSkill()
    {
        int ran = Random.Range(1, 6);
        switch (ran)
        {
            case 1: Strike(); break;
            case 2: Earthquake(); break;
            case 3: Kick(); break;
            case 4: Blast(); break;
            case 5: Trampling(); break;
        }
    }
    public void Strike()
    {
        ChangeAnim("strike");
        Vector2 targetPosition = target.transform.position;
        transform.DOMove(targetPosition, 1.25f).OnComplete(() => { ChangeAnim("idle"); EnableHitBox(strikeHitBox); });
        Invoke(nameof(DisableHitBox), 0.25f);
    }
    public void Trampling()
    {
        ChangeAnim("jump");
        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = target.transform.position;
        Vector2 flyPosition = new Vector2(currentPosition.x, currentPosition.y + 2.5f);
        Vector2 crouchPosition = new Vector2(targetPosition.x, currentPosition.y + 2.5f);
        transform.DOMove(flyPosition, 0.3f).OnComplete(() =>
        {
            ChangeAnim("fly");
            transform.DOMove(crouchPosition, 1f).OnComplete(() =>
            {
                ChangeAnim("crouch");
                EnableHitBox(tramplingHitBox);
            });
        });
        ChangeAnim("idle");
        Invoke(nameof(DisableHitBox), 0.25f);
    }
    public void Blast()
    {
        ChangeAnim("attack");
        Instantiate(fireball, throwPoint.position, throwPoint.rotation);
        ChangeAnim("idle");
    }
    public void Kick()
    {
        ChangeAnim("kick");
        Vector2 targetPosition = target.transform.position;
        transform.DOMove(targetPosition, 1.25f).OnComplete(() => { ChangeAnim("idle"); EnableHitBox(strikeHitBox); });

        Invoke(nameof(DisableHitBox), 0.25f);
    }
    public void Earthquake()
    {
        ChangeAnim("jump");
        Vector2 currentPosition = transform.position;
        Vector2 flyPosition = new Vector2(currentPosition.x, currentPosition.y + 2.5f);
        transform.DOMove(flyPosition, 0.3f).OnComplete(() =>
        {
            ChangeAnim("crouch");
            EnableHitBox(earthquakeHitBox);

        });
        WindEffect(currentPosition);

        ChangeAnim("idle");
        Invoke(nameof(DisableHitBox), 0.25f);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if ((Target is null || target is null) && collision.tag == "EnemyWall")
        {
            ChangeDirection(!isRight);
        }
    }
    public void WindEffect(Vector2 pos)
    {
        Vector2 leftPos = new Vector2(pos.x - 1f, -2.7f);
        Vector2 rightPos = new Vector2(pos.x + 1f, -2.7f);
        WindEff left = Instantiate(windLeft).GetComponent<WindEff>();
        WindEff right = Instantiate(rightW).GetComponent<WindEff>();

        left.transform.position = leftPos;
        right.transform.position = rightPos;
    }
    protected void EnableHitBox(GameObject go)
    {
        go.SetActive(true);
    }

    protected void DisableHitBox()
    {
        earthquakeHitBox.SetActive(false);
        strikeHitBox.SetActive(false);
        tramplingHitBox.SetActive(false);
    }
}
