using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneHealth : MonoBehaviour
{
    public float Health;

    // Start is called before the first frame update
    void Start()
    {
        Health = 60;        
    }
    void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health < 0)
        {
            Health = 0;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(4.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
