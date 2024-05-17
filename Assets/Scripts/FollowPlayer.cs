using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform player;
    public Vector3 Offset = new Vector3(0.0f, 5.0f, 0.0f);
    // Start is called before the first frame update
    void Start()
    {
        transform.position = player.position + Offset;
        transform.LookAt(player);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.position = player.position + Offset;
            transform.LookAt(player);
        }
        else
        {
            Debug.LogWarning("Player reference is null. Ensure the player object is assigned in the Inspector.");
        }
    }
}
