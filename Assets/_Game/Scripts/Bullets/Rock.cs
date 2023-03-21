using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public GameObject hitVFX;
    public Rigidbody2D rb;
    public AudioClip hitSound;
    [SerializeField]private float timeRockExist;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        //rb.velocity = transform.right * 5f;
        Invoke(nameof(OnDespawn), timeRockExist);
    }

    public void OnDespawn()
    {
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag == "Enemy")
        //{
        //    AudioController.Ins.PlaySound(hitSound);
        //    collision.GetComponent<Character>().OnHit(30f);
        //    Instantiate(hitVFX, transform.position, transform.rotation);
        //    OnDespawn();
        //}
    }
}
