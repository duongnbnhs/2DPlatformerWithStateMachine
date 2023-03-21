using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] private AudioClip CollectSound;
    [SerializeField] private HealthBar healthBar;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            var player = collision.GetComponent<Player>();
            player.OnHeal(healthValue);
            healthBar.SetNewHp(player.hp);
            gameObject.SetActive(false);
            AudioController.Ins.PlaySound(CollectSound);
            //Up level then delete
            Destroy(gameObject);
        }
    }
}
