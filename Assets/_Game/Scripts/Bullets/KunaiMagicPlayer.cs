using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiMagicPlayer : Kunai
{
    [SerializeField] private float damageToEnemy;
    //float timer = 0;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            base.timer += Time.deltaTime;
            AudioController.Ins.PlaySound(hitSound);
            collision.GetComponent<Character>().OnHit(30f);
            Instantiate(hitVFX, transform.position, transform.rotation);

            AutoDestroy();
        }
        //if (collision.tag == "EnemyRock")
        //{
        //    Instantiate(hitVFX, transform.position, transform.rotation);
        //    OnDespawn();
        //}
    }
    private void AutoDestroy()
    {
        Invoke(nameof(OnDespawn), 3f);
    }
}
