using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonIdleState : IState<Dragon>
{
    float randomTime;
    float timer;
    public void OnEnter(Dragon dragon)
    {
        dragon.StopMoving();
        timer = 0;
        randomTime = Random.Range(2f, 4f);
    }

    public void OnExecute(Dragon dragon)
    {
        timer += Time.deltaTime;

        if (timer > randomTime)
        {
            dragon.ChangeState(new DragonPatrolState());
        }
    }

    public void OnExit(Dragon t)
    {

    }
}
