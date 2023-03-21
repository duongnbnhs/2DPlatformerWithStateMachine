using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TramplingHitBox : MonoBehaviour
{
    [SerializeField] float dmgDealed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Character>().OnHit(dmgDealed);
        }
    }
}
