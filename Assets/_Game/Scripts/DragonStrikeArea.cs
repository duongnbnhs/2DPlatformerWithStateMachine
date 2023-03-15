using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonStrikeArea : MonoBehaviour
{
    [SerializeField] Dragon dragon;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Character>().OnHit(50f);
            Debug.Log("HIT");
        }
        if (collision.CompareTag("DizzyArea"))
        {
            dragon.Hurt();
        }
    }
}
