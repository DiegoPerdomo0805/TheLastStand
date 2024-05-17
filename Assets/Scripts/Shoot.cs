using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bullet;
    public float speed = 20f;
    public float CoolDown = 0.5f;
    public float duration;

    void Start()
    {
        duration -= Time.deltaTime;

        if(Input.GetButtonDown("Fire1") && duration <= 0f){
            Fire();
            duration = CoolDown;
        }
    }

    void Fire()
    {
        GameObject b = Instantiate(bullet, transform.position, transform.rotation);

        Rigidbody rb = b.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
