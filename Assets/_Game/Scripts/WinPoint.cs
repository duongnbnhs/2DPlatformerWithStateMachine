using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPoint : MonoBehaviour
{
    private UIManage uiManager;
    private Animator anim;
    public int ScoreWin;
    private void Awake()
    {
        uiManager = FindObjectOfType<UIManage>();
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (collision.tag == "Player" && player.rewardCollect == ScoreWin)
        {
            anim.SetBool("activate", true);
            uiManager.GameOver();
            return;
        }else if(collision.tag == "Player" && player.rewardCollect < ScoreWin)
        {
            //ToDo: Add message UI not enough chest treasure
        }
    }
}
