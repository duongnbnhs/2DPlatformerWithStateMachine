using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKunai : Kunai
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AudioController.Ins.PlaySound(hitSound);
            collision.GetComponent<Character>().OnHit(5f);
            Instantiate(hitVFX, transform.position, transform.rotation);
            OnDespawn();
        }
    }
}
