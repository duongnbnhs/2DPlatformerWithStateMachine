﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPoint : MonoBehaviour
{
    private UIManage uiManager;
    private Animator anim;
    public int ScoreWin;
    [SerializeField] protected CombatText CombatTextPrefab;

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
            uiManager.GameOver("Victory!!!");
            return;
        }else if(collision.tag == "Player" && player.rewardCollect < ScoreWin)
        {
            //ToDo: Add message UI not enough chest treasure
            Instantiate(CombatTextPrefab, transform.position + Vector3.up, Quaternion.identity).OnInitAnnounce("Collect more chests to become the winner!");
        }
    }
}
