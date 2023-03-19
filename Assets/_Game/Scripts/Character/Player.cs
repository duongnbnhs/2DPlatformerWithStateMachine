using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 5;

    [SerializeField] private float jumpForce = 350;

    [SerializeField] private Kunai kunaiPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject attackArea;

    [Header("Game sounds Effect: ")]
    public AudioClip slashSound;
    public AudioClip kunaiSound;
    public AudioClip getCoinSound;

    private bool isGrounded = true;
    private bool isJumping = false;
    private bool isAttack = false;
    private bool isDeath = false;

    private float horizontal;

    //Number of treasure chest collected
    public int rewardCollect = 0;
    //private int coin = 0;

    private Vector3 savePoint;

    private void Awake()
    {
        //coin = PlayerPrefs.GetInt("coin", 0);
        //coin = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Update");
        //Debug.LogError("Update");

        if (IsDead)
        {
            return;
        }

        isGrounded = CheckGrounded();

        //-1 -> 0 -> 1
        horizontal = Input.GetAxisRaw("Horizontal");
        //verticle = Input.GetAxisRaw("Vertical");

        if (isAttack)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (isGrounded)
        {
            if (isJumping)
            {
                return;
            }
            //ChangeAnim(StringHelper.ANIM_IDLE);
            //jump
            if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
            {
                Jump();
            }
 
            //change anim run
            if (Mathf.Abs(horizontal) > 0.1f)
            {
                ChangeAnim(StringHelper.ANIM_RUN);
            }

            //attack
            if (Input.GetKeyDown(KeyCode.C) && isGrounded)
            {
                Attack();
            }

            //throw
            if (Input.GetKeyDown(KeyCode.V) && isGrounded)
            {
                Throw();
            }
        }

        //check falling
        if (!isGrounded && rb.velocity.y < 0)
        {
            ChangeAnim(StringHelper.ANIM_FALL);
            isJumping = false;
        }


        //Moving
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            //horizontal > 0 -> tra ve 0, neu horizontal <= 0 -> tra ve la 180
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
            //transform.localScale = new Vector3(horizontal, 1, 1);
        }
        //idle
        else if (isGrounded)
        {
            ChangeAnim(StringHelper.ANIM_IDLE);
            rb.velocity = Vector2.up * rb.velocity.y;
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        isAttack = false;

        transform.position = savePoint;
        ChangeAnim(StringHelper.ANIM_IDLE);
        DeActiveAttack();

        SavePoint();
        //UIManager.instance.SetCoin(coin);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        OnInit();
    }

    protected override void OnDeath()
    {
        base.OnDeath();        
    }

    public override void OnHeal(float hpHeal)
    {
        base.OnHeal(hpHeal);
    }

    private bool CheckGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.1f, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);

        //if (hit.collider != null)
        //{
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}

        return hit.collider != null;
    }

    public virtual void Attack()
    {
        AudioController.Ins.PlaySound(slashSound);
        ChangeAnim(StringHelper.ANIM_ATTACK);
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.5f);
    }

    public void Throw()
    {
        AudioController.Ins.PlaySound(kunaiSound);
        ChangeAnim(StringHelper.ANIM_THROW);
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
        Instantiate(kunaiPrefab, throwPoint.position, throwPoint.rotation);
    }

    private void ResetAttack()
    {
        isAttack = false;
        //ChangeAnim(StringHelper.ANIM_IDLE);
        //ChangeAnim("ile");//idle ???
        anim.ResetTrigger(StringHelper.ANIM_IDLE);
        anim.SetTrigger(StringHelper.ANIM_IDLE);
    }

    public void Jump()
    {
        isJumping = true;
        ChangeAnim(StringHelper.ANIM_JUMP);
        rb.AddForce(jumpForce * Vector2.up);
    }


    internal void SavePoint()
    {
        savePoint = transform.position;
    }

    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }

    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }

    public void SetMove(float horizontal)
    {
        this.horizontal = horizontal;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            AudioController.Ins.PlaySound(getCoinSound);
            //coin++;
            //PlayerPrefs.SetInt("coin", coin);
            //UIManager.instance.SetCoin(coin);
            Destroy(collision.gameObject);
        }
        if (collision.tag == "DeathZone")
        {
            ChangeAnim(StringHelper.ANIM_DIE);
            Invoke(nameof(OnInit), 1f);
        }
    }

}
