using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMagicKunai : Kunai
{
    [SerializeField] private float damageToPlayer;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AudioController.Ins.PlaySound(hitSound);
            collision.GetComponent<Character>().OnHit(damageToPlayer);
            Instantiate(hitVFX, transform.position, transform.rotation);
            AutoDestroy();
        }
    }
    private void AutoDestroy()
    {
        Invoke(nameof(OnDespawn), 1f);
    }
}
