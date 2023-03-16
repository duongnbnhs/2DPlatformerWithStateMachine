using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPoint : MonoBehaviour
{
    private UIManage uiManager;
    private Animator anim;
    private void Awake()
    {
        uiManager = FindObjectOfType<UIManage>();
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            anim.SetBool("activate", true);
            uiManager.GameOver();
            return;
        }
    }
}
