using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Character
{
    [SerializeField] protected float attackRange;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected Rigidbody2D rb;
    protected bool isRight = true;

    public Character target;
    public Character Target => target;
    protected IState<Dragon> currentState;
    // Start is called before the first frame update
    void Start()
    {
        base.OnInit();
        //DeActiveAttack();
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentState != null && !IsDead)
        {
            currentState.OnExecute(this);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Trampling();
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
        //ChangeState(null);
        base.OnDeath();
        gameObject.SetActive(false);
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
    public void Strike()
    {
        ChangeAnim("strike");
        Vector2 targetPosition = target.transform.position;
        transform.DOMove(targetPosition, 1.25f).OnComplete(() => { ChangeAnim("idle"); });
    }
    public void Trampling()
    {
        
        ChangeAnim("jump");
        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = target.transform.position;
        Vector2 flyPosition = new Vector2(currentPosition.x, currentPosition.y + 2.5f);
        Vector2 crouchPosition = new Vector2(targetPosition.x, currentPosition.y + 2.5f);
        transform.DOMove(flyPosition, 0.6f).OnComplete(() =>
        {
            ChangeAnim("fly");
            transform.DOMove(crouchPosition, 1.2f).OnComplete(() =>
            {
                ChangeAnim("crouch");
            });
        });


    }
}
