using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public GameObject Instigator { get; private set; }

    private float _speed;
    private int _damage;

    public void Initialize(GameObject instigator, float speed, int damage, float lifeSpan)
    {
        // Constructor; Initialize the projectiles stats on instantiation
        Instigator = instigator;

        _speed = speed;
        _damage = damage;

        Invoke(nameof(KillProjectile), lifeSpan);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _speed);
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
        Destroy(gameObject);
    }
}
