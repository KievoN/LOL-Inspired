using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public GameObject Instigator { get; private set; }

    private Vector3 _startPos;

    private float _speed;
    private int _damage;
    private int _distanceSpan;

    private Transform _target;

    public void Initialize(GameObject instigator, float speed, int damage, int distanceSpan = 1000, Transform target = null)
    {
        // Constructor; Initialize the projectiles stats on instantiation
        Instigator = instigator;

        _speed = speed;
        _damage = damage;
        _distanceSpan = distanceSpan;

        _target = target;

        _startPos = transform.position;
    }

    public void Initialize(GameObject instigator, float speed, int damage, Transform target = null, int distanceSpan = 1000)
    {
        // Constructor; Initialize the projectiles stats on instantiation
        Instigator = instigator;

        _speed = speed;
        _damage = damage;
        _distanceSpan = distanceSpan;

        _target = target;

        _startPos = transform.position;
    }

    private void FixedUpdate()
    {
        if(_target == null) SkillShotProjectileMove();
        else TargetedProjectileMove();
    }

    private void SkillShotProjectileMove()
    {
        transform.Translate(Vector3.forward * _speed);
        if (Vector3.Distance(_startPos, transform.position) >= (_distanceSpan / 100))
        {
            // Projectile hit it's distance limit and will destroy itself
            KillProjectile();
        }
    }

    private void TargetedProjectileMove()
    {
        FaceTarget();

        // Move forward along the z transform
        transform.Translate(Vector3.forward * _speed);
    }

    public void FaceTarget()
    {
        // Find direction towards target
        Vector3 targetPositionFlat = new(_target.position.x, 0f, _target.position.z);
        Vector3 positionFlat = new(transform.position.x, 0f, transform.position.z);
        Vector3 forward = targetPositionFlat - positionFlat;

        // Rotate towards target
        transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Dummy"))
        {
            other.GetComponent<DummyTestScript>().TakeDamage(_damage);
            KillProjectile();
        }
    }

    private void KillProjectile()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/ProjectileExplosion"), transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
