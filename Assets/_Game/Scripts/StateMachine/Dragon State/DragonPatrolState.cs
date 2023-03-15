using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonPatrolState : IState<Dragon>
{
    float randomTime;
    float timer;
    public void OnEnter(Dragon t)
    {
        timer = 0;
        randomTime = Random.Range(3f, 6f);
    }

    public void OnExecute(Dragon dragon)
    {
        timer += Time.deltaTime;

        if (dragon.Target != null)
        {
            //doi huong enemy toi huong cua player
            dragon.ChangeDirection(dragon.Target.transform.position.x > dragon.transform.position.x);

            if (dragon.IsTargetInRange())
            {
                dragon.ChangeState(new DragonStrikeState());
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

    public void OnExit(Dragon t)
    {
        
    }
}
