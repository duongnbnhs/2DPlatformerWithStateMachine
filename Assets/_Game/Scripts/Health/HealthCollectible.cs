using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] protected HealthBar healthBar;
    [Header("Game sounds Effect: ")]
    public AudioClip getCoinSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AudioController.Ins.PlaySound(getCoinSound);
            var player = collision.GetComponent<Player>();
            player.OnHeal(healthValue);
            healthBar.SetNewHp(player.hp);
            gameObject.SetActive(false);
            
        }
    }
}
