using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    public GameObject hitVFX;
    public Rigidbody2D rb;
    public AudioClip hitSound;
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
        Destroy(gameObject);
    }
    protected float timer = 0;
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            timer += Time.deltaTime;
            AudioController.Ins.PlaySound(hitSound);
            collision.GetComponent<Character>().OnHit(30f);
            Instantiate(hitVFX, transform.position, transform.rotation);

            OnDespawn();
        }
        if (collision.tag == "EnemyRock")
        {
            Instantiate(hitVFX, transform.position, transform.rotation);
            OnDespawn();
        }
    }
}
