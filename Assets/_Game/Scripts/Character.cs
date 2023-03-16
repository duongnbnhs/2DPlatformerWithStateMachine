using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected Animator anim;
    [SerializeField] protected HealthBar healthBar;

    [SerializeField] protected CombatText CombatTextPrefab;


    private float hp;
    private string currentAnimName;

    public bool IsDead => hp <= 0;

    private void Start()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        hp = 100;
        healthBar.OnInit(100, transform);
    }

    public virtual void OnDespawn()
    {

    }

    protected virtual void OnDeath()
    {
        ChangeAnim(StringHelper.ANIM_DIE);
        Invoke(nameof(OnDespawn), 2f);
    }

    internal void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    public void OnHit(float damage)
    {
        if (!IsDead)
        {
            hp -= damage;

            if (IsDead)
            {
                hp = 0;
                OnDeath();
            }

            healthBar.SetNewHp(hp);
            Instantiate(CombatTextPrefab, transform.position + Vector3.up, Quaternion.identity).OnInit(damage);
        }
    }

}
