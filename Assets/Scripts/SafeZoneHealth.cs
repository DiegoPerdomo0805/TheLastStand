using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneHealth : MonoBehaviour
{
    public float Health;
    public float MaxHealth = 60;
    public float Timer = 120f;
    public bool alive = true;
    public bool EndGame = false;

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;      
    }
    void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            alive = false;
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
        if (!EndGame)
        {
            if (!alive) EndGame = true;
            else
            {
                Timer -= Time.deltaTime;
                if (Timer <= 0)
                {
                    Timer = 0;
                    EndGame = true;
                }
            }
        }
        else
        {
            if(alive) YouWin();
            else YouLose();
        }
    }

    void YouWin()
    {
        Debug.Log(" - You win!");
    }

    void YouLose()
    {
        Debug.Log(" - You Lose! ");
    }
}
