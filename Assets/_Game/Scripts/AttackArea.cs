using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] public float damageTaken;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Enemy")
        {
            collision.GetComponent<Character>().OnHit(damageTaken);
        }
    }
    public void damageInUplevel(int level)
    {
        damageTaken += level * 15;
    }
}
