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
        if(Enemy1!=null&& Reward1!=null)
            ActiveReward(Enemy1, Reward1);
        if (Enemy2 != null && Reward2 != null)
            ActiveReward(Enemy2, Reward2);
        if (Enemy3 != null && Reward3 != null)
            ActiveReward(Enemy3, Reward3);
        if (Enemy4 != null && Reward4 != null)
            ActiveReward(Enemy4, Reward4);
        if (Enemy5 != null && Reward5 != null)
            ActiveReward(Enemy5, Reward5);
        if (Enemy6 != null && Reward6 != null)
            ActiveReward(Enemy6, Reward6);
        if (Enemy7 != null && Reward7 != null)
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
