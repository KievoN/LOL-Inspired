using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour
{
    public GameObject Instigator { get; private set; }

    private Vector3 _startPos;

    private int _damage;
    private int _distanceSpan;

    public void Initialize(GameObject instigator, int damage, int distanceSpan, bool teleportPlayer = false)
    {
        // Constructor; Initialize the projectiles stats on instantiation
        Instigator = instigator;

        _damage = damage;
        _distanceSpan = distanceSpan;

        _startPos = transform.position;

        if(teleportPlayer) TeleportPlayer();
    }

    public void TeleportPlayer()
    {
        Instigator.GetComponent<PlayerController>().TeleportToPoint(_startPos);

        Invoke(nameof(KillProjectile), 0.25f);
    }

    private void KillProjectile()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/ProjectileExplosion"), transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
