using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitVFX : MonoBehaviour
{
    float timer;
    void Awake()
    {
        timer = 0;
        timer+=Time.deltaTime;
        if(timer > 0.35f)
        {
            Destroy(this.gameObject);
        }
    }

    
}
