using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpLevelCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] private AudioClip CollectSound;
    [SerializeField] private HealthBar healthBar;
    private int levelBonus = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            var player = collision.GetComponent<Player>();
            player.UpLevel(levelBonus);
            player.OnHeal(healthValue);
            healthBar.SetNewHp(player.hp);
            gameObject.SetActive(false);
            AudioController.Ins.PlaySound(CollectSound);
        }
    }
}
