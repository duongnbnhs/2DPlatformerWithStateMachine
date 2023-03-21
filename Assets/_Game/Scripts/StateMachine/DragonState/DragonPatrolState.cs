using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonPatrolState : IState<Dragon>
{
    float randomTime;
    float timer;
    public void OnEnter(Dragon dragon)
    {
        timer = 0;
        randomTime = Random.Range(3f, 6f);
    }

    public void OnExecute(Dragon dragon)
    {
        timer += Time.deltaTime;

        if (dragon.Target != null && dragon.Target.transform.position.y - dragon.transform.position.y < 0.1f)
        //if (dragon.Target != null)
        {
                //doi huong enemy toi huong cua player
                dragon.ChangeDirection(dragon.Target.transform.position.x > dragon.transform.position.x);

                if (dragon.IsTargetInRange())
                {
                    dragon.ChangeState(new DragonAttackState());
                }
                else
                {
                    dragon.Moving();
                }
            
        }
        else
        {
            if (timer < randomTime)
            {
                dragon.Moving();
            }
            else
            {
                dragon.ChangeState(new DragonIdleState());
            }
        }
    }

    public void OnExit(Dragon dragon)
    {

    }
}
