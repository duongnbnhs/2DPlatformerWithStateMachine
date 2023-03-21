using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameObject explosion;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        rb.velocity = transform.right * 5f;
        Invoke(nameof(OnDespawn), 4f);
    }

    public void OnDespawn()
    {
        // khoi tao animation, xoa luon animation
        Destroy(Instantiate(explosion, transform.position, transform.rotation));
        Destroy(gameObject);
    }
    protected float timer = 0;
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //AudioController.Ins.PlaySound(hitSound);
            collision.GetComponent<Character>().OnHit(50f);
            OnDespawn();
        }
    }
}
