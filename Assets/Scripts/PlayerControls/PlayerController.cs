using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent Agent;

    [SerializeField] private LayerMask _groundMask;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    private bool _moveHeld = false;

    private const float _holdMoveDelay = 0.2f;

    private Coroutine _holdMoveRoutine = null;

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) _holdMoveRoutine = StartCoroutine(HoldMove());
        if (ctx.canceled) StopCoroutine(_holdMoveRoutine);
    }

    private IEnumerator HoldMove()
    {
        _moveHeld = true;

        while (_moveHeld)
        {
            Debug.Log("Move!");
            MoveToPoint();
            yield return new WaitForSeconds(_holdMoveDelay);
        }
    }

    private void MoveToPoint()
    {
        // Player clicked to move
        if (Physics.Raycast(GameManager.MouseRayPoint, out var hitInfo, Mathf.Infinity, _groundMask))
        {
            Agent.SetDestination(hitInfo.point);
        }
    }
}
