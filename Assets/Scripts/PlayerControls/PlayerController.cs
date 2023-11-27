using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent Agent;

    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private LayerMask _attackMask;

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
            DetermineMoveType();
            yield return new WaitForSeconds(_holdMoveDelay);
        }
    }

    private void DetermineMoveType()
    {
        PlayerAttack playerAttack = GetComponent<PlayerAttack>();

        playerAttack.CancelAttack();

        // Clicked on attackable raycast
        if (Physics.Raycast(GameManager.MouseRayPoint, out var hitInfoAttack, Mathf.Infinity, _attackMask))
        {
            StartCoroutine(AttackMove(hitInfoAttack, playerAttack));
        }
        else
        // Player clicked to move
        if (Physics.Raycast(GameManager.MouseRayPoint, out var hitInfoMove, Mathf.Infinity, _groundMask))
        {
            MoveToPoint(hitInfoMove);
        }
    }

    private IEnumerator AttackMove(RaycastHit hitInfo, PlayerAttack playerAttack)
    {
        Vector3 targetPos = hitInfo.collider.gameObject.transform.position;
        Vector3 targetPosFlat = new(targetPos.x, 0f, targetPos.z);
        Vector3 posFlat = new(transform.position.x, 0f, transform.position.z);
        if (Vector3.Distance(targetPosFlat, posFlat) > playerAttack.GetAttackRange())
        {
            MoveToPoint(hitInfo);
        }

        float targetDistance = Vector3.Distance(targetPosFlat, posFlat);
        while (targetDistance > playerAttack.GetAttackRange())
        {
            posFlat = new(transform.position.x, 0f, transform.position.z);
            targetDistance = Vector3.Distance(targetPosFlat, posFlat);
            Debug.Log("Not in range!");
            yield return null;
        }

        Debug.Log("Attack!");

        playerAttack.StartAttack(hitInfo.collider.gameObject);

        Agent.SetDestination(transform.position);
    }

    private void MoveToPoint(RaycastHit hitInfo)
    {
        Debug.Log("Move!");
        Agent.SetDestination(hitInfo.point);
        Instantiate(Resources.Load<GameObject>("Cursors/ClickParticle"), hitInfo.point, Quaternion.identity);
    }

    public void TeleportToPoint(Vector3 teleportPoint)
    {
        Agent.Warp(teleportPoint);
    }

    #region UNITY METHODS

    private Texture2D _basicCursor, _attackCursor;

    private void Start()
    {
        _basicCursor = Resources.Load<Texture2D>("Cursors/Cursors 64/Cursor_Basic");
        _attackCursor = Resources.Load<Texture2D>("Cursors/Cursors 64/Cursor_Basic2");
    }

    private void Update()
    {
        // Updates the cursor image to correspond to what is being moused over, IE; Ground, Attackable Object
        if (Physics.Raycast(GameManager.MouseRayPoint, out var hitInfo, Mathf.Infinity, _attackMask))
        {
            Cursor.SetCursor(_attackCursor, Vector2.zero, CursorMode.Auto);
        }
        else Cursor.SetCursor(_basicCursor, Vector2.zero, CursorMode.Auto);
    }

    #endregion
}
