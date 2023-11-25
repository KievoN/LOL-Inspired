using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerFacing : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if(agent.velocity.magnitude > 0f) transform.localRotation = Quaternion.LookRotation(agent.velocity, Vector3.up);
        // agent.velocity;
    }
}
