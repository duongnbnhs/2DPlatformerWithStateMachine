using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(OnDespawn), 0.5f);
    }

    public void OnDespawn()
    {
        Destroy(gameObject);
    }
}
