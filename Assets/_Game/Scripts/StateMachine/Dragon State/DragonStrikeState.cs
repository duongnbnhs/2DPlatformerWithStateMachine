using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonStrikeState : IState<Dragon>
{
    float timer;
    public void OnEnter(Dragon dragon)
    {
        if (dragon.Target != null)
        {
            //doi huong enemy toi huong cua player
            dragon.ChangeDirection(dragon.Target.transform.position.x > dragon.transform.position.x);
            dragon.StopMoving();
            //dragon.ChangeAnim("strike");
            dragon.Strike();
        }

        timer = 0;
    }

    public void OnExecute(Dragon dragon)
    {
        timer += Time.deltaTime;
        if (timer >= 2f)
        {
            dragon.ChangeState(new DragonPatrolState());
        }
    }

    public void OnExit(Dragon t)
    {

    }
}
