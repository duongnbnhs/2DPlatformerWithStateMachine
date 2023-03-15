using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSight : MonoBehaviour
{
    public Dragon dragon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            dragon.SetTarget(collision.GetComponent<Character>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            dragon.SetTarget(null);
        }
    }
}
