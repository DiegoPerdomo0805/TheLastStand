using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HuntPlayer : MonoBehaviour
{
    public Transform player;
    public Transform safeZone;
    private NavMeshAgent agent;
    private float pursuitRange = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= pursuitRange)
        {
            Debug.Log(" - Estamos cerca");
        }
    }
}
