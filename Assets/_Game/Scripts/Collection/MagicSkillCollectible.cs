using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSkillCollectible : MonoBehaviour
{
    [SerializeField] private AudioClip CollectSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            var player = collision.GetComponent<Player>();
            player.OnMagicUnlock(true);
            gameObject.SetActive(false);
            AudioController.Ins.PlaySound(CollectSound);
        }
    }
}
