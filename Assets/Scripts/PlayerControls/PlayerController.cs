using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent Agent;

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
        Vector3 mousePos = new Vector3(Pointer.current.position.ReadValue().x, Pointer.current.position.ReadValue().y, 0f);
        Ray movePosition = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(movePosition, out var hitInfo))
        {
            Agent.SetDestination(hitInfo.point);
        }
    }
}
