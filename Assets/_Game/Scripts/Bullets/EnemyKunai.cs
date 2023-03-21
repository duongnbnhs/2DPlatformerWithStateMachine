using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKunai : Kunai
{
    [SerializeField] private float damageToPlayer;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AudioController.Ins.PlaySound(hitSound);
            collision.GetComponent<Character>().OnHit(damageToPlayer);
            Instantiate(hitVFX, transform.position, transform.rotation);
            OnDespawn();
        }
        if (collision.tag == "Rock")
        {
            Instantiate(hitVFX, transform.position, transform.rotation);
            OnDespawn();
        }
        if (collision.tag == "Shield")
        {
            Instantiate(hitVFX, transform.position, transform.rotation);
            OnDespawn();
        }
    }
}
