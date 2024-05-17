using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float Speed = 15f;
    public float Speed2 = 30f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        
        if(Input.GetKey(KeyCode.Space)){
            Debug.Log(" Running");
            rb.AddForce(movement * Speed2);
        }
        else{
            rb.AddForce(movement * Speed);
        }    
    }
}
