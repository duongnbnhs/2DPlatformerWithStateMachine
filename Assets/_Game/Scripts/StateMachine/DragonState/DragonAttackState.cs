using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAttackState : IState<Dragon>
{
    float timer;
    public void OnEnter(Dragon dragon)
    {
        Debug.Log("DRA atk");
        //doi huong enemy toi huong cua player
        dragon.ChangeDirection(dragon.Target.transform.position.x > dragon.transform.position.x);

        dragon.StopMoving();
        if (dragon.CheckHaflHP())
        {
            dragon.ChooseSkillByRange();
        }
        else
        {
            dragon.RandomSkill();
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

    public void OnExit(Dragon dragon)
    {
        
    }
}
