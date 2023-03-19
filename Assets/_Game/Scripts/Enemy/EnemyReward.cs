using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReward : MonoBehaviour
{
    public GameObject Enemy1, Enemy2, Enemy3, Enemy4, Enemy5, Enemy6, Enemy7;
    public GameObject Reward1, Reward2, Reward3, Reward4, Reward5, Reward6, Reward7;

    // Update is called once per frame
    void Update()
    {
        ActiveReward(Enemy1, Reward1);
        ActiveReward(Enemy2, Reward2);
        ActiveReward(Enemy3, Reward3);
        ActiveReward(Enemy4, Reward4);
        ActiveReward(Enemy5, Reward5);
        ActiveReward(Enemy6, Reward6);
        ActiveReward(Enemy7, Reward7);
    }
    private void ActiveReward(GameObject Enemy, GameObject Reward)
    {
        if (Enemy != null && !Enemy.activeSelf)
        {
            Reward.SetActive(true);
        }
    }
}
