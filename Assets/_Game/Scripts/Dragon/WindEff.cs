using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEff : MonoBehaviour
{
    [SerializeField] protected Animator anim;
    private string currentAnimName;
    float timer;
    private void Start()
    {
        Invoke(nameof(OnDespawn), 2f);
    }
    public void OnDespawn()
    {
        Destroy(gameObject);
    }
    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            //Debug.Log("new:" + animName);
            //Debug.Log("old:"+currentAnimName);
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
}
