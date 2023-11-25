using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private NavMeshAgent agent;

    void Update()
    {
        anim.SetFloat("MotionSpeed", agent.speed / 2f);
        anim.SetFloat("Speed", agent.velocity.magnitude);
    }
}
