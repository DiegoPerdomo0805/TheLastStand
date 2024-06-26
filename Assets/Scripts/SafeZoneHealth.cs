using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SafeZoneHealth : MonoBehaviour
{
    public float Health;
    public float MaxHealth = 60;
    public float Timer = 120f;
    public bool alive = true;
    public bool EndGame = false;

    // UI
    public int minutes;
    public int seconds;
    public string charSeconds;
    public int percentage;
    private float temp;
    public TMPro.TextMeshProUGUI min;
    public TMPro.TextMeshProUGUI seg;
    public TMPro.TextMeshProUGUI p;


    // Screens
    public GameObject winCanvas;
    public GameObject loseCanvas;



    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;      
        if (winCanvas != null) winCanvas.SetActive(false);
        if (loseCanvas != null) loseCanvas.SetActive(false);
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
            TakeDamage(MaxHealth / 50);
        }
    }

        void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(MaxHealth / 100 * Time.deltaTime); // Damage over time while in contact
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
                minutes = (int)Timer/60;
                seconds = (int) Timer % 60;
                charSeconds = seconds > 9? seconds.ToString(): "0" + seconds.ToString();
                min.text = minutes.ToString();
                seg.text = charSeconds;       
                temp = Health/MaxHealth;
                temp = temp * 100;
                percentage = (int) temp;
                p.text = percentage.ToString();
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
        if (winCanvas != null) winCanvas.SetActive(true);
    }

    void YouLose()
    {
        Debug.Log(" - You Lose! ");
        if (loseCanvas != null) loseCanvas.SetActive(true);

    }
}
