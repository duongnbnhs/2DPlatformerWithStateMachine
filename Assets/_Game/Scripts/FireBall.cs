using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject explo;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();

    }
    public void OnInit()
    {
        rb.velocity = transform.right * 5f;
        Invoke(nameof(OnDespawn), 2.75f);
    }

    public void OnDespawn()
    {
        GameObject go = Instantiate(explo, transform.position, transform.rotation);        
        
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player")
        {            
            collision.GetComponent<Character>().OnHit(35f);
            
            OnDespawn();            
        }
        if (collision.tag == "DizzyArea")
        {         
           
            OnDespawn();            
        }
    }
}
