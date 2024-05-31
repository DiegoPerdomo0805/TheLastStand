using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HuntPlayer : MonoBehaviour
{
    public Transform player;
    public Transform safeZone;
    private NavMeshAgent agent;
    private float pursuitRange = 8f;

    private float StalkingSpeed = 5f;
    private float HuntingSpeed = 10f;

    private enum State { MovingToTerritory, PursuingPlayer, Dead};
    private State CurrentState = State.MovingToTerritory;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = StalkingSpeed;
        CurrentState = State.MovingToTerritory;
        Health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentState != State.Dead)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            switch (CurrentState)
            {
                case State.MovingToTerritory:
                    MoveToTerritory();
                    if (distance <= pursuitRange)
                    {
                        //Debug.Log(" - Estamos cerca");
                        //CurrentState = State.PursuingPlayer;
                        StartPursuingPlayer();
                    }
                    break;
                
                case State.PursuingPlayer:
                    PursuePlayer();
                    if (distance > pursuitRange)
                    {
                        //Debug.Log(" - Estamos lejos");
                        //CurrentState = State.MovingToTerritory;
                        StopPursuingPlayer();
                    }
                    break;
            }
        }
    }

    void MoveToTerritory()
    {
        // Logic for moving to a designated territory or patrolling
        if (safeZone != null)
        {
            agent.SetDestination(safeZone.position);
        }
    }

    void StartPursuingPlayer()
    {
        CurrentState = State.PursuingPlayer;
        agent.speed = HuntingSpeed;
    }

    void PursuePlayer()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }

    void StopPursuingPlayer()
    {
        CurrentState = State.MovingToTerritory;
        agent.speed = StalkingSpeed;
        MoveToTerritory();
    }

    private float Health;
    public float maxHealth  = 40f;

    public void TakeDamage(float damage)
    {
        //Debug.Log(" - Vampire hit! " + damage);
        Health -= damage;
        if (Health <= 0) Die();
    }

    public void Die()
    {
        CurrentState = State.Dead;
        agent.isStopped = true;
        gameObject.tag = "Dead";
    }


}
